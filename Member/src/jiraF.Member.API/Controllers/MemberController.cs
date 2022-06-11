using jiraF.Member.API.Domain;
using jiraF.Member.API.Dtos.Member;
using jiraF.Member.API.Infrastructure.Data.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace jiraF.Member.API.Controllers;

[ApiController]
[Route("[controller]")]
public class MemberController : ControllerBase
{
    private readonly AppDbContext _dbContext;

    public MemberController(
        AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet("{id}")]
    public async Task<MemberDto> Get(Guid id)
    {
        MemberModel model = await _dbContext.Members
            .Where(x => x.Id == id)
            .Select(x =>
                new MemberModel(
                    x.Id,
                    x.DateOfRegistration,
                    x.Name))
            .FirstOrDefaultAsync();
        MemberDto dto = new()
        {
            Id = model.Number,
            DateOfRegistration = model.DateOfRegistration,
            Name = model.Name,
        };
        return dto;
    }
}
