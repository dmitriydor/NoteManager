using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
        private readonly IMapper _mapper;
        public UserController(UserManager<User> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<UserResponse> Get()
        {
            var userId = HttpContext.User.FindFirst("id").Value;
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId);
            return new UserResponse
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName
            };
        }

        [HttpPost]
        public async Task UploadImage(IFormFile file)
        {
        }
        
        //todo: должент быть метод редактирования
        //todo: настройка оповещений ( начало события - за 15 мин за 5 мин, истечение срока событий указанных в календаре)
    }
}