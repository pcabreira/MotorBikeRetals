using Moq;
using MotorBikeRetals.Application.Queries.GetAllBikes;
using MotorBikeRetals.Application.Queries.GetAllPlans;
using MotorBikeRetals.Core.Entities;
using MotorBikeRetals.Core.Repositories;

namespace MotorBikeRetals.UnitTests.Application.Queries
{
    public class GetAllBikesCommandHandlerTests
    {
        [Fact]
        public async Task ThreerentalsExist_Executed_ReturnThreeContractViewModels()
        {
            //Arrange
            var bikes = new List<Bike>
            {
                new Bike(DateTime.Now.Year, "Yamaha Fazer", "DOK2A21"),
                new Bike(DateTime.Now.Year, "Honda Biz", "FSR3B22"),
                new Bike(DateTime.Now.Year, "Suzuki Intruder", "EBO4C23")
            };

            var bikesRepositoryMock = new Mock<IBikeRepository>();
            bikesRepositoryMock.Setup(pr => pr.GetAllAsync().Result).Returns(bikes);

            var getAllBikesQuery = new GetAllBikesQuery();
            var getAllBikesQueryHandler = new GetAllBikesQueryHandler(bikesRepositoryMock.Object);

            //Act
            var bikeViewModelList = await getAllBikesQueryHandler.Handle(getAllBikesQuery, new CancellationToken());

            //Assert
            Assert.NotNull(bikeViewModelList);
            Assert.NotEmpty(bikeViewModelList);
            Assert.Equal(bikes.Count, bikeViewModelList.Count);

            bikesRepositoryMock.Verify(pr => pr.GetAllAsync().Result, Times.Once);
        }
    }
}
