using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using PeliculasAPI.DTOs;
using PeliculasAPI.Entidades;
using PeliculasAPI.Helpers;
using PeliculasAPI.Servicios.Interfaces;

namespace PeliculasAPI.Servicios
{
    public class GenerosServices : ControllerBase, IGenerosServices
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly IActionContextAccessor actionContextAccessor;

        public GenerosServices(ApplicationDbContext context, IMapper mapper, IActionContextAccessor actionContextAccessor)
        {
            this.context = context;
            this.mapper = mapper;
            this.actionContextAccessor = actionContextAccessor;
        }

        public async Task<ActionResult<List<GeneroDTO>>> Get(BaseFilter baseFilter)
        {
            var genero = context.Generos.AsQueryable();
            return await genero.FilterSortPaginate<Genero, GeneroDTO>(baseFilter, mapper, actionContextAccessor);

        }

    }
}
