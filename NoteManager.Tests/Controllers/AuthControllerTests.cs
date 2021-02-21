using System.Threading.Tasks;
using Moq;
using NoteManager.Api.Contracts.Requests;
using NoteManager.Api.Controllers;
using NoteManager.Domain.Models;
using NoteManager.Infrasturcture.Services;
using NUnit.Framework;

namespace NoteManager.Tests.Controllers
{
    [TestFixture]
    public class AuthControllerTests
    {
        private readonly Mock<IAuthenticationService> _authService = new Mock<IAuthenticationService>();

        [Test]
        public async Task Registration_Register_Registered()
        {
            var controller = new AuthController(_authService.Object);
            var regRequest = new RegistrationRequest
            {
                Email = "example@example.com",
                FirstName = "FirstNameExample",
                LastName = "LastNameExample",
                Password = "Password"
            };

            _authService.Setup(x =>
                    x.RegistrationAsync(regRequest.Email, regRequest.Password, regRequest.FirstName,
                        regRequest.LastName))
                .ReturnsAsync(new AuthenticationResult
                {
                    IsAuthenticated = true
                });

            var result = await controller.Registration(regRequest);

            Assert.IsTrue(result.IsAuthenticated);
        }
    }
}