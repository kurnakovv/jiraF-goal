using jiraF.Goal.API.Dtos.Goal.Add;
using jiraF.Goal.API.Dtos.Goal.Update;
using jiraF.Goal.API.Infrastructure.Data.Contexts;
using jiraF.Goal.API.Secrets;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace jiraF.Goal.EndToEndTests.Controllers
{
    public class GoalControllerTests
    {
        private readonly HttpClient _client;

        public GoalControllerTests()
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
        [InlineData("/Goal")]
        [InlineData("/Goal/a27723d9-fd4c-4b83-add8-f1c9152585ea")]
        public async Task CheckAllGETApiMethodsIsValid_StatusCode200(string url)
        {
            HttpResponseMessage response = await _client.GetAsync(url);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal("application/json; charset=utf-8",
                response?.Content?.Headers?.ContentType?.ToString());
        }

        [Fact]
        public async Task Add_CanAddValidModel_StatusCode200()
        {
            AddGoalRequestDto requestDto = new()
            {
                Title = "Test value",
                Description = "Test value",
                AssigneeId = System.Guid.Empty,
                ReporterId = System.Guid.Empty,
                LabelTitle = "Test value" 
            };

            string jsonModel = JsonSerializer.Serialize(requestDto);
            var stringContent = new StringContent(jsonModel, UnicodeEncoding.UTF8, "application/json");

            HttpResponseMessage response = await _client.PostAsync("/Goal", stringContent);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Update_CanUpdateValidModel_StatusCode200()
        {
            UpdateGoalRequestDto requestDto = new()
            {
                Id = new System.Guid("a27723d9-fd4c-4b83-add8-f1c9152585ea"),
                Title = "Test value",
                Description = "Test value",
                AssigneeId = System.Guid.Empty,
                ReporterId = System.Guid.Empty,
                LabelTitle = "Test value"
            };

            string jsonModel = JsonSerializer.Serialize(requestDto);
            var stringContent = new StringContent(jsonModel, UnicodeEncoding.UTF8, "application/json");

            HttpResponseMessage response = await _client.PutAsync("/Goal", stringContent);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Delete_CanDeleteGoalByValidId_StatusCode200()
        {
            HttpResponseMessage response = await _client.DeleteAsync("/Goal?id=a27723d9-fd4c-4b83-add8-f1c9152585ea");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
