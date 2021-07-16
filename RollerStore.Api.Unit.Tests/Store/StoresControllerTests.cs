using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RollerStore.Core.Store;
using RollerStoreOnion.Controllers;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace RollerStore.Api.Unit.Tests.Store
{
    public class StoresControllerTests
    {
        private readonly StoresController _controller;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IStoreService> _storeServiceMock;

        public StoresControllerTests()
        {
            _mapperMock = new Mock<IMapper>();
            _storeServiceMock = new Mock<IStoreService>();

            _controller = new StoresController(_mapperMock.Object, _storeServiceMock.Object);
        }
        [Fact]
        public async Task PostAsync_IfServiceReturnStore_ReturnsResponse()
        {
            //Arrange
            var store = new global::RollerStore.Orschestrators.Store.Contracts.CreateStore
            {
                Name = "Rollerland",
                Address = "adress"
            };

            var storeForService = new global::RollerStore.Core.Store.Store
            { 
                Name = store.Name,
                Address = store.Address
            };

            var storeFromService = new global::RollerStore.Core.Store.Store
            {
                Id = 2,
                Name = storeForService.Name,
                Address = storeForService.Address
            };

            var storeAfterMapping = new global::RollerStore.Orschestrators.Store.Contracts.Store
            {
                Id = storeFromService.Id,
                Name = storeFromService.Name,
                Address = storeFromService.Address
            };

            _mapperMock.Setup(m => m.Map<global::RollerStore.Core.Store.Store>(store))
                .Returns(storeForService);

            _storeServiceMock.Setup(st => st.AddAsync(storeForService))
                .ReturnsAsync(storeFromService);

            _mapperMock.Setup(m => m.Map<global::RollerStore.Orschestrators.Store.Contracts.Store>(storeFromService))
                .Returns(storeAfterMapping);

            //Act
            var result = await _controller.PostAsync(store) as OkObjectResult;

            //Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be((int)HttpStatusCode.OK);
            result.Value.Should().Be(storeAfterMapping);
        }
    }
}
