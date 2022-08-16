using jiraF.User.API.Infrastructure.Data.Contexts;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net;
using jiraF.User.API.Dtos.User.Registration;
using System.Text.Json;
using System.Text;
using System.Collections.Generic;

namespace jiraF.User.EndToEndTests.Controllers;

public class UserControllerTests
{
    private readonly HttpClient _client;

    public UserControllerTests()
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
    }

    [Theory]
    [InlineData("/User/2f857708-6e97-413b-b495-f2161135616a")]
    [InlineData("/User/2f857708-6e97-413b-b495-f2161135616b")]
    [InlineData("/User/2f857708-6e97-413b-b495-f2161135616c")]
    public async Task CheckAllGETApiMethodsIsValid_StatusCode200(string url)
    {
        HttpResponseMessage response = await _client.GetAsync(url);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal("application/json; charset=utf-8",
            response?.Content?.Headers?.ContentType?.ToString());
    }

    [Fact]
    public async Task GetByIds_CanGetDataByUserIds_StatusCode200()
    {
        List<Guid> requestDto = new()
        {
            new Guid("2f857708-6e97-413b-b495-f2161135616a"),
            new Guid("2f857708-6e97-413b-b495-f2161135616b")
        };
        string jsonModel = JsonSerializer.Serialize(requestDto);
        var stringContent = new StringContent(jsonModel, UnicodeEncoding.UTF8, "application/json");
        HttpResponseMessage response = await _client.PostAsync("/User/GetByIds", stringContent);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Registration_CanRegistrateValidUser_StatusCode200()
    {
        RegistrationUserRequestDto requestDto = new()
        {
            Name = "TestName",
        };
        string jsonModel = JsonSerializer.Serialize(requestDto);
        var stringContent = new StringContent(jsonModel, UnicodeEncoding.UTF8, "application/json");

        HttpResponseMessage response = await _client.PostAsync("/User", stringContent);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Bun_CanBunUserIfUserExists_StatusCode_200()
    {
        HttpResponseMessage response = await _client.DeleteAsync("/User/2f857708-6e97-413b-b495-f2161135616a");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}
