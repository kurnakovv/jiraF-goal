using jiraF.Goal.API.Contracts;
using jiraF.Goal.API.Domain;
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
    public async Task<IEnumerable<LabelModel>> Get()
    {
        return await _labelRepository.GetAsync();
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
