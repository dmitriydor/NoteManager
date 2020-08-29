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
                    ErrorMessages = ModelState.Values.SelectMany(x => x.Errors.Select(err => err.ErrorMessage))
                };
            }
            var authenticationResult = await _authenticationService.RegistrationAsync(request.Email, request.Password,
                request.FirstName, request.LastName);
            if (!authenticationResult.IsAuthenticated)
            {
                return new AuthResponse
                {
                    ErrorMessages = authenticationResult.ErrorMessages
                };
            }

            return new AuthResponse
            {
                Token = authenticationResult.Token,
                RefreshToken = authenticationResult.RefreshToken
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
                    ErrorMessages = authenticationResult.ErrorMessages
                };
            }

            return new AuthResponse
            {
                Token = authenticationResult.Token,
                RefreshToken = authenticationResult.RefreshToken
            };
        }

        [HttpPost("refresh-token")]
        public async Task<AuthResponse> RefreshToken([FromBody] RefreshRequest request)
        {
            var authenticationResult =
                await _authenticationService.RefreshTokenAsync(request.Token, request.RefreshToken);
            if (!authenticationResult.IsAuthenticated)
            {
                return new AuthResponse
                {
                    ErrorMessages = authenticationResult.ErrorMessages
                };
            }

            return new AuthResponse
            {
                Token = authenticationResult.Token,
                RefreshToken = authenticationResult.RefreshToken
            };
        }
    }
}