using jiraF.Goal.API.Contracts;
using jiraF.Goal.API.Domain;
using jiraF.Goal.API.Dtos.Label;
using jiraF.Goal.API.Dtos.Label.Add;
using jiraF.Goal.API.Dtos.Label.Get;
using jiraF.Goal.API.Dtos.Label.GetById;
using jiraF.Goal.API.Dtos.Label.Update;
using jiraF.Goal.API.ValueObjects;
using Microsoft.AspNetCore.Mvc;

namespace jiraF.Goal.API.Controllers;

[ApiController]
[Route("[controller]")]
public class LabelController : ControllerBase
{
    private readonly ILabelRepository _labelRepository;

    public LabelController(
        ILabelRepository labelRepository)
    {
        _labelRepository = labelRepository;
    }

    [HttpGet]
    public async Task<GetLabelsResponseDto> Get()
    {
        IEnumerable<LabelModel> goals = await _labelRepository.GetAsync();
        IEnumerable<LabelDto> dtos = goals.Select(x => new LabelDto
        {
            Id = x.Number,
            Title = x.Title.Value,
        });
        return new GetLabelsResponseDto() { Labels = dtos };
    }

    [HttpGet("{id}")]
    public async Task<GetLabelByIdResponseDto> Get(Guid id)
    {
        LabelModel label = await _labelRepository.GetByIdAsync(id);
        LabelDto dto = new()
        {
            Id = label.Number,
            Title = label.Title.Value,
        };
        return new GetLabelByIdResponseDto { Label = dto };
    }

    [HttpPost]
    public async Task<AddLabelResponseDto> Add(AddLabelRequestDto requestDto)
    {
        LabelModel model = new(
            new Title(requestDto.Title));
        Guid labelNumber = await _labelRepository.AddAsync(model);
        return new AddLabelResponseDto { Id = labelNumber };
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateLabelRequestDto requestDto)
    {
        LabelModel label = new(
            new Title(requestDto.Label.Title));
        await _labelRepository.UpdateAsync(requestDto.Label.Id, label);
        return Ok();
    }

    [HttpDelete]
    public async Task<ActionResult> Delete(Guid id)
    {
        await _labelRepository.DeleteByIdAsync(id);
        return Ok();
    }
}
