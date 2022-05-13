using jiraF.Goal.API.Attributes;
using jiraF.Goal.API.Secrets;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace jiraF.Goal.EndToEndTests.Attributes;

public class ApiKeyAuthorizeAttributeTests
{
    private readonly HttpClient _client;

    public ApiKeyAuthorizeAttributeTests()
    {
        var application = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                    // ... Configure test services
            });

        _client = application.CreateClient();
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