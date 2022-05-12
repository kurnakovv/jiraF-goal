using jiraF.Goal.API.Contracts;
using jiraF.Goal.API.Domain;
using jiraF.Goal.API.Dtos.Label;
using jiraF.Goal.API.Dtos.Label.Get;
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
            Title = x.Title.Value,
        });
        return new GetLabelsResponseDto() { Labels = dtos };
    }

    [HttpGet("{id}")]
    public async Task<LabelModel> Get(Guid id)
    {
        return await _labelRepository.GetByIdAsync(id);
    }

    [HttpPost]
    public async Task<IActionResult> Add(LabelModel model)
    {
        await _labelRepository.AddAsync(model);
        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> Update(Guid id, LabelModel model)
    {
        await _labelRepository.UpdateAsync(id, model);
        return Ok();
    }

    [HttpDelete]
    public async Task<ActionResult> Delete(Guid id)
    {
        await _labelRepository.DeleteByIdAsync(id);
        return Ok();
    }
}
