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

        [HttpPost("/registration")]
        public async Task<RegistrationResponse> Registration([FromBody]RegistrationRequest request )
        {
            var authResponse = await _authenticationService.RegistrationAsync(request.Email, request.Password,
                request.FirstName, request.LastName);
            if (!authResponse.Success)
            {
                return new RegistrationResponse()
                {
                    ErrorMessages = authResponse.ErrorMessages
                };
            }

            return new RegistrationResponse()
            {
                Token = authResponse.Token,
            };
        }
    }
}