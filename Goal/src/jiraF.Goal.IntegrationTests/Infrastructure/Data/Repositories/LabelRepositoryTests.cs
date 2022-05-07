using jiraF.Goal.API.Contracts;
using jiraF.Goal.API.Domain;
using jiraF.Goal.API.Infrastructure.Data.Contexts;
using jiraF.Goal.API.Infrastructure.Data.Entities;
using jiraF.Goal.API.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace jiraF.Goal.IntegrationTests.Infrastructure.Data.Repositories
{
    public class LabelRepositoryTests : IDisposable
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

        public void Dispose()
        {
            _dbContext.Database.EnsureDeleted();
            _dbContext.DisposeAsync();
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
    }
}
