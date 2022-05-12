using jiraF.Goal.API.Domain;
using jiraF.Goal.API.Domain.Dtos;
using jiraF.Goal.API.Dtos.Goal.Add;
using jiraF.Goal.API.ValueObjects;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
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
            AddRequestDto requestDto = new()
            {
                Title = "Test value",
                Description = "Test value",
                AssigneeId = System.Guid.Empty,
                ReporterId = System.Guid.Empty,
                Label = new API.Dtos.Label.LabelDto { Title = "Test value" }
            };

            string jsonModel = JsonSerializer.Serialize(requestDto);
            var stringContent = new StringContent(jsonModel, UnicodeEncoding.UTF8, "application/json");

            HttpResponseMessage response = await _client.PostAsync("/Goal", stringContent);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Update_CanUpdateValidModel_StatusCode200()
        {
            GoalModel goal = new(
                new Title("Test value"),
                new Description("Test value"),
                new User(),
                new User(),
                new LabelModel(new Title("Test value")));

            string jsonModel = JsonSerializer.Serialize(goal);
            var stringContent = new StringContent(jsonModel, UnicodeEncoding.UTF8, "application/json");

            HttpResponseMessage response = await _client.PutAsync("/Goal?id=a27723d9-fd4c-4b83-add8-f1c9152585ea", stringContent);

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
