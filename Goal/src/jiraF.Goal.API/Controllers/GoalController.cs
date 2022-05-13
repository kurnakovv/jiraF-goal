using jiraF.Goal.API.Attributes;
using jiraF.Goal.API.Contracts;
using jiraF.Goal.API.Domain;
using jiraF.Goal.API.Dtos;
using jiraF.Goal.API.Dtos.Goal;
using jiraF.Goal.API.Dtos.Goal.Add;
using jiraF.Goal.API.Dtos.Goal.Get;
using jiraF.Goal.API.Dtos.Goal.GetById;
using jiraF.Goal.API.Dtos.Goal.Update;
using jiraF.Goal.API.Dtos.Label;
using jiraF.Goal.API.ValueObjects;
using Microsoft.AspNetCore.Mvc;

namespace jiraF.Goal.API.Controllers;

[ApiKeyAuthorize]
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
    public async Task<AddResponseDto> Add(AddRequestDto requestDto)
    {
        GoalModel goal = new(
            new Title(requestDto.Title),
            new Description(requestDto.Description),
            new Domain.Dtos.User { },
            new Domain.Dtos.User { },
            new LabelModel(new Title(requestDto.Title)));

        Guid goalNumber = await _goalRepository.AddAsync(goal);
        return new AddResponseDto { Id = goalNumber };
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateRequestDto requestDto)
    {
        GoalModel goal = new(
            new Title(requestDto.Title),
            new Description(requestDto.Description),
            new Domain.Dtos.User { },
            new Domain.Dtos.User { },
            new LabelModel(new Title(requestDto.Title)));
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
