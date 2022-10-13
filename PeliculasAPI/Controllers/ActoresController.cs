using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeliculasAPI.DTOs;
using PeliculasAPI.Entidades;
using PeliculasAPI.Helpers;
using PeliculasAPI.Servicios;
using PeliculasAPI.Servicios.Interfaces;

namespace PeliculasAPI.Controllers
{
    [ApiController]
    [Route("api/actores")]
    public class ActoresController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly IAlmacenadorArchivos almacenadorArchivos;
        private readonly IActoresServices actoresServices;
        private readonly ICustomBaseControllerServices customBaseControllerServices;

        public ActoresController(ApplicationDbContext context, IMapper mapper, IAlmacenadorArchivos almacenadorArchivos,IActoresServices actoresServices, ICustomBaseControllerServices customBaseControllerServices)
        {
            this.context = context;
            this.mapper = mapper;
            this.almacenadorArchivos = almacenadorArchivos;
            this.actoresServices = actoresServices;
            this.customBaseControllerServices = customBaseControllerServices;
        }

        [HttpGet]
        public async Task<ActionResult<List<ActorDTO>>> Get([FromQuery] BaseFilter filter)
        {
            return await customBaseControllerServices.Get<Actor, ActorDTO>();
        }

        [HttpGet("{id}", Name = "obtenerActor")]
        public async Task<ActionResult<ActorDTO>> Get(int id)
        {
            return await customBaseControllerServices.Get<Actor, ActorDTO>(id);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromForm] ActorCreacionDTO actorCreacionDTO)
        {
            return await actoresServices.Post(actorCreacionDTO);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromForm] ActorCreacionDTO actorCreacionDTO)
        {
            return await actoresServices.Put(id, actorCreacionDTO);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> Patch(int id, [FromBody] JsonPatchDocument<ActorPatchDTO> patchDocument)
        {
            return await customBaseControllerServices.Patch<Actor, ActorPatchDTO>(id, patchDocument);

        }



        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            return await customBaseControllerServices.Delete<Actor>(id);
        }
    }
}
