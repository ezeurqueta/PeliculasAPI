using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using PeliculasAPI.DTOs;
using PeliculasAPI.Entidades;
using PeliculasAPI.Servicios;
using PeliculasAPI.Servicios.Interfaces;

namespace PeliculasAPI.Controllers
{
    [Route("api/SalasDeCine")]
    [ApiController]
    public class SalasDeCineController : CustomBaseControllerServices
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly GeometryFactory geometryFactory;
        private readonly ISalasDeCineServicios salasDeCineServicios;
        private readonly CustomBaseControllerServices customBaseControllerServices;

        public SalasDeCineController(ApplicationDbContext context,
            IMapper mapper, GeometryFactory geometryFactory, ISalasDeCineServicios salasDeCineServicios, CustomBaseControllerServices customBaseControllerServices )
            : base(context, mapper)
        {
            this.context = context;
            this.mapper = mapper;
            this.geometryFactory = geometryFactory;
            this.salasDeCineServicios = salasDeCineServicios;
            this.customBaseControllerServices = customBaseControllerServices;
        }

        [HttpGet]
        public async Task<ActionResult<List<SalaDeCineDTO>>> Get([FromQuery] BaseFilter baseFilter)
        {
            return await customBaseControllerServices.Get<SalaDeCine, SalaDeCineDTO>();
        }

        [HttpGet("{id:int}", Name = "obtenerSalaDeCine")]
        public async Task<ActionResult<SalaDeCineDTO>> Get(int id)
        {
            return await customBaseControllerServices.Get<SalaDeCine, SalaDeCineDTO>(id);
        }



        [HttpGet("Cercanos")]
        public async Task<ActionResult<List<SalaDeCineCercanoDTO>>> Cercanos(
           [FromQuery] SalaDeCineCercanoFiltroDTO filtro)
        {
            return await salasDeCineServicios.Cercanos(filtro);
        }



        [HttpPost]
        public async Task<ActionResult> Post([FromBody] SalaDeCineCreacionDTO salaDeCineCreacionDTO)
        {
            return await customBaseControllerServices.Post<SalaDeCineCreacionDTO, SalaDeCine, SalaDeCineDTO>(salaDeCineCreacionDTO, "obtenerSalaDeCine");
        } 

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] SalaDeCineCreacionDTO salaDeCineCreacionDTO)
        {
            return await customBaseControllerServices.Put<SalaDeCineCreacionDTO, SalaDeCine>(id, salaDeCineCreacionDTO);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            return await customBaseControllerServices.Delete<SalaDeCine>(id);
        }
    }
}