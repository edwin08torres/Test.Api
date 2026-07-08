using AutosChallenge.Api.Controllers;
using AutosChallenge.Core;
using AutosChallenge.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace AutosChallenge.Tests
{
    public class MarcasAutosControllerTests : IDisposable
    {
        private readonly AppDbContext _dbContext;
        private readonly MarcasAutosController _controller;

        public MarcasAutosControllerTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _dbContext = new AppDbContext(options);
            _dbContext.Database.EnsureCreated();

            _controller = new MarcasAutosController(_dbContext);
        }

        [Fact]
        public async Task GetMarcasAutos_ReturnsOkResult_WithSeededData()
        {
            var result = await _controller.GetMarcasAutos();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var marcas = Assert.IsAssignableFrom<IEnumerable<MarcaAuto>>(okResult.Value);
            
            Assert.True(marcas.Count() >= 3);
        }

        [Fact]
        public async Task GetMarcasAutos_ReturnsNoContent_WhenDatabaseIsEmpty()
        {
            _dbContext.MarcasAutos.RemoveRange(_dbContext.MarcasAutos);
            await _dbContext.SaveChangesAsync();

            var result = await _controller.GetMarcasAutos();

            Assert.IsType<NoContentResult>(result.Result);
        }

        public void Dispose()
        {
            _dbContext.Database.EnsureDeleted();
            _dbContext.Dispose();
        }
    }
}
