using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeliculasAPI.DTOs;
using PeliculasAPI.Entidades;
using PeliculasAPI.Helpers;
using PeliculasAPI.Servicios;
using PeliculasAPI.Servicios.Interfaces;
using System.Security.Claims;

namespace PeliculasAPI.Controllers
{
    [Route("api/peliculas/{peliculaId:int}/review")]
    [ServiceFilter(typeof(PeliculaExisteAttribute))]
    public class ReviewController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly IReviewServices reviewServices;

        public ReviewController(ApplicationDbContext context,
            IMapper mapper, IReviewServices reviewServices, ICustomBaseControllerServices customBaseControllerServices)
        {
            this.context = context;
            this.mapper = mapper;
            this.reviewServices = reviewServices;
        }

        [HttpGet]
        public async Task<ActionResult<List<ReviewDTO>>> Get([FromQuery] BaseFilter baseFilter)
        {

            return await reviewServices.Get(baseFilter);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Post(int peliculaId, [FromBody] ReviewCreacionDTO reviewCreacionDTO)
        {
            return await reviewServices.Post(peliculaId, reviewCreacionDTO, HttpContext);
        }

        [HttpPut("{reviewId:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Put(int peliculaId, int reviewId,
            [FromBody] ReviewCreacionDTO reviewCreacionDTO)
        {
            return await reviewServices.Put(peliculaId, reviewId, reviewCreacionDTO);
        }

        [HttpDelete("{reviewId:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Delete(int reviewId)
        {
            return await reviewServices.Delete(reviewId);
        }
    }
}