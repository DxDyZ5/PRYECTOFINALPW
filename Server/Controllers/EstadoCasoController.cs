using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PROYECTOFINALPW.Server.Models;
using PROYECTOFINALPW.Shared;

namespace PROYECTOFINALPW.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstadoCasoController : ControllerBase
    {
        private readonly ProyectofinalpwContext _ProyectoPWcontext;

        public EstadoCasoController(ProyectofinalpwContext proyectoPWcontext)
        {
            _ProyectoPWcontext = proyectoPWcontext;
        }

        [HttpGet("Lista")]

        public async Task<IActionResult> Listado()
        {
            var responseAPI = new ResponseAPI<List<EstadoCasoDTO>>();

            var ListaEstado = new List<EstadoCasoDTO>();

            try
            {
                foreach (var item in await _ProyectoPWcontext.EstadoCasos.ToListAsync())
                {
                    ListaEstado.Add(new EstadoCasoDTO
                    {
                        Id = item.Id,
                        Estado = item.Estado
                    });
                }

                responseAPI.EsCorrecto = true;
                responseAPI.Valor = ListaEstado;
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
            var responseAPI = new ResponseAPI<EstadoCasoDTO>();

            var Estado = new EstadoCasoDTO();

            try
            {

                var dbEstado= await _ProyectoPWcontext.EstadoCasos.FirstOrDefaultAsync(c => c.Id == id);

                if (dbEstado != null)
                {
                    Estado.Id = dbEstado.Id;
                    Estado.Estado = dbEstado.Estado;

                    responseAPI.EsCorrecto = true;
                    responseAPI.Valor = Estado;
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

        public async Task<IActionResult> Agregar(EstadoCasoDTO estado)
        {
            var responseAPI = new ResponseAPI<int>();

            try
            {

                var dbEstado = new EstadoCaso 
                {
                    Estado = estado.Estado
                };

                _ProyectoPWcontext.Add(dbEstado);
                await _ProyectoPWcontext.SaveChangesAsync();

                if (dbEstado.Id != 0)
                {
                    responseAPI.EsCorrecto = true;
                    responseAPI.Valor = dbEstado.Id;
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

        public async Task<IActionResult> Editar(EstadoCasoDTO estado, int id)
        {
            var responseAPI = new ResponseAPI<int>();

            try
            {

                var dbEstado = await _ProyectoPWcontext.EstadoCasos.FirstOrDefaultAsync(c => c.Id == id);

                if (dbEstado != null)
                {
                    dbEstado.Estado = estado.Estado;

                    _ProyectoPWcontext.Update(dbEstado);
                    await _ProyectoPWcontext.SaveChangesAsync();

                    responseAPI.EsCorrecto = true;
                    responseAPI.Valor = dbEstado.Id;

                }
                else
                {
                    responseAPI.EsCorrecto = false;
                    responseAPI.Mensaje = "Estado de caso no encontrado";
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
                var dbEstado = await _ProyectoPWcontext.EstadoCasos.FirstOrDefaultAsync(c => c.Id == id);

                if (dbEstado != null)
                {

                    _ProyectoPWcontext.Remove(dbEstado);
                    await _ProyectoPWcontext.SaveChangesAsync();

                    responseAPI.EsCorrecto = true;

                }
                else
                {
                    responseAPI.EsCorrecto = false;
                    responseAPI.Mensaje = "Estado de caso no encontrado";
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
