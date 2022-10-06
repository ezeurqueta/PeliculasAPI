using Microsoft.AspNetCore.Mvc;
using PeliculasAPI.DTOs;

namespace PeliculasAPI.Servicios.Interfaces
{
    public interface ISalasDeCineServicios
    {
        Task<ActionResult<List<SalaDeCineCercanoDTO>>> Cercanos(
           [FromQuery] SalaDeCineCercanoFiltroDTO filtro);
    }
}
