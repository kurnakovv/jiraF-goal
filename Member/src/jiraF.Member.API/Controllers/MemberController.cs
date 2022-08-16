using jiraF.User.API.Domain;
using jiraF.User.API.Dtos.User;
using jiraF.User.API.Dtos.User.Registration;
using jiraF.User.API.Infrastructure.Data.Contexts;
using jiraF.User.API.Infrastructure.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace jiraF.User.API.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly AppDbContext _dbContext;

    public UserController(
        AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet("{id}")]
    public async Task<UserDto> Get(Guid id)
    {
        UserModel model = await _dbContext.Users
            .Where(x => x.Id == id)
            .Select(x =>
                new UserModel(
                    x.Id,
                    x.DateOfRegistration,
                    x.Name))
            .FirstOrDefaultAsync();
        UserDto dto = new()
        {
            Id = model.Number,
            DateOfRegistration = model.DateOfRegistration,
            Name = model.Name,
        };
        return dto;
    }

    [HttpPost("GetByIds")]
    public async Task<IEnumerable<UserDto>> GetByIds(List<Guid> ids)
    {
        IEnumerable<UserModel> models = await _dbContext.Users
            .Where(x => ids.Contains(x.Id))
            .Select(x => new UserModel(
                x.Id,
                x.DateOfRegistration,
                x.Name))
            .ToListAsync();
        return models.Select(x => new UserDto()
        {
            Id = x.Number,
            DateOfRegistration = x.DateOfRegistration,
            Name = x.Name,
        }).ToList();
    }

    [HttpPost]
    public async Task<RegistrationUserResponseDto> Registration(RegistrationUserRequestDto requestDto)
    {
        UserModel model = new(requestDto.Name);
        UserEntity entity = new() 
        { 
            Name = model.Name, 
            DateOfRegistration = DateTime.UtcNow 
        };
        _dbContext.Users.Add(entity);
        await _dbContext.SaveChangesAsync();
        return new RegistrationUserResponseDto() 
        { 
            User = new UserDto 
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
        UserEntity entity = await _dbContext.Users
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync();
        _dbContext.Users.Remove(entity);
        await _dbContext.SaveChangesAsync();
        return Ok();
    }
}
