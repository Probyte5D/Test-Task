using Moq;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using TestSystem.Models;
using TestSystem.Services;
using TestSystem.Controllers;
using Microsoft.AspNetCore.Mvc;
using TestSystem.Services; // <- necessario per IMongoDBService


namespace TestSystem.Tests
{
    public class MongoServiceTests
    {
        [Fact]
        public void GetAllTests_ShouldReturnTestsFromMock()
        {
            // Lista fake di Test
            var testList = new List<Test>
            {
                new Test { Title = "Test 1" },
                new Test { Title = "Test 2" }
            };

            // Mock del cursore MongoDB
            var mockCursor = new Mock<IAsyncCursor<Test>>();
            mockCursor.Setup(_ => _.Current).Returns(testList);
            mockCursor.SetupSequence(_ => _.MoveNext(It.IsAny<System.Threading.CancellationToken>()))
                      .Returns(true)
                      .Returns(false);
            mockCursor.SetupSequence(_ => _.MoveNextAsync(It.IsAny<System.Threading.CancellationToken>()))
                      .ReturnsAsync(true)
                      .ReturnsAsync(false);

            // Mock della collection
            var mockCollection = new Mock<IMongoCollection<Test>>();
            mockCollection.Setup(c => c.FindSync(
                It.IsAny<FilterDefinition<Test>>(),
                It.IsAny<FindOptions<Test, Test>>(),
                It.IsAny<System.Threading.CancellationToken>())
            ).Returns(mockCursor.Object);

            // Mock del servizio
            var mockService = new Mock<IMongoDBService>();
            mockService.Setup(s => s.Tests).Returns(mockCollection.Object);

            // Controller con mock
            var controller = new TestController(mockService.Object);

            // Esecuzione del metodo
            var result = controller.GetAllTests();
            var okResult = Assert.IsType<OkObjectResult>(result);
            var tests = Assert.IsAssignableFrom<IEnumerable<Test>>(okResult.Value);

            Assert.Equal(2, tests.Count());
            Assert.Equal("Test 1", tests.First().Title);
        }

        [Fact]
        public void CreateTest_ShouldCallInsertOneOnCollection()
        {
            var test = new Test { Title = "Nuovo Test" };

            // Mock della collection
            var mockCollection = new Mock<IMongoCollection<Test>>();
            mockCollection.Setup(c => c.InsertOne(
                It.IsAny<Test>(),
                It.IsAny<InsertOneOptions>(),
                It.IsAny<System.Threading.CancellationToken>())
            );

            // Mock del servizio
            var mockService = new Mock<IMongoDBService>();
            mockService.Setup(s => s.Tests).Returns(mockCollection.Object);

            var controller = new TestController(mockService.Object);

            // Esecuzione del metodo
            var result = controller.CreateTest(test);
            var okResult = Assert.IsType<OkObjectResult>(result);

            // Verifica che InsertOne sia stato chiamato
            mockCollection.Verify(c => c.InsertOne(test, null, default), Times.Once);
        }
    }
}
