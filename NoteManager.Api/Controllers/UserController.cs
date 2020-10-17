using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NoteManager.Api.Contracts.Responses;
using NoteManager.Api.Models;
using NoteManager.Api.Services;

namespace NoteManager.Api.Controllers
{
    [ApiController]
    [Route("api/user")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UserController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IAuthenticationService _authenticationService;
        private readonly IMapper _mapper;
        public UserController(UserManager<User> userManager, IAuthenticationService authenticationService, IMapper mapper)
        {
            _userManager = userManager;
            _authenticationService = authenticationService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<UserResponse> Get()
        {
            var accessToken = HttpContext.Request.Headers["Authorization"];
            var token = accessToken.First().Split(' ')[1];
            var userId = _authenticationService.GetPrincipal(token).FindFirst("id").Value;
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId);
            return new UserResponse
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName
            };
        }
        
        //todo: должент быть метод редактирования
        //todo: настройка оповещений ( начало события - за 15 мин за 5 мин, истечение срока событий указанных в календаре)
    }
}