using jiraF.Goal.API.Infrastructure.Data.Contexts;
using jiraF.Goal.API.Secrets;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace jiraF.Goal.EndToEndTests
{
    public class PingTests
    {
        private readonly HttpClient _client;

        public PingTests()
        {
            var application = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        services.AddDbContext<AppDbContext>(options =>
                        {
                            options.UseInMemoryDatabase(Guid.NewGuid().ToString());
                        });
                    });
                });

            _client = application.CreateClient();
            _client.DefaultRequestHeaders.Add("GoalApiKey", ApiKey.Value);
        }

        [Theory]
        [InlineData("/ping")]
        public async Task CheckPing_StatusCode200(string url)
        {
            HttpResponseMessage response = await _client.GetAsync(url);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
