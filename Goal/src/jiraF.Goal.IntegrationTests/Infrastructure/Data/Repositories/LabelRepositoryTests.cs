using jiraF.Goal.API.Contracts;
using jiraF.Goal.API.Domain;
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
    public class LabelRepositoryTests : IAsyncDisposable
    {
        private readonly AppDbContext _dbContext;
        private ILabelRepository _labelRepository;
        private static Guid _entityId = Guid.NewGuid();
        private LabelEntity _entity = new() { Id = _entityId, Title = "Test title" };

        public LabelRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            _dbContext = new AppDbContext(options);
            _dbContext.Labels.AddRange(new List<LabelEntity>
            {
                new LabelEntity { Title = "Title1" },
                new LabelEntity { Title = "Title2" },
                new LabelEntity { Title = "Title3" },
            });
            _dbContext.SaveChanges();
            _labelRepository = new LabelRepository(_dbContext);
        }

        public async ValueTask DisposeAsync()
        {
            _dbContext.Database.EnsureDeleted();
            await _dbContext.DisposeAsync();
            GC.SuppressFinalize(this);
        }

        [Fact]
        public async Task GetAsync_CanGetAllLabels_EnumerableOfLabelModel()
        {
            // Act
            IEnumerable<LabelModel> result = await _labelRepository.GetAsync();

            // Assert
            Assert.NotNull(result.ToList());
            Assert.True(result.Any());
            Assert.Equal(_dbContext.Labels.Count(), result.Count());
        }

        [Fact]
        public async Task GetByIdAsync_CanGetLabelById_LabelModel()
        {
            // Arrange
            _dbContext.Labels.Add(_entity);
            _dbContext.SaveChanges();

            // Act
            LabelModel result = await _labelRepository.GetByIdAsync(_entityId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(_entity.Title, result.Title.Value);
        }

        [Fact]
        public async Task AddAsync_CanAddLabel_EntityInStore()
        {
            // Arrange
            string uniqueTitle = Guid.NewGuid().ToString();
            LabelModel model = new(
                new Title(uniqueTitle));

            // Act
            await _labelRepository.AddAsync(model);

            // Assert
            Assert.NotNull(await _dbContext.Labels
                .FirstOrDefaultAsync(x => x.Title == uniqueTitle));
        }

        [Fact]
        public async Task UpdateAsync_CanUpdateLabel_UpdatedLabelInStore()
        {
            // Arrange
            _dbContext.Labels.Add(_entity);
            _dbContext.SaveChanges();
            LabelModel model = new(
                new Title("Updated title"));

            // Act
            await _labelRepository.UpdateAsync(_entityId, model);

            // Assert
            Assert.Equal("Updated title", _entity.Title);
        }
    }
}
