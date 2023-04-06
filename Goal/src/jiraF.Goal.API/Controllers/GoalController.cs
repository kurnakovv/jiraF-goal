using jiraF.Goal.API.Attributes;
using jiraF.Goal.API.Contracts;
using jiraF.Goal.API.Domain;
using jiraF.Goal.API.Domain.Dtos;
using jiraF.Goal.API.Dtos;
using jiraF.Goal.API.Dtos.Goal;
using jiraF.Goal.API.Dtos.Goal.Add;
using jiraF.Goal.API.Dtos.Goal.Get;
using jiraF.Goal.API.Dtos.Goal.GetById;
using jiraF.Goal.API.Dtos.Goal.Update;
using jiraF.Goal.API.Dtos.Label;
using jiraF.Goal.API.GlobalVariables;
using jiraF.Goal.API.Infrastructure.ApiClients;
using jiraF.Goal.API.ValueObjects;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace jiraF.Goal.API.Controllers;

[ApiKeyAuthorize]
[ApiController]
[Route("[controller]")]
public class GoalController : ControllerBase
{
    private readonly IGoalRepository _goalRepository;
    private readonly MemberApiClient _memberApiClient;

    public GoalController(
        IGoalRepository goalRepository,
        IHttpClientFactory httpClientFactory,
        IConfiguration configuration)
    {
        _goalRepository = goalRepository;
        _memberApiClient = new MemberApiClient(httpClientFactory.CreateClient(configuration.GetValue<string>("ApiClients:MemberApiClient")));
    }

    [HttpGet]
    public async Task<GetGoalsResponseDto> Get()
    {
        IEnumerable<GoalModel> goals = await _goalRepository.GetAsync();
        List<Guid> memberIds = new();
        memberIds.AddRange(goals.Select(x => x.Reporter.Number));
        memberIds.AddRange(goals.Select(x => x.Assignee.Number));
        IEnumerable<MemberDto> members = await _memberApiClient.GetAsync(memberIds);
        IEnumerable<GoalModel> goalsWithMembers =
            from g in goals
            join m in members on g.Reporter.Number equals m.Id into reporters
            join m in members on g.Assignee.Number equals m.Id into assignees
            from r in reporters.DefaultIfEmpty()
            from a in assignees.DefaultIfEmpty()
            select new GoalModel(
                g.Number,
                g.Title,
                g.Description,
                r == null ? new Member(new Guid(DefaultMemberVariables.Id)) : new Member(r.Id, r.Name, r.Img),
                a == null ? new Member(new Guid(DefaultMemberVariables.Id)) : new Member(a.Id, a.Name, a.Img),
                g.DateOfCreate,
                g.DateOfUpdate,
                g.Label
                );
        IEnumerable<GoalDto> dtos = goalsWithMembers.Select(x => Convert(x));
        return new GetGoalsResponseDto() { Goals = dtos };
    }

    [HttpGet("{id}")]
    public async Task<GetGoalByIdResponseDto> Get(Guid id)
    {
        GoalModel goal = await _goalRepository.GetByIdAsync(id);
        IEnumerable<Guid> reporterAndAssigneeIds = new List<Guid>() { goal.Reporter.Number, goal.Assignee.Number };
        IEnumerable<MemberDto> reporterAndAssigneeDtos = new List<MemberDto>();
        using (HttpClient client = new() { BaseAddress = new Uri("https://jiraf-member.onrender.com") })
        {
            string jsonModel = JsonSerializer.Serialize(reporterAndAssigneeIds);
            var stringContent = new StringContent(jsonModel, UnicodeEncoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync("/Member/GetByIds", stringContent);
            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                reporterAndAssigneeDtos = JsonSerializer.Deserialize<IEnumerable<MemberDto>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
        }
        IEnumerable<Member> reporterAndAssignee = reporterAndAssigneeDtos.Select(x => new Member(x.Id, x.Name, x.Img));
        goal = new GoalModel(
            goal.Number,
            goal.Title,
            goal.Description,
            reporterAndAssignee.FirstOrDefault(x => x.Number == goal.Reporter.Number) ?? goal.Reporter,
            reporterAndAssignee.FirstOrDefault(x => x.Number == goal.Assignee.Number) ?? goal.Assignee,
            goal.DateOfCreate,
            goal.DateOfUpdate,
            goal.Label);
        return new GetGoalByIdResponseDto
        {
            Goal = Convert(goal),
        };
    }

    [HttpPost]
    public async Task<AddGoalResponseDto> Add(AddGoalRequestDto requestDto)
    {
        if (requestDto.ReporterId != null && !await _memberApiClient.IsExistsAsync(requestDto.ReporterId.Value))
        {
            throw new Exception($"Member by id: '{requestDto.ReporterId}' does not exists");
        }
        if (requestDto.AssigneeId != null && !await _memberApiClient.IsExistsAsync(requestDto.AssigneeId.Value))
        {
            throw new Exception($"Member by id: '{requestDto.AssigneeId}' does not exists");
        }
        GoalModel goal = new(
            new Title(requestDto.Title),
            new Description(requestDto.Description),
            requestDto.ReporterId ?? Guid.Empty,
            requestDto.AssigneeId ?? Guid.Empty,
            requestDto.LabelTitle);

        Guid goalNumber = await _goalRepository.AddAsync(goal);
        return new AddGoalResponseDto { Id = goalNumber };
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateGoalRequestDto requestDto)
    {
        GoalModel goal = new(
            new Title(requestDto.Title),
            new Description(requestDto.Description),
            requestDto.ReporterId,
            requestDto.AssigneeId,
            requestDto.LabelTitle);
        await _goalRepository.UpdateAsync(requestDto.Id, goal);
        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _goalRepository.DeleteByIdAsync(id);
        return Ok();
    }

    private GoalDto Convert(GoalModel model)
    {
        return new GoalDto
        {
            Id = model.Number,
            Title = model.Title.Value,
            Assigee = new MemberDto
            {
                Id = model.Assignee.Number,
                Img = model.Assignee.Img,
                Name = model.Assignee.Name,
            },
            Reporter = new MemberDto
            {
                Id = model.Reporter.Number,
                Name = model.Reporter.Name,
                Img = model.Reporter.Img,
            },
            DateOfCreate = model.DateOfCreate,
            DateOfUpdate = model.DateOfUpdate,
            Description = model.Description.Value,
            Label = new LabelDto
            {
                Id = model.Label.Number,
                Title = model.Label.Title.Value,
            }
        };
    }
}
