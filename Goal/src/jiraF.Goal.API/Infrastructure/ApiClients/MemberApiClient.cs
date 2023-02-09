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

    public async Task<MemberDto> GetAsync(Guid id)
    {
        string jsonModel = JsonSerializer.Serialize(id);
        var stringContent = new StringContent(jsonModel, UnicodeEncoding.UTF8, "application/json");
        HttpResponseMessage response = await _client.PostAsync("/Member/Get", stringContent);
        if (!response.IsSuccessStatusCode)
        {
            return new MemberDto();
        }
        string json = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<MemberDto>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
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

    public async Task<bool> IsExistsAsync(Guid id)
    {
        HttpResponseMessage response = await _client.GetAsync($"/Member/IsExists/{id}");
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Error in member API client, status code: {response.StatusCode}");
        }
        string json = await response.Content.ReadAsStringAsync();
        bool isExist = JsonSerializer.Deserialize<bool>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
        return isExist;
    }
}
