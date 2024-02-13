using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using Microsoft.VisualStudio.TestPlatform.TestHost;
namespace MusicPortal.IntegrationTests.PageAccessibilityTests
{
    public class BasicTests
    : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public BasicTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }
        // Ётот код представл€ет собой набор тестов дл€ проверки доступности и корректности контента
        // ƒл€ различных конечных точек
        // ѕроверка доступных URI
        // Get «апрос к каждому из указанных путей 
        [Theory]
        [InlineData("/")]
        [InlineData("/Account/SignIn")]
        [InlineData("/Account/SignUp")]
        [InlineData("/Genres/Index")]
        [InlineData("/Songs/Create")]
        public async Task Get_EndpointsReturnSuccessAndCorrectContentType(string url)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal("text/html; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }
    }
}