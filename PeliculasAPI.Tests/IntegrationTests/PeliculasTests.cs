﻿using Newtonsoft.Json;
using PeliculasAPI.DTOs;
using System.Net.Http.Json;

namespace PeliculasAPI.Tests.IntegrationTests
{
    public class PeliculasTests : BasePruebas<Program>
    {
        private static readonly string url = "/api/peliculas";
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
            var peliculaId = 3;
            var response = await client.GetAsync($"{url}/{peliculaId}");
            response.EnsureSuccessStatusCode();

            var contentString = await response.Content.ReadAsStringAsync();
            var requestedPelicula = JsonConvert.DeserializeObject<ReviewDTO>(contentString);

            Assert.AreEqual(requestedPelicula.Id, peliculaId);
        }

        [TestMethod]
        public async Task CreatePelicula_ShouldReturn200()
        {
            var factory = BuildWebApplicationFactory(Guid.NewGuid().ToString());
            var client = factory.CreateClient();

            var newPeliculaCreator = new PeliculaCreacionDTO()
            {
                Titulo = "Harrypotter"
            };

            var response = await client.PostAsJsonAsync($"{url}/new-actor", newPeliculaCreator);
            response.EnsureSuccessStatusCode();
            var dataAsString = await response.Content.ReadAsStringAsync();
            var newpeliculaRequest = JsonConvert.DeserializeObject<PeliculaCreacionDTO>(dataAsString);

            Assert.AreEqual(newpeliculaRequest.Titulo, newPeliculaCreator.Titulo);

        }

    }
}
