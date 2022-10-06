using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using PeliculasAPI.DTOs;
using PeliculasAPI.Entidades;
using PeliculasAPI.Helpers;
using PeliculasAPI.Servicios.Interfaces;

namespace PeliculasAPI.Servicios
{
    public class SalasDeCineServicios : ControllerBase, ISalasDeCineServicios
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly GeometryFactory geometryFactory;
        private readonly IActionContextAccessor actionContextAccessor;

        public SalasDeCineServicios(ApplicationDbContext context,
            IMapper mapper, GeometryFactory geometryFactory, IActionContextAccessor actionContextAccessor)
        {
            this.context = context;
            this.mapper = mapper;
            this.geometryFactory = geometryFactory;
            this.actionContextAccessor = actionContextAccessor;
        }

        public async Task<ActionResult<List<SalaDeCineCercanoDTO>>> Cercanos(
           [FromQuery] SalaDeCineCercanoFiltroDTO filtro)
        {
            var ubicacionUsuario = geometryFactory.CreatePoint(new Coordinate(filtro.Longitud, filtro.Latitud));

            var salasDeCine = await context.SalasDeCine
                .OrderBy(x => x.Ubicacion.Distance(ubicacionUsuario))
                .Where(x => x.Ubicacion.IsWithinDistance(ubicacionUsuario, filtro.DistanciaEnKms * 1000))
                .Select(x => new SalaDeCineCercanoDTO
                {
                    Id = x.Id,
                    Nombre = x.Nombre,
                    Latitud = x.Ubicacion.Y,
                    Longitud = x.Ubicacion.X,
                    DistanciaEnMetros = Math.Round(x.Ubicacion.Distance(ubicacionUsuario))
                }).ToListAsync();

            return salasDeCine;
        }


        public async Task<ActionResult<List<SalaDeCineDTO>>> Get(BaseFilter baseFilter)
        {
            var salasDeCine = context.SalasDeCine.AsQueryable();
            return await salasDeCine.FilterSortPaginate<SalaDeCine, SalaDeCineDTO>(baseFilter, mapper, actionContextAccessor);
        }


    }
}
