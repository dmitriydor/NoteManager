using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NoteManager.Api.Contracts.Requests;
using NoteManager.Api.Contracts.Responses;
using NoteManager.Api.Services;

namespace NoteManager.Api.Controllers
{
    [ApiController]
    [Route("api/authenticate")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        public AuthController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("register")]
        public async Task<AuthResponse> Registration([FromBody]RegistrationRequest request )
        {
            if (!ModelState.IsValid)
            {
                return new AuthResponse
                {
                    IsAuthenticated = false,
                    ErrorMessages = ModelState.Values.SelectMany(x => x.Errors.Select(err => err.ErrorMessage))
                };
            }
            var authenticationResult = await _authenticationService.RegistrationAsync(request.Email, request.Password,
                request.FirstName, request.LastName);
            if (!authenticationResult.IsAuthenticated)
            {
                return new AuthResponse
                {
                    IsAuthenticated = false,
                    ErrorMessages = authenticationResult.ErrorMessages
                };
            }
            await _authenticationService.SetRefreshTokenInCookie(authenticationResult.RefreshToken, HttpContext);
            return new AuthResponse
            {
                IsAuthenticated = authenticationResult.IsAuthenticated,
                AccessToken = authenticationResult.Token
            };
        }

        [HttpPost("login")]
        public async Task<AuthResponse> Login([FromBody]LoginRequest request)
        {
            var authenticationResult = await _authenticationService.LoginAsync(request.Email, request.Password);
            if (!authenticationResult.IsAuthenticated)
            {
                return new AuthResponse
                {
                    IsAuthenticated = false,
                    ErrorMessages = authenticationResult.ErrorMessages
                };
            }
            await _authenticationService.SetRefreshTokenInCookie(authenticationResult.RefreshToken, HttpContext);
            return new AuthResponse
            {
                IsAuthenticated = authenticationResult.IsAuthenticated,
                AccessToken = authenticationResult.Token
            };
        }

        [HttpPost("refresh-token")]
        public async Task<AuthResponse> RefreshToken()
        {
            var accessToken = HttpContext.Request.Headers["Authorization"]
                .FirstOrDefault(_ => true)?
                .Split(' ')[1];
            var refreshToken = Request.Cookies["refresh_token"];
            if (string.IsNullOrEmpty(refreshToken))
            {
                return new AuthResponse
                {
                    IsAuthenticated = false,
                    ErrorMessages = new []{"Refresh Token not found!"}
                };
            }
            var authenticationResult =
                await _authenticationService.RefreshTokenAsync(accessToken, refreshToken);
            if (!authenticationResult.IsAuthenticated)
            {
                return new AuthResponse
                {
                    IsAuthenticated = false,
                    ErrorMessages = authenticationResult.ErrorMessages
                };
            }
            await _authenticationService.SetRefreshTokenInCookie(authenticationResult.RefreshToken, HttpContext);
            return new AuthResponse
            {
                IsAuthenticated = authenticationResult.IsAuthenticated,
                AccessToken = authenticationResult.Token
            };
        }
    }
}