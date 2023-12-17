using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PROYECTOFINALPW.Server.Models;
using PROYECTOFINALPW.Shared;

namespace PROYECTOFINALPW.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AbogadoController : ControllerBase
    {
        private readonly ProyectofinalpwContext _ProyectoPWcontext;

        public AbogadoController(ProyectofinalpwContext proyectoPWcontext)
        {
            _ProyectoPWcontext = proyectoPWcontext;
        }

        [HttpGet("Lista")]

        public async Task<IActionResult> Listado()
        {
            var responseAPI = new ResponseAPI<List<AbogadoDTO>>();

            var ListaAbogado = new List<AbogadoDTO>();

            try
            {
                foreach (var item in await _ProyectoPWcontext.Abogados.ToListAsync())
                {
                    ListaAbogado.Add(new AbogadoDTO
                    {
                        Id = item.Id,
                        Nombre = item.Nombre
                    });
                }

                responseAPI.EsCorrecto = true;
                responseAPI.Valor = ListaAbogado;
            }
            catch (Exception ex)
            {
                responseAPI.EsCorrecto = false;
                responseAPI.Mensaje = ex.Message;
            }
            return Ok(responseAPI);
        }


        [HttpGet("Buscar/{id}")]

        public async Task<IActionResult> Buscar(int id)
        {
            var responseAPI = new ResponseAPI<AbogadoDTO>();

            var Abogado = new AbogadoDTO();

            try
            {

                var dbAbogado = await _ProyectoPWcontext.Abogados.FirstOrDefaultAsync(c => c.Id == id);

                if (dbAbogado != null)
                {
                    Abogado.Id = dbAbogado.Id;
                    Abogado.Nombre = dbAbogado.Nombre;

                    responseAPI.EsCorrecto = true;
                    responseAPI.Valor = Abogado;
                }
                else
                {
                    responseAPI.EsCorrecto = false;
                    responseAPI.Mensaje = "No encontrado";
                }

            }
            catch (Exception ex)
            {
                responseAPI.EsCorrecto = false;
                responseAPI.Mensaje = ex.Message;
            }

            return Ok(responseAPI);
        }


        [HttpPost("Agregar")]

        public async Task<IActionResult> Agregar(AbogadoDTO abogado)
        {
            var responseAPI = new ResponseAPI<int>();

            try
            {

                var dbAbogado = new Abogado
                {
                    Nombre = abogado.Nombre            
                };

                _ProyectoPWcontext.Add(dbAbogado);
                await _ProyectoPWcontext.SaveChangesAsync();

                if (dbAbogado.Id != 0)
                {
                    responseAPI.EsCorrecto = true;
                    responseAPI.Valor = dbAbogado.Id;
                }
                else
                {
                    responseAPI.EsCorrecto = false;
                    responseAPI.Mensaje = "No guardado";
                }



            }
            catch (Exception ex)
            {
                responseAPI.EsCorrecto = false;
                responseAPI.Mensaje = ex.Message;
            }

            return Ok(responseAPI);
        }


        [HttpPut("Editar/{id}")]

        public async Task<IActionResult> Editar(AbogadoDTO abogado, int id)
        {
            var responseAPI = new ResponseAPI<int>();

            try
            {

                var dbAbogado = await _ProyectoPWcontext.Abogados.FirstOrDefaultAsync(c => c.Id == id);

                if (dbAbogado != null)
                {
                    dbAbogado.Nombre = abogado.Nombre;

                    _ProyectoPWcontext.Update(dbAbogado);
                    await _ProyectoPWcontext.SaveChangesAsync();

                    responseAPI.EsCorrecto = true;
                    responseAPI.Valor = dbAbogado.Id;

                }
                else
                {
                    responseAPI.EsCorrecto = false;
                    responseAPI.Mensaje = "Abogado no encontrado";
                }



            }
            catch (Exception ex)
            {
                responseAPI.EsCorrecto = false;
                responseAPI.Mensaje = ex.Message;
            }

            return Ok(responseAPI);
        }

        [HttpDelete("Eliminar/{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var responseAPI = new ResponseAPI<int>();

            try
            {
                var dbAbogado = await _ProyectoPWcontext.Abogados.FirstOrDefaultAsync(c => c.Id == id);

                if (dbAbogado != null)
                {

                    _ProyectoPWcontext.Remove(dbAbogado);
                    await _ProyectoPWcontext.SaveChangesAsync();

                    responseAPI.EsCorrecto = true;

                }
                else
                {
                    responseAPI.EsCorrecto = false;
                    responseAPI.Mensaje = "Abogado no encontrado";
                }
            }
            catch (Exception ex)
            {

                responseAPI.EsCorrecto = false;
                responseAPI.Mensaje = ex.Message;
            }

            return Ok(responseAPI);
        }
    }
}
