using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace jiraF.Goal.EndToEndTests.Controllers
{
    public class GoalController
    {
        private readonly HttpClient _client;

        public GoalController()
        {
            var application = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder =>
                {
                    // ... Configure test services
                });

            _client = application.CreateClient();
        }

        [Theory]
        [InlineData("/Goal")]
        [InlineData("/Goal/a27723d9-fd4c-4b83-add8-f1c9152585ea")]
        public async Task CheckAllApiMethodsIsValid_StatusCode200(string url)
        {
            HttpResponseMessage response = await _client.GetAsync(url);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal("application/json; charset=utf-8",
                response?.Content?.Headers?.ContentType?.ToString());
        }
    }
}
