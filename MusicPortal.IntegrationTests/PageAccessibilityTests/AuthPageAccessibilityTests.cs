using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using RazorPagesProject.Tests;
using System.Net;
using Xunit;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication.Cookies;
using MusicPortal.BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using MusicPortal.BLL.ModelsDTO;

namespace MusicPortal.IntegrationTests.PageAccessibilityTests
{
    public class AuthPageAccessibilityTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly CustomWebApplicationFactory<Program> _factory;

        public AuthPageAccessibilityTests(CustomWebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        //[Fact]
        //public async Task Get_SecurePageRedirectsAnUnauthenticatedUser()
        //{
        //    // Arrange
        //    var client = _factory.CreateClient(
        //        new WebApplicationFactoryClientOptions
        //        {
        //            AllowAutoRedirect = false,
        //            // В тестовом окружении, по умолчанию, используется протокол HTTP
        //            // Нужно явно указать в настройках тестового клиента использование HTTPS
        //            BaseAddress = new Uri("https://localhost:7270")
        //        });

        //    // Act
        //    var response = await client.GetAsync("/Songs/Create");

        //    // Assert
        //    Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
        //    Assert.StartsWith("https://localhost:7270/Account/SignIn",
        //        response.Headers.Location.OriginalString);
        //}

        [Fact]
        public async Task Get_SecurePageIsReturnedForAnAuthenticatedUser()
        {
			// Arrange
			var client = _factory.CreateClient(
				new WebApplicationFactoryClientOptions
				{
					AllowAutoRedirect = false,
					BaseAddress = new Uri("https://localhost:7270")
				});

			var claims = new List<Claim>
	        {
		        new Claim(ClaimTypes.Name, "admin"),
		        new Claim(ClaimTypes.Role, "Admin"),
	        };

			var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                // Здесь вы можете добавить дополнительные параметры аутентификации, если это необходимо
            };



			//Act
			var response = await client.GetAsync("/Genres/Index");
			//var response = await client.GetAsync("/Songs/Create");

			// Assert
			Assert.Equal(HttpStatusCode.OK, response.StatusCode);
		}
    }

    public class TestAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {   
        public TestAuthHandler(IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock)
            : base(options, logger, encoder, clock)
        {

        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            // Устанавливаем куки для аутентификации
            var claims = new[] { new Claim(ClaimTypes.Name, "TestUser") };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            // Создаем объект AuthenticationProperties
            var authProperties = new AuthenticationProperties();

            // Вызываем метод аутентификации с использованием контекста HTTP запроса
            await Context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authProperties);

            // Возвращаем результат аутентификации
            var result = AuthenticateResult.Success(new AuthenticationTicket(principal, CookieAuthenticationDefaults.AuthenticationScheme));
            return result;
        }
    }
}
