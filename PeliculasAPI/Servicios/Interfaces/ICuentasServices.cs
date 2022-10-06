using Microsoft.AspNetCore.Mvc;
using PeliculasAPI.DTOs;

namespace PeliculasAPI.Servicios.Interfaces
{
    public interface ICuentasServices
    {
        Task<ActionResult<UserToken>> CreateUser([FromBody] UserInfo model);
        Task<ActionResult<UserToken>> Login([FromBody] UserInfo model);
        Task<ActionResult<UserToken>> Renovar();
        Task<UserToken> ConstruirToken(UserInfo userInfo);
        Task<ActionResult<List<string>>> GetRoles();
        Task<ActionResult> AsignarRol(EditarRolDTO editarRolDTO);
        Task<ActionResult> RemoverRol(EditarRolDTO editarRolDTO);
    }
}
