using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace jiraF.Goal.EndToEndTests.Controllers
{
    public class LabelControllerTests
    {
        private readonly HttpClient _client;

        public LabelControllerTests()
        {
            var application = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder =>
                {
                    // ... Configure test services
                });

            _client = application.CreateClient();
        }

        [Theory]
        [InlineData("/Label")]
        public async Task CheckAllGETApiMethodsIsValid_StatusCode200(string url)
        {
            HttpResponseMessage response = await _client.GetAsync(url);

            Assert.Equal(HttpStatusCode.OK, response?.StatusCode);
            Assert.Equal("application/json; charset=utf-8",
                response?.Content?.Headers?.ContentType?.ToString());
        }
    }
}
