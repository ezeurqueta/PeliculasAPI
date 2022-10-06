using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeliculasAPI.DTOs;
using PeliculasAPI.Entidades;
using PeliculasAPI.Helpers;
using System.Linq.Dynamic.Core;
using PeliculasAPI.Servicios;
using PeliculasAPI.Servicios.Interfaces;

namespace PeliculasAPI.Controllers
{
    [ApiController]
    [Route("api/peliculas")]
    public class PeliculasController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly IAlmacenadorArchivos almacenadorArchivos;
        private readonly ILogger<PeliculasController> logger;
        private readonly IPeliculasServices peliculasServices;
        private readonly CustomBaseControllerServices customBaseControllerServices;

        public PeliculasController(ApplicationDbContext context, IMapper mapper, IAlmacenadorArchivos almacenadorArchivos, ILogger<PeliculasController> logger, IPeliculasServices peliculasServices, CustomBaseControllerServices customBaseControllerServices )
        {
            this.context = context;
            this.mapper = mapper;
            this.almacenadorArchivos = almacenadorArchivos;
            this.logger = logger;
            this.peliculasServices = peliculasServices;
            this.customBaseControllerServices = customBaseControllerServices;
        } 

        [HttpGet]
        public async Task<ActionResult<List<PeliculaDTO>>> Get([FromQuery] BaseFilter baseFilter)
        {
            return await peliculasServices.Get(baseFilter);
        }



        [HttpGet("{id}", Name = "obtenerPelicula")]
        public async Task<ActionResult<PeliculaDetallesDTO>> Get(int id)
        {
            return await peliculasServices.Get(id);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromForm] PeliculaCreacionDTO peliculaCreacionDTO)
        {
            return await peliculasServices.Post(peliculaCreacionDTO);
        }



        [HttpPut("{id}")]

        public async Task<ActionResult> Put(int id, [FromForm] PeliculaCreacionDTO peliculaCreacionDTO)
        {
            return await peliculasServices.Put(id, peliculaCreacionDTO);
        }


        [HttpPatch("{id}")]
        public async Task<ActionResult> Patch(int id, [FromBody] JsonPatchDocument<PeliculaPatchDTO> patchDocument)
        {
            return await customBaseControllerServices.Patch<Pelicula, PeliculaPatchDTO>(id,patchDocument);

        }



        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            return await customBaseControllerServices.Delete<Pelicula>(id);
        }





    }
}
