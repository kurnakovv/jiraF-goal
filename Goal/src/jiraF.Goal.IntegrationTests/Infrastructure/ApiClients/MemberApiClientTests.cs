using jiraF.Goal.API.GlobalVariables;
using jiraF.Goal.API.Infrastructure.ApiClients;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace jiraF.Goal.IntegrationTests.Infrastructure.ApiClients;

public class MemberApiClientTests
{
    private MemberApiClient _memberApiClient;

    public MemberApiClientTests()
    {
        _memberApiClient = new MemberApiClient(new HttpClient
        {
            BaseAddress = new Uri("https://jiraf-member.onrender.com/")
        });
        DefaultMemberVariables.Id = "94ff67f3-294b-43f1-88ce-b815e80ff278";
    }

    [Fact]
    public async Task GetAsync_GetMembersByIds_PassWithoutException()
    {
        Exception exception = await Record.ExceptionAsync(async () => 
            await _memberApiClient.GetAsync(new List<Guid> { new Guid("2f857708-6e97-413b-b495-f2161135616a") })
        );
        Assert.Null(exception);
    }

    [Fact]
    public async Task GetAsync_GetMembersById_PassWithoutException()
    {
        Exception exception = await Record.ExceptionAsync(async () =>
            await _memberApiClient.GetAsync(new Guid("2f857708-6e97-413b-b495-f2161135616a"))
        );
        Assert.Null(exception);
    }

    [Fact]
    public async Task IsExistsAsync_DefaultMemberIsExists_PassWithoutException()
    {
        Exception exception = await Record.ExceptionAsync(async () =>
            await _memberApiClient.IsExistsAsync(new Guid(DefaultMemberVariables.Id))
        );
        Assert.Null(exception);
    }

    [Fact]
    public async Task IsExistsAsync_DefaultMemberIsExists_True()
    {
        bool isExists = await _memberApiClient.IsExistsAsync(new Guid(DefaultMemberVariables.Id));
        Assert.True(isExists);
    }

    [Fact]
    public async Task IsExistsAsync_MemberIsNotExistsByInvalidId_False()
    {
        Guid invalidId = new("53c5d4ea-73a4-4f41-8635-5a54f45b2e14");
        bool isExists = await _memberApiClient.IsExistsAsync(invalidId);
        Assert.False(isExists);
    }
}
