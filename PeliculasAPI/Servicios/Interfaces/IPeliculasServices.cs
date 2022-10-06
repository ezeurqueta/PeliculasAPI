using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using PeliculasAPI.DTOs;

namespace PeliculasAPI.Servicios.Interfaces
{
    public interface IPeliculasServices
    {
        Task<ActionResult<PeliculaDetallesDTO>> Get(int id);
        Task<ActionResult> Post([FromForm] PeliculaCreacionDTO peliculaCreacionDTO);
        Task<ActionResult> Put(int id, [FromForm] PeliculaCreacionDTO peliculaCreacionDTO);
        Task<ActionResult<List<PeliculaDTO>>> Get(BaseFilter baseFilter);
    }
}
