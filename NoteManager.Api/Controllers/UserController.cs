using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NoteManager.Api.Contracts.Responses;
using NoteManager.Domain.Models;
using NoteManager.Infrasturcture.Services;
using FileOptions = NoteManager.Domain.Options.FileOptions;

namespace NoteManager.Api.Controllers
{
    [ApiController]
    [Route("api/user")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UserController : ControllerBase
    {
        private readonly FileOptions _fileOptions;
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public UserController(UserManager<User> userManager, IMapper mapper, IFileService fileService,
            IOptions<FileOptions> fileOptions)
        {
            _userManager = userManager;
            _mapper = mapper;
            _fileService = fileService;
            _fileOptions = fileOptions.Value;
        }

        [HttpGet]
        public async Task<UserResponse> GetUserAsync()
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

        [HttpPost("profile-image")]
        public async Task<FileResponse> UploadImageAsync(IFormFile file)
        {
            var userId = HttpContext.User.FindFirst("id").Value;
            var result = await _fileService.SaveFileAsync(file, userId);
            return new FileResponse
                {Id = result.Id, Path = Path.Combine(_fileOptions.DirectoryPath, $"{result.Name}{result.Format}")};
        }

        [HttpGet("profile-image")]
        public async Task<FileResponse> GetImageAsync()
        {
            var userId = HttpContext.User.FindFirst("id").Value;
            var result = await _fileService.GetFileAsync(userId);
            return new FileResponse
                {Id = result.Id, Path = Path.Combine(_fileOptions.DirectoryPath, $"{result.Name}{result.Format}")};
        }

        [HttpDelete("profile-image")]
        public async Task DeleteImageAsync()
        {
            var userId = HttpContext.User.FindFirst("id").Value;
            await _fileService.DeleteFileAsync(userId);
        }

        //todo: должент быть метод редактирования
        //todo: настройка оповещений ( начало события - за 15 мин за 5 мин, истечение срока событий указанных в календаре)
    }
}