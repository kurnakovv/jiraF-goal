using jiraF.Goal.API.Domain;
using jiraF.Goal.API.Dtos.Label;
using jiraF.Goal.API.Dtos.Label.Add;
using jiraF.Goal.API.Dtos.Label.Update;
using jiraF.Goal.API.GlobalVariables;
using jiraF.Goal.API.Infrastructure.Data.Contexts;
using jiraF.Goal.API.Secrets;
using jiraF.Goal.API.ValueObjects;
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
    public class LabelControllerTests : IDisposable
    {
        private readonly HttpClient _client;

        public LabelControllerTests()
        {
            TestVariables.IsWorkNow = true;
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

        public void Dispose()
        {
            TestVariables.IsWorkNow = false;
            GC.SuppressFinalize(this);
        }

        [Theory(Skip = "Does not work for GitHub actions")]
        [InlineData("/Label")]
        [InlineData("/Label/4674f93c-6331-4e63-b298-349619fa8741")]
        public async Task CheckAllGETApiMethodsIsValid_StatusCode200(string url)
        {
            HttpResponseMessage response = await _client.GetAsync(url);

            Assert.Equal(HttpStatusCode.OK, response?.StatusCode);
            Assert.Equal("application/json; charset=utf-8",
                response?.Content?.Headers?.ContentType?.ToString());
        }

        [Fact(Skip = "Does not work for GitHub actions")]
        public async Task Add_CanAddValidModel_StatusCode200()
        {
            AddLabelRequestDto requestDto = new()
            {
                Title = "New test value",
            };
            string jsonModel = JsonSerializer.Serialize(requestDto);
            var stringContent = new StringContent(jsonModel, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _client.PostAsync("/Label", stringContent);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact(Skip = "Does not work for GitHub actions")]
        public async Task Update_CanUpdateValidModel_StatusCode200()
        {
            UpdateLabelRequestDto requestDto = new()
            {
                Label = new LabelDto
                {
                    Id = new System.Guid("4674f93c-6331-4e63-b298-349619fa8741"),
                    Title = "Updated title",
                }
            };

            string jsonModel = JsonSerializer.Serialize(requestDto);
            var stringContent = new StringContent(jsonModel, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _client.PutAsync("/Label", stringContent);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact(Skip = "Does not work for GitHub actions")]
        public async Task Delete_CanDeleteModelByValidId_StatusCode200()
        {
            HttpResponseMessage response = await _client.DeleteAsync("/Label?id=4674f93c-6331-4e63-b298-349619fa8741");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
