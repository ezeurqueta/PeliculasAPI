using Newtonsoft.Json;
using PeliculasAPI.DTOs;
using PeliculasAPI.Entidades;
using System.Net.Http.Json;

namespace PeliculasAPI.Tests.IntegrationTests
{
    [TestClass]
    public class SalasDeCineTests : BasePruebas<Program>
    {
        private static readonly string url = "/api/SalasDeCine";
        private int salaCineId = 200;
        [TestMethod]
        public async Task Get_All_Actors_Should_Return_200()
        {
            var factory = BuildWebApplicationFactory(Guid.NewGuid().ToString());
            var client = factory.CreateClient();
            var response = await client.GetAsync(url);

            response.EnsureSuccessStatusCode();

            var contentString = await response.Content.ReadAsStringAsync();
            var content = JsonConvert.DeserializeObject<List<SalaDeCineDTO>>(contentString);

            Assert.IsNotNull(content);
        }

        [TestMethod]
        public async Task Get_should_return_200_and_bring_the_requested_actor()
        {
            var factory = BuildWebApplicationFactory(Guid.NewGuid().ToString());
            var client = factory.CreateClient();
            var response = await client.GetAsync($"{url}/{salaCineId}");
            response.EnsureSuccessStatusCode();

            var contentString = await response.Content.ReadAsStringAsync();
            var requestedSalaCine = JsonConvert.DeserializeObject<SalaDeCineDTO>(contentString);

            Assert.AreEqual(requestedSalaCine.Id, salaCineId);
        }

        [TestMethod]
        public async Task CreateReview_ShouldReturn200()
        {
            var factory = BuildWebApplicationFactory(Guid.NewGuid().ToString());
            var client = factory.CreateClient();

            var newSalaCineCreator = new SalaDeCineCreacionDTO()
            {
                Nombre = "hoytz"
            };

            var response = await client.PostAsJsonAsync(url, newSalaCineCreator);
            response.EnsureSuccessStatusCode();
            var dataAsString = await response.Content.ReadAsStringAsync();
            var newSalaCineRequest = JsonConvert.DeserializeObject<SalaDeCineDTO>(dataAsString);

            Assert.AreEqual(newSalaCineRequest.Nombre, newSalaCineCreator.Nombre);
        }
        [TestMethod]
        public async Task DeleteSalaCineId_ShouldReturnNoContent()
        {
            var factory = BuildWebApplicationFactory(Guid.NewGuid().ToString());
            var client = factory.CreateClient();
            var response = await client.DeleteAsync($"{url}/{salaCineId}");
            response.EnsureSuccessStatusCode();
        }
    }
}
