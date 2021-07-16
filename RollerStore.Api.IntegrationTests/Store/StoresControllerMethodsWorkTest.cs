using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using RollerStore.Data.DB;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace RollerStore.Api.IntegrationTests.Store
{
    public class StoresControllerMethodsWorkTest : 
        IClassFixture<CustomWebApplicationFactory<RollerStoreOnion.Startup>>
    {
        private readonly HttpClient _client;
        private readonly CustomWebApplicationFactory<RollerStoreOnion.Startup>
            _factory;

        public StoresControllerMethodsWorkTest(
            CustomWebApplicationFactory<RollerStoreOnion.Startup> factory)
        {
            _factory = factory;
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }

        [Fact]
        public async Task GetByIdAsync_IfWorks_ReturnStore()
        {
            // Arrange
            var addedStore = new global::RollerStore.Data.Store.StoreEntity
            { 
                Name = "rollerland",
                IsDeleted = false, 
                Address = "4a"
                
            };

            global::RollerStore.Data.Store.StoreEntity storeFromDB;
            using (var scope = _factory.serviceProvider.CreateScope())
            {
                var DB = scope.ServiceProvider.GetRequiredService<RollerStoreDbContext>();
                storeFromDB = DB.Stores.Add(addedStore).Entity;
                DB.SaveChanges();
            }
            var getRequest = new HttpRequestMessage(HttpMethod.Get, $"/api/RollerStore/v1/Stores/{storeFromDB.Id}");

            //Act

            var getResponse = await _client.SendAsync(getRequest);
            var store = await GetModelFromHttpResponce.GetSingle <global::RollerStore.Orschestrators.Store.Contracts.Store>(getResponse);

            // Assert
            getResponse.EnsureSuccessStatusCode();
            Assert.Equal(storeFromDB.Name, store.Name);
            Assert.Equal(storeFromDB.Address, store.Address);
        }
    }
}
