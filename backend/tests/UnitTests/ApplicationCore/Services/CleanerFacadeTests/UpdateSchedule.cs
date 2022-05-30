using Moq;
using PartyKlinest.ApplicationCore.Entities.Orders;
using PartyKlinest.ApplicationCore.Entities.Users;
using PartyKlinest.ApplicationCore.Entities.Users.Cleaners;
using PartyKlinest.ApplicationCore.Interfaces;
using PartyKlinest.ApplicationCore.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnitTests.Factories;
using Xunit;

namespace UnitTests.ApplicationCore.Services.CleanerFacadeTests
{
    public class UpdateSchedule
    {
        private readonly Mock<IRepository<Order>> _mockOrderRepo = new();
        private readonly Mock<IRepository<Cleaner>> _mockCleanerRepo = new();
        private readonly Mock<IRepository<Client>> _mockClientRepo = new();
        private readonly Mock<IClientService> _mockClientService = new();
        private readonly Mock<IGraphClient> _mockGraphClient = new();

        [Theory(DisplayName = "Make 1 Update: shedule")]
        [InlineData(CleanerStatus.Active, CleanerStatus.Active)]
        public async Task VerifyUpdateSchedule1(CleanerStatus localStatus, CleanerStatus sentStatus)
        {
            // Arrange
            var entries = new List<ScheduleEntry>();
            var cleanerBuilder = new CleanerBuilder();
            cleanerBuilder.WithStatus(localStatus);
            cleanerBuilder.WithSchedule(entries);
            Cleaner localCleaner = cleanerBuilder.Build();

            entries = new List<ScheduleEntry>()
            {
                new ScheduleEntry(TimeOnly.MinValue, TimeOnly.MaxValue, DayOfWeek.Tuesday),
            };
            cleanerBuilder = new CleanerBuilder();
            cleanerBuilder.WithStatus(sentStatus);
            cleanerBuilder.WithSchedule(entries);
            Cleaner sentCleaner = cleanerBuilder.Build();

            _mockCleanerRepo
                .Setup(x => x.GetByIdAsync(It.IsAny<string>(), default))
                .ReturnsAsync(localCleaner);
            OrderFacade orderFacade = new(_mockOrderRepo.Object, _mockClientRepo.Object);
            var cleanerFacade = new CleanerFacade(_mockCleanerRepo.Object, orderFacade, _mockClientService.Object, _mockGraphClient.Object);

            // Act
            await cleanerFacade.UpdateCleanerAsync(sentCleaner);

            // Assert
            _mockCleanerRepo.Verify(x => x.UpdateAsync(It.IsAny<Cleaner>(), default), Times.Once);
        }
    }
}
