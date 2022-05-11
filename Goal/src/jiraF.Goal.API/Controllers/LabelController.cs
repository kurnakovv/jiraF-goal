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
}
