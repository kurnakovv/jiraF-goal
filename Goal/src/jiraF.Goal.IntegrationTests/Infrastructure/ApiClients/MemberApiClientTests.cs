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
    }

    [Fact]
    public async Task GetAsync_GetMembersByIds_MemberDtos()
    {
        Exception exception = await Record.ExceptionAsync(async () => 
            await _memberApiClient.GetAsync(new List<Guid> { new Guid("2f857708-6e97-413b-b495-f2161135616a") })
        );
        Assert.Null(exception);
    }
}
