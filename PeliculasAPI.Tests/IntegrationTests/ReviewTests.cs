using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using PeliculasAPI.DTOs;
using System.Net.Http.Json;

namespace PeliculasAPI.Tests.IntegrationTests
{
    [TestClass]
    public class ReviewTests : BasePruebas<Program>
    {
        private int peliculaId = 300;
        private static readonly string url = $"api/peliculas/{peliculaId}/review";
        private int reviewId = 400;
        [TestMethod]
        public async Task Get_All_Reviews_Should_Return_200()
        {
            var factory = BuildWebApplicationFactory(Guid.NewGuid().ToString());
            var client = factory.CreateClient();
            var response = await client.GetAsync(url);

            response.EnsureSuccessStatusCode();

            var contentString = await response.Content.ReadAsStringAsync();
            var content = JsonConvert.DeserializeObject<List<ReviewDTO>>(contentString);

            Assert.IsNotNull(content);
        }

        [TestMethod]
        public async Task Get_should_return_200_and_bring_the_requested_review()
        {
            var factory = BuildWebApplicationFactory(Guid.NewGuid().ToString());
            var client = factory.CreateClient();
            var response = await client.GetAsync($"{url}/{reviewId}");
            response.EnsureSuccessStatusCode();

            var contentString = await response.Content.ReadAsStringAsync();
            var requestedReview = JsonConvert.DeserializeObject<ReviewDTO>(contentString);

            Assert.AreEqual(requestedReview.Id, reviewId);
        }

        [TestMethod]
        public async Task CreateReview_ShouldReturn200()
        {
            var factory = BuildWebApplicationFactory(Guid.NewGuid().ToString());
            var client = factory.CreateClient();

            var newReviewCreator = new ReviewCreacionDTO()
            {
                Comentario = "Excelente"
            };

            var response = await client.PostAsJsonAsync(url, newReviewCreator);
            response.EnsureSuccessStatusCode();
            var dataAsString = await response.Content.ReadAsStringAsync();
            var newReviewRequest = JsonConvert.DeserializeObject<ReviewDTO>(dataAsString);

            Assert.AreEqual(newReviewRequest.Comentario, newReviewCreator.Comentario);
        }

        [TestMethod]
        public async Task DeleteReviewId_ShouldReturnNoContent()
        {
            var factory = BuildWebApplicationFactory(Guid.NewGuid().ToString());
            var client = factory.CreateClient();
            var response = await client.DeleteAsync($"{url}/{reviewId}");
            response.EnsureSuccessStatusCode();
        }
    }
}
