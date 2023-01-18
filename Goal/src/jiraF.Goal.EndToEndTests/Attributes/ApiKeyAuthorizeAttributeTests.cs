using jiraF.Goal.API.Attributes;
using jiraF.Goal.API.Secrets;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using jiraF.Goal.API.Infrastructure.Data.Contexts;
using System;
using jiraF.Goal.API.GlobalVariables;

namespace jiraF.Goal.EndToEndTests.Attributes;

public class ApiKeyAuthorizeAttributeTests : IDisposable
{
    private readonly HttpClient _client;

    public ApiKeyAuthorizeAttributeTests()
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
    }

    public void Dispose()
    {
        TestVariables.IsWorkNow = false;
    }

    [Fact]
    public async Task ApiKeyAuthorizeAttribute_CanGetOkStatusCodeIfApiKeyExist_StatusCode200()
    {
        // Arrange
        _client.DefaultRequestHeaders.Add("GoalApiKey", ApiKey.Value);

        // Act
        HttpResponseMessage? response = await _client.GetAsync("/Test");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response?.StatusCode);
    }

    [Fact]
    public async Task ApiKeyAuthorizeAttribute_CannotGetOkStatusCodeIfApiKeyNotExist_StatusCode401()
    {
        // Act
        HttpResponseMessage? response = await _client.GetAsync("/Test");

        // Assert
        Assert.Equal(HttpStatusCode.Unauthorized, response?.StatusCode);
    }

    [Fact]
    public async Task ApiKeyAuthorizeAttribute_CanGetOkStatusCodeIfDisabledPropertyIsTrue_StatusCode200()
    {
        // Act
        HttpResponseMessage? response = await _client.GetAsync("/Test/123");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response?.StatusCode);
    }
}