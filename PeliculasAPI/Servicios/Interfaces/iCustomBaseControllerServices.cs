using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using PeliculasAPI.DTOs;
using PeliculasAPI.Entidades;

namespace PeliculasAPI.Servicios.Interfaces
{
    public interface iCustomBaseControllerServices
    {
        Task<List<TDTO>> Get<TEntidad, TDTO>() where TEntidad : class;
        Task<List<TDTO>> Get<TEntidad, TDTO>(PaginacionDTO paginacionDTO, IQueryable<TEntidad> queryable) where TEntidad : class;
        Task<ActionResult<TDTO>> Get<TEntidad, TDTO>(int id) where TEntidad : class, IId;
        Task<ActionResult> Delete<TEntidad>(int id) where TEntidad : class, IId, new();
        Task<ActionResult> Patch<TEntidad, TDTO>(int id, JsonPatchDocument<TDTO> patchDocument) where TDTO : class
        where TEntidad : class, IId;
    }
}



