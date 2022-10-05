using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using PeliculasAPI.DTOs;

namespace PeliculasAPI.Servicios.Interfaces
{
    public interface IActoresServices
    {
        Task<ActionResult<List<ActorDTO>>> Get(PaginacionDTO paginacionDTO);
        Task<ActionResult<ActorDTO>> Get(int id); //get por id
       Task<ActionResult> Post(ActorCreacionDTO actorCreacionDTO);
        Task<ActionResult> Put(int id, ActorCreacionDTO actorCreacionDTO);
        Task<ActionResult> Patch(int id,JsonPatchDocument<ActorPatchDTO> patchDocument);
        Task<ActionResult> Delete(int id);
    }
}
