using Microsoft.AspNetCore.Mvc;
using PeliculasAPI.DTOs;

namespace PeliculasAPI.Servicios.Interfaces
{
    public interface IReviewServices
    {
        Task<ActionResult<List<ReviewDTO>>> Get(BaseFilter baseFilter);
        Task<ActionResult> Put(int peliculaId, int reviewId, [FromBody] ReviewCreacionDTO reviewCreacionDTO);
        Task<ActionResult> Delete(int reviewId);
        public Task<ActionResult> Post(int peliculaId, [FromBody] ReviewCreacionDTO reviewCreacionDTO, HttpContext httpContext);
    }
}
