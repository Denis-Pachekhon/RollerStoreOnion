using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RollerStore.Core.Roller;
using RollerStoreOnion.Controllers;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace RollerStore.Api.Unit.Tests.Roller
{
    public class RollersControllerTests
    {
        private readonly RollersController _controller;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IRollerService> _rollerServiceMock;

        public RollersControllerTests()
        {
            _mapperMock = new Mock<IMapper>();
            _rollerServiceMock = new Mock<IRollerService>();

            _controller = new RollersController(_mapperMock.Object, _rollerServiceMock.Object);
        }
        [Fact]
        public async Task UpdatePriceAsync_IfServiceReturnRoller_ReturnsResponse()
        {
            //Arrange
            int rollerId = 1;

            var newRollerPrice = new global::RollerStore.Orschestrators.Roller.Contracts.UpdateRollerPrice
            { 
                Price = 5000
            };


            var rollerFromService = new global::RollerStore.Core.Roller.Roller
            {
                Id = rollerId,
                StoreId = 1,
                Name = "rollerblade 5000",
                Price = newRollerPrice.Price
            };

            var rollerAfterMapping = new global::RollerStore.Orschestrators.Roller.Contracts.Roller
            { 
                Id = rollerFromService.Id,
                StoreId = rollerFromService.StoreId,
                Name = rollerFromService.Name,
                Price = rollerFromService.Price
            };


            _rollerServiceMock.Setup(r => r.UpdatePriceAsync(rollerId, newRollerPrice.Price))
                .ReturnsAsync(rollerFromService);

            _mapperMock.Setup(m => m.Map<global::RollerStore.Orschestrators.Roller.Contracts.Roller>(rollerFromService))
                .Returns(rollerAfterMapping);

            //Act
            var result = await _controller.UpdatePriceAsync(rollerId, newRollerPrice) as OkObjectResult;

            //Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be((int)HttpStatusCode.OK);
            result.Value.Should().Be(rollerAfterMapping);
        }
    }
}
