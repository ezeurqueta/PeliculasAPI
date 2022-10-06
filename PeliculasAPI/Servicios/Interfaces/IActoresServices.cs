using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using PeliculasAPI.DTOs;

namespace PeliculasAPI.Servicios.Interfaces
{
    public interface IActoresServices
    {
       Task<ActionResult> Post(ActorCreacionDTO actorCreacionDTO);
        Task<ActionResult> Put(int id, [FromForm] ActorCreacionDTO actorCreacionDTO);


    }
}
