using jiraF.Goal.API.Contracts;
using jiraF.Goal.API.Domain;
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
    public async Task<IEnumerable<GoalModel>> Get()
    {
        return await _goalRepository.GetAsync();
    }
}
