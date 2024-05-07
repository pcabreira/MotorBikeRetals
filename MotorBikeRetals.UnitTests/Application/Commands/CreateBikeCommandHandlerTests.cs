using Microsoft.Extensions.Logging;
using Moq;
using MotorBikeRetals.Application.Commands.CreateBike;
using MotorBikeRetals.Core.Entities;
using MotorBikeRetals.Core.Repositories;
using MotorBikeRetals.Core.Services;

namespace MotorBikeRetals.UnitTests.Application.Commands
{
    public class CreateBikeCommandHandlerTests
    {
        [Fact]
        public async Task InputDataIsOk_Executed_ReturnOk()
        {
            // Arrange
            var bikeRepository = new Mock<IBikeRepository>();
            var bikeService = new Mock<IBikeService>();
            var logger = new Mock<ILogger<CreateBikeCommand>>();

            var createBikeCommand = new CreateBikeCommand
            {
                Year = DateTime.Now.Year,
                Model = "Honda Titan",
                Plate = "GRT8G12"
            };

            var createBikeCommandHandler = new CreateBikeCommandHandler(bikeRepository.Object, bikeService.Object, logger.Object);

            // Act
            var bike = await createBikeCommandHandler.Handle(createBikeCommand, new CancellationToken());

            // Assert
            bikeRepository.Verify(pr => pr.AddAsync(It.IsAny<Bike>()), Times.Once);
        }
    }
}
