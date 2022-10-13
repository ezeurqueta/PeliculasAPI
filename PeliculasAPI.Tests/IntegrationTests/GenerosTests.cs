using Newtonsoft.Json;
using PeliculasAPI.DTOs;
using System.Net.Http.Json;

namespace PeliculasAPI.Tests.IntegrationTests
{
    [TestClass]
    public class GenerosTests : BasePruebas<Program>
    {
        
        private static readonly string url = "/api/generos";
        [TestMethod]
        public async Task Get_All_Actors_Should_Return_200()
        {
            var factory = BuildWebApplicationFactory(Guid.NewGuid().ToString());
            var client = factory.CreateClient();
            var response = await client.GetAsync(url);

            response.EnsureSuccessStatusCode();

            var contentString = await response.Content.ReadAsStringAsync();
            var content = JsonConvert.DeserializeObject<List<ActorDTO>>(contentString);

            Assert.IsNotNull(content);
        }


        [TestMethod]
        public async Task Get_should_return_200_and_bring_the_requested_actor()
        {
            var factory = BuildWebApplicationFactory(Guid.NewGuid().ToString());
            var client = factory.CreateClient();
            var generoId = 2;
            var response = await client.GetAsync($"{url}/{generoId}");
            response.EnsureSuccessStatusCode();

            var contentString = await response.Content.ReadAsStringAsync();
            var requestedGenero = JsonConvert.DeserializeObject<GeneroDTO>(contentString);

            Assert.AreEqual(requestedGenero.Id, generoId);
        }

        [TestMethod]
        public async Task CreateGenero_ShouldReturn200()
        {
            var factory = BuildWebApplicationFactory(Guid.NewGuid().ToString());
            var client = factory.CreateClient();

            var newGeneroCreator = new GeneroCreacionDTO()
            {
                Nombre ="comedia"
            };

            var response = await client.PostAsJsonAsync($"{url}/new-actor", newGeneroCreator);
            response.EnsureSuccessStatusCode();
            var dataAsString = await response.Content.ReadAsStringAsync();
            var newgeneroRequest = JsonConvert.DeserializeObject<GeneroCreacionDTO>(dataAsString);

            Assert.AreEqual(newgeneroRequest.Nombre, newGeneroCreator.Nombre);

        }
    }
}
