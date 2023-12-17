using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PROYECTOFINALPW.Shared;

namespace PROYECTOFINALPW.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        [HttpPost]
        [Route("Login")]
        public IActionResult Login([FromBody] Login login)
        {
            Sesion sesion = new Sesion();

            if (login.Usuario == "adamix" && login.Clave == "123456")
            {
                sesion.Nombre = "Amadis Suarez Genao";
                sesion.Usuario = login.Usuario;
                sesion.Rol = "Administrador";
            }

            return StatusCode(StatusCodes.Status200OK, sesion);
        }
    }
}
