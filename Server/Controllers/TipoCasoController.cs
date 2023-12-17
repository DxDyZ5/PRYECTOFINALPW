using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PROYECTOFINALPW.Server.Models;
using PROYECTOFINALPW.Shared;

namespace PROYECTOFINALPW.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoCasoController : ControllerBase
    {
        private readonly ProyectofinalpwContext _ProyectoPWcontext;

        public TipoCasoController(ProyectofinalpwContext proyectoPWcontext)
        {
            _ProyectoPWcontext = proyectoPWcontext;
        }

        [HttpGet("Lista")]

        public async Task<IActionResult> Listado()
        {
            var responseAPI = new ResponseAPI<List<TipoCasoDTO>>();

            var ListaTipo = new List<TipoCasoDTO>();

            try
            {
                foreach (var item in await _ProyectoPWcontext.TipoCasos.ToListAsync())
                {
                    ListaTipo.Add(new TipoCasoDTO
                    {
                        Id = item.Id,
                        Tipo = item.Tipo
                    });
                }

                responseAPI.EsCorrecto = true;
                responseAPI.Valor = ListaTipo;
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
            var responseAPI = new ResponseAPI<TipoCasoDTO>();

            var Tipo = new TipoCasoDTO();

            try
            {

                var dbTipo = await _ProyectoPWcontext.TipoCasos.FirstOrDefaultAsync(c => c.Id == id);

                if (dbTipo != null)
                {
                    Tipo.Id = dbTipo.Id;
                    Tipo.Tipo = dbTipo.Tipo;

                    responseAPI.EsCorrecto = true;
                    responseAPI.Valor = Tipo;
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

        public async Task<IActionResult> Agregar(TipoCasoDTO Tipo)
        {
            var responseAPI = new ResponseAPI<int>();

            try
            {

                var dbTipo = new TipoCaso
                {
                    Tipo = Tipo.Tipo
                };

                _ProyectoPWcontext.Add(dbTipo);
                await _ProyectoPWcontext.SaveChangesAsync();

                if (dbTipo.Id != 0)
                {
                    responseAPI.EsCorrecto = true;
                    responseAPI.Valor = dbTipo.Id;
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

        public async Task<IActionResult> Editar(TipoCasoDTO tipo, int id)
        {
            var responseAPI = new ResponseAPI<int>();

            try
            {

                var dbTipo = await _ProyectoPWcontext.TipoCasos.FirstOrDefaultAsync(c => c.Id == id);

                if (dbTipo != null)
                {
                    dbTipo.Tipo = tipo.Tipo;

                    _ProyectoPWcontext.Update(dbTipo);
                    await _ProyectoPWcontext.SaveChangesAsync();

                    responseAPI.EsCorrecto = true;
                    responseAPI.Valor = dbTipo.Id;

                }
                else
                {
                    responseAPI.EsCorrecto = false;
                    responseAPI.Mensaje = "Tipo de caso no encontrado";
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
                var dbTipo = await _ProyectoPWcontext.TipoCasos.FirstOrDefaultAsync(c => c.Id == id);

                if (dbTipo != null)
                {

                    _ProyectoPWcontext.Remove(dbTipo);
                    await _ProyectoPWcontext.SaveChangesAsync();

                    responseAPI.EsCorrecto = true;

                }
                else
                {
                    responseAPI.EsCorrecto = false;
                    responseAPI.Mensaje = "Tipo de caso no encontrado";
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
