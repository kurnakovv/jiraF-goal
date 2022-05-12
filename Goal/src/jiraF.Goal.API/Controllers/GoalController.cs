using jiraF.Goal.API.Contracts;
using jiraF.Goal.API.Domain;
using jiraF.Goal.API.Dtos;
using jiraF.Goal.API.Dtos.Goal;
using jiraF.Goal.API.Dtos.Goal.Get;
using jiraF.Goal.API.Dtos.Goal.GetById;
using jiraF.Goal.API.Dtos.Label;
using Microsoft.AspNetCore.Mvc;

namespace jiraF.Goal.API.Controllers;

[ApiController]
[Route("[controller]")]
public class GoalController : ControllerBase
{
    private readonly IGoalRepository _goalRepository;

    public GoalController(
        IGoalRepository goalRepository)
    {
        _goalRepository = goalRepository;
    }

    [HttpGet]
    public async Task<GetResponseDto> Get()
    {
        IEnumerable<GoalModel> goals = await _goalRepository.GetAsync();
        IEnumerable<GoalDto> dtos = goals.Select(x => Convert(x));
        return new GetResponseDto() { Goals = dtos };
    }

    [HttpGet("{id}")]
    public async Task<GetByIdResponseDto> Get(Guid id)
    {
        GoalModel goal = await _goalRepository.GetByIdAsync(id);
        return new GetByIdResponseDto
        {
            Goal = Convert(goal),
        };
    }

    [HttpPost]
    public async Task<IActionResult> Add(GoalModel model)
    {
        await _goalRepository.AddAsync(model);
        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> Update(Guid id, GoalModel model)
    {
        await _goalRepository.UpdateAsync(id, model);
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
            Title = model.Title.Value,
            Assigee = new UserDto
            {
                Img = model.Assignee.Img,
                Name = model.Assignee.Name,
            },
            Reporter = new UserDto
            {
                Name = model.Reporter.Name,
                Img = model.Reporter.Img,
            },
            DateOfCreate = model.DateOfCreate,
            DateOfUpdate = model.DateOfUpdate,
            Description = model.Description.Value,
            Label = new LabelDto
            {
                Title = model.Label.Title.Value,
            }
        };
    }
}
