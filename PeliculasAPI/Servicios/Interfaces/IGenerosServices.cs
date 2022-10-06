using Microsoft.AspNetCore.Mvc;
using PeliculasAPI.DTOs;

namespace PeliculasAPI.Servicios.Interfaces
{
    public interface IGenerosServices
    {
        Task<ActionResult<List<GeneroDTO>>> Get(BaseFilter filter);

    }
}
