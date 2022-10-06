using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeliculasAPI.DTOs;
using PeliculasAPI.Entidades;
using PeliculasAPI.Servicios;
using PeliculasAPI.Servicios.Interfaces;

namespace PeliculasAPI.Controllers
{
    [ApiController]
    [Route("api/generos")]
    public class GenerosController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly IGenerosServices generosServices;
        private readonly CustomBaseControllerServices customBaseControllerServices;

        public GenerosController(ApplicationDbContext context, IMapper mapper, IGenerosServices generosServices, CustomBaseControllerServices customBaseControllerServices   )
        {
            this.context = context;
            this.mapper = mapper;
            this.generosServices = generosServices;
            this.customBaseControllerServices = customBaseControllerServices;
        }

        [HttpGet]
        public async Task<ActionResult<List<GeneroDTO>>> Get([FromQuery] BaseFilter baseFilter)
        {
            return await generosServices.Get(baseFilter);
        }

        [HttpGet("{id:int}", Name ="obtenerGenero")]
        public async Task<ActionResult<GeneroDTO>> Get(int id)
        {
            return await customBaseControllerServices.Get<Genero, GeneroDTO>(id);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] GeneroCreacionDTO generoCreacionDTO)
        {
            return await customBaseControllerServices.Post<GeneroCreacionDTO, Genero, GeneroDTO>(generoCreacionDTO, "obtenerGenero");
        }


        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] GeneroCreacionDTO generoCreacionDTO)
        {
            return await customBaseControllerServices.Put<GeneroCreacionDTO, Genero>(id, generoCreacionDTO);
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            return await customBaseControllerServices.Delete<Genero>(id);
        }

    }
}
