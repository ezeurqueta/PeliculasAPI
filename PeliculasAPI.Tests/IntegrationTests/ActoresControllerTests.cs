using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using PeliculasAPI.DTOs;
using PeliculasAPI.Entidades;
using System;
using System.Net.Http.Json;

namespace PeliculasAPI.Tests.IntegrationTests
{
    [TestClass]
    public class ActoresControllerTests : BasePruebas<Program>
    {
        private static readonly string url = "/api/actores";



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
            var actorId = 1;
            var response = await client.GetAsync($"{url}/{actorId}");
            response.EnsureSuccessStatusCode();

            var contentString = await response.Content.ReadAsStringAsync();
            var requestedActor = JsonConvert.DeserializeObject<ActorDTO>(contentString);

            Assert.AreEqual(requestedActor.Id, actorId);
        }

        [TestMethod]
        public async Task CreateActor_ShouldReturn200()
        {
            var factory = BuildWebApplicationFactory(Guid.NewGuid().ToString());
            var client = factory.CreateClient();

            var newActorCreator = new ActorCreacionDTO()
            {
                 Nombre ="Ezequiel"
            };

            var response = await client.PostAsJsonAsync($"{url}/new-actor", newActorCreator);
            response.EnsureSuccessStatusCode();
            var dataAsString = await response.Content.ReadAsStringAsync();
            var newActorRequest = JsonConvert.DeserializeObject<ActorCreacionDTO>(dataAsString);

            Assert.AreEqual(newActorRequest.Nombre, newActorCreator.Nombre);

        }


    }
}
