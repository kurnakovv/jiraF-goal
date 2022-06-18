using jiraF.Member.API.Domain;
using jiraF.Member.API.Dtos.Member;
using jiraF.Member.API.Dtos.Member.Registration;
using jiraF.Member.API.Infrastructure.Data.Contexts;
using jiraF.Member.API.Infrastructure.Data.Entities;
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

    [HttpPost]
    public async Task<RegistrationMemberResponseDto> Registration(RegistrationMemberRequestDto requestDto)
    {
        MemberModel model = new(requestDto.Name);
        MemberEntity entity = new() 
        { 
            Name = model.Name, 
            DateOfRegistration = DateTime.UtcNow 
        };
        _dbContext.Members.Add(entity);
        await _dbContext.SaveChangesAsync();
        return new RegistrationMemberResponseDto() 
        { 
            Member = new MemberDto 
            { 
                Id = entity.Id,
                DateOfRegistration = entity.DateOfRegistration,
                Name = entity.Name,
            } 
        };
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Bun(Guid id)
    {
        MemberEntity entity = await _dbContext.Members
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync();
        _dbContext.Members.Remove(entity);
        await _dbContext.SaveChangesAsync();
        return Ok();
    }
}
