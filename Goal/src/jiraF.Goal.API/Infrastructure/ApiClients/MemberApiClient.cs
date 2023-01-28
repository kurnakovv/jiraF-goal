using jiraF.Goal.API.Dtos;
using System.Text.Json;
using System.Text;

namespace jiraF.Goal.API.Infrastructure.ApiClients;

public class MemberApiClient
{
    private readonly HttpClient _client;

    public MemberApiClient(HttpClient client)
    {
        _client = client;
    }

    public async Task<IEnumerable<MemberDto>> GetAsync(List<Guid> ids)
    {
        string jsonModel = JsonSerializer.Serialize(ids);
        var stringContent = new StringContent(jsonModel, UnicodeEncoding.UTF8, "application/json");
        HttpResponseMessage response = await _client.PostAsync("/Member/GetByIds", stringContent);
        if (!response.IsSuccessStatusCode)
        {
            return new List<MemberDto>();
        }
        string json = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<IEnumerable<MemberDto>>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
    }
}
