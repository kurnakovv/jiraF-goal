using jiraF.Goal.API.Contracts;
using jiraF.Goal.API.Domain;
using jiraF.Goal.API.Domain.Dtos;
using jiraF.Goal.API.GlobalVariables;
using jiraF.Goal.API.Infrastructure.Data.Contexts;
using jiraF.Goal.API.Infrastructure.Data.Entities;
using jiraF.Goal.API.Infrastructure.Data.Repositories;
using jiraF.Goal.API.ValueObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace jiraF.Goal.IntegrationTests.Infrastructure.Data.Repositories
{
    public class GoalRepositoryTests : IDisposable
    {
        private readonly AppDbContext _dbContext;
        private IGoalRepository _goalRepository;
        private static Guid _entityId = Guid.NewGuid();
        private GoalEntity _entity = new() { Id = _entityId, Title = "Test title", Description = "Test desc" };
        public GoalRepositoryTests()
        {
            TestVariables.IsWorkNow = true;
            DefaultMemberVariables.Id = "94ff67f3-294b-43f1-88ce-b815e80ff278";
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            _dbContext = new AppDbContext(options);
            _dbContext.Goals.AddRange(new List<GoalEntity>
            {
                new GoalEntity { Title = "Title1", DateOfCreate = DateTime.UtcNow, Description = "Desc1" },
                new GoalEntity { Title = "Title2", DateOfCreate = DateTime.UtcNow, Description = "Desc2" },
                new GoalEntity { Title = "Title3", DateOfCreate = DateTime.UtcNow, Description = "Desc3" },
            });
            _dbContext.SaveChanges();
            _goalRepository = new GoalRepository(_dbContext);
        }

        public void Dispose()
        {
            _dbContext.Database.EnsureDeleted();
            _dbContext.DisposeAsync();
            TestVariables.IsWorkNow = false;
            GC.SuppressFinalize(this);
        }

        [Fact]
        public async Task GetAsync_CanGetAllGoals_EnumerableOfGoalModel()
        {
            // Act
            IEnumerable<GoalModel> result = await _goalRepository.GetAsync();

            // Assert
            Assert.NotNull(result.ToList());
            Assert.True(result.Any());
            Assert.Equal(_dbContext.Goals.Count(), result.Count());
        }

        [Fact]
        public async Task GetByIdAsync_CanGetGoalById_GoalModel()
        {
            // Arrange
            _dbContext.Goals.Add(_entity);
            _dbContext.SaveChanges();

            // Act
            GoalModel result = await _goalRepository.GetByIdAsync(_entityId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(_entity.Id, result.Number);
            Assert.Equal(_entity.Title, result.Title.Value);
        }

        [Fact]
        public async Task AddAsync_CanAddGoal_EntityInStore()
        {
            // Arrange
            string uniqueTitle = Guid.NewGuid().ToString();
            string uniqueDescription = Guid.NewGuid().ToString();
            GoalModel model = new(
                new Title(uniqueTitle),
                new Description(uniqueDescription),
                Guid.NewGuid(),
                Guid.NewGuid(),
                "Test value");

            // Act
            await _goalRepository.AddAsync(model);

            // Assert
            Assert.NotNull(await _dbContext.Goals
                .FirstOrDefaultAsync(x => 
                    x.Title == uniqueTitle && 
                    x.Description == uniqueDescription));
        }

        [Fact]
        public async Task UpdateAsync_CanUpdateGoal_UpdatedGoalInStore()
        {
            // Arrange
            _dbContext.Goals.Add(_entity);
            _dbContext.SaveChanges();
            GoalModel model = new(
                new Title("Updated title"),
                new Description("Updated description"),
                Guid.NewGuid(),
                Guid.NewGuid(),
                "Test value");

            // Act
            await _goalRepository.UpdateAsync(_entityId, model);

            // Assert
            Assert.Equal("Updated title", _entity.Title);
            Assert.Equal("Updated description", _entity.Description);
        }

        [Fact]
        public async Task DeleteByIdAsync_CanDeleteGoal_DeletedGoalInStore()
        {
            // Arrange
            _dbContext.Goals.Add(_entity);
            _dbContext.SaveChanges();

            // Act
            await _goalRepository.DeleteByIdAsync(_entityId);

            // Assert
            Assert.Null(await _dbContext.Goals
                .FirstOrDefaultAsync(x => x.Id == _entityId));
        }
    }
}
