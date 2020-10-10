using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NoteManager.Api.Data.Repositories;
using NoteManager.Api.Models;
using NoteManager.Api.Options;

namespace NoteManager.Api.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<User> _userManager;
        private readonly JwtOptions _jwtOptions;
        private readonly TokenValidationParameters _tokenValidationParameters;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        public AuthenticationService(UserManager<User> userManager, IOptions<JwtOptions> jwtOptions, TokenValidationParameters tokenValidationParameters, IRefreshTokenRepository refreshTokenRepository)
        {
            _userManager = userManager;
            _tokenValidationParameters = tokenValidationParameters;
            _refreshTokenRepository = refreshTokenRepository;
            _jwtOptions = jwtOptions.Value;
        }
        public async Task<AuthenticationResult> RegistrationAsync(string email, string password, string firstName, string lastName)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                return new AuthenticationResult
                {
                    IsAuthenticated = false,
                    ErrorMessages = new[] {"User with this email already exists"}
                };
            }

            var newUser = new User
            {
                Email = email,
                UserName = email,
                FirstName = firstName,
                LastName = lastName,
                CreatedDate = DateTime.Now
            };

            var createdUser = await _userManager.CreateAsync(newUser, password);
            if (!createdUser.Succeeded)
            {
                return new AuthenticationResult
                {
                    IsAuthenticated = createdUser.Succeeded,
                    ErrorMessages = createdUser.Errors.Select(x => x.Description)
                };
            }

            return await GenerateJwtToken(newUser);
        }

        public async Task<AuthenticationResult> LoginAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return new AuthenticationResult
                {
                    IsAuthenticated = false,
                    ErrorMessages = new[] {"User does not exits"}
                };
            }

            var validPassword = await _userManager.CheckPasswordAsync(user, password);
            if (!validPassword)
            {
                return new AuthenticationResult
                {
                    IsAuthenticated = false,
                    ErrorMessages = new[] {"Wrong email or password"}
                };
            }

            return await GenerateJwtToken(user);
        }

        public async Task<AuthenticationResult> RefreshTokenAsync(string accessToken, string refreshToken)
        {
            var validatedToken = GetPrincipal(accessToken);	
            if (validatedToken == null)	
            {	
                return new AuthenticationResult	
                {	
                    ErrorMessages = new[] {"Invalid Token"}	
                };	
            }	

            var expDateUnix =	
                long.Parse(validatedToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Exp).Value);	
            var expDateTimeUtc = DateTime.UnixEpoch.AddSeconds(expDateUnix).Subtract(_jwtOptions.LifeTime);	

            if (expDateTimeUtc > DateTime.UtcNow)	
            {	
                return new AuthenticationResult	
                {	
                    ErrorMessages = new[] {"This token has not expired yet"}	
                };	
            }
            var jti = validatedToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Jti).Value;
            var storedRefreshToken = await _refreshTokenRepository.GetTokenAsync(refreshToken);
            if (storedRefreshToken == null)
            {
                return new AuthenticationResult
                {
                    ErrorMessages = new[] {"This refresh token doesn't exist"}
                };
            }

            if (DateTime.UtcNow > storedRefreshToken.ExpiryDate)
            {
                return new AuthenticationResult
                {
                    ErrorMessages = new[] {"This refresh token has expired"}
                };
            }
            if (storedRefreshToken.Invalidated)
            {
                return new AuthenticationResult
                {
                    ErrorMessages = new[] {"This refresh token has been invalidated"}
                };
            }
            if (storedRefreshToken.Used)
            {
                return new AuthenticationResult
                {
                    ErrorMessages = new[] {"This refresh token has been used"}
                };
            }
            
            if (storedRefreshToken.Jti != jti)	
            {	
                return new AuthenticationResult	
                {	
                    ErrorMessages = new[] {"This refresh token does not match this JWT"}	
                };	
            }	

            storedRefreshToken.Used = true;
            await _refreshTokenRepository.UpdateTokenAsync(storedRefreshToken);
            User user = await _userManager.FindByIdAsync(validatedToken.Claims.Single(x => x.Type == "id").Value);
            return await GenerateJwtToken(user);
        }

        public async Task SetRefreshTokenInCookie(RefreshToken refreshToken, HttpContext context)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                Expires = refreshToken.ExpiryDate,
                Path = "api/authenticate"
            };
            await Task.Run(() => context.Response.Cookies.Append("refresh_token", refreshToken.Token, cookieOptions));
        }

        // для харанения в cookie 
        public async Task SetAccessTokenInCookie(string accessToken, HttpContext context)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict
            };
            await Task.Run(() => context.Response.Cookies.Append("access_token", accessToken, cookieOptions));
        }

        private ClaimsPrincipal GetPrincipal(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                var principal = tokenHandler.ValidateToken(token, _tokenValidationParameters, out var validatedToken);
                if (!((validatedToken is JwtSecurityToken jwtSecurityToken) &&
                      jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                          StringComparison.InvariantCultureIgnoreCase)))
                {
                    return null;
                }

                return principal;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }
        
        private async Task<AuthenticationResult> GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtOptions.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim("id", user.Id)
                }),
                Expires = DateTime.UtcNow.Add(_jwtOptions.LifeTime),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new AuthenticationResult
            {
                IsAuthenticated = true,
                Token = tokenHandler.WriteToken(token),
                RefreshToken = await _refreshTokenRepository.CreateTokenAsync(token, user)
            };
        }
    }
}