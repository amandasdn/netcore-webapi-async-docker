using Blog.Application.DTOs;
using Blog.WebUI.IntegrationTests.Helpers;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Text;
using System.Text.Json;

namespace Blog.WebUI.IntegrationTests.Controllers
{
    public class CommentControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        private string _apiKeyName;
        private string _apikeyValue;

        public CommentControllerTests(CustomWebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();

            var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

            _apiKeyName = configuration["ApiKey:KeyName"];
            _apikeyValue = configuration["ApiKey:KeyValue"];
        }

        [Fact]
        public void GetComment_WithoutApiKey_Unauthorized()
        {
            // Act
            var response = _client.GetAsync("/comment").Result;
            var responseString = response.Content.ReadAsStringAsync().Result;

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public void GetComment_InvalidApiKey_Unauthorized()
        {
            // Arrange
            _client.DefaultRequestHeaders.Add(_apiKeyName, _apikeyValue + "test");

            // Act
            var response = _client.GetAsync("/comment").Result;
            var responseString = response.Content.ReadAsStringAsync().Result;

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public void GetComment_WithApiKeyWithoutFilter_BadRequest()
        {
            // Arrange
            _client.DefaultRequestHeaders.Add(_apiKeyName, _apikeyValue);

            // Act
            var response = _client.GetAsync("/comment").Result;
            var responseString = response.Content.ReadAsStringAsync().Result;

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public void GetComment_WithApiKeyWithFilter_Success()
        {
            // Arrange
            _client.DefaultRequestHeaders.Add(_apiKeyName, _apikeyValue);
            var filter = new CommentFilterRequestDTO { PageSize = 1, PageNumber = 10 };
            var sb = new StringBuilder();
            sb.Append($"{filter.GetBindProperty(nameof(filter.PageSize))}={filter.PageSize}");
            sb.Append($"&{filter.GetBindProperty(nameof(filter.PageNumber))}={filter.PageNumber}");

            // Act
            var response = _client.GetAsync("/comment?" + sb.ToString()).Result;
            var responseString = response.Content.ReadAsStringAsync().Result;

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public void PostComment_WithApiKeyWithoutRequest_BadRequest()
        {
            // Arrange
            _client.DefaultRequestHeaders.Add(_apiKeyName, _apikeyValue);
            var request = new CommentRequestDTO();
            var content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");

            // Act
            var response = _client.PostAsync("/comment", content).Result;
            var responseString = response.Content.ReadAsStringAsync().Result;

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public void PostComment_WithApiKeyWithRequest_Success()
        {
            // Arrange
            _client.DefaultRequestHeaders.Add(_apiKeyName, _apikeyValue);
            var request = new CommentRequestDTO { Author = "Jenny", Content = "Nulla faucibus pharetra tempus." };
            var content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");

            // Act
            var response = _client.PostAsync("/comment", content).Result;
            var responseString = response.Content.ReadAsStringAsync().Result;

            // Assert
            Assert.Equal(HttpStatusCode.Accepted, response.StatusCode);
        }
    }
}