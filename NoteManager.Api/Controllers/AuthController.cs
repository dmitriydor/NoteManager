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

        [HttpPost("registration")]
        public async Task<RegistrationResponse> Registration([FromBody]RegistrationRequest request )
        {
            var authenticationResult = await _authenticationService.RegistrationAsync(request.Email, request.Password,
                request.FirstName, request.LastName);
            if (!authenticationResult.Success)
            {
                return new RegistrationResponse
                {
                    ErrorMessages = authenticationResult.ErrorMessages
                };
            }

            return new RegistrationResponse
            {
                Token = authenticationResult.Token,
            };
        }

        [HttpPost("login")]
        public async Task<LoginResponse> Login([FromBody]LoginRequest request)
        {
            var authenticationResult = await _authenticationService.LoginAsync(request.Email, request.Password);
            if (!authenticationResult.Success)
            {
                return new LoginResponse
                {
                    ErrorMessages = authenticationResult.ErrorMessages
                };
            }

            return new LoginResponse
            {
                Token = authenticationResult.Token,
            };
        }
    }
}