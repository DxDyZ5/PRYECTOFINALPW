using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PROYECTOFINALPW.Server.Models;
using PROYECTOFINALPW.Shared;

namespace PROYECTOFINALPW.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CasoController : ControllerBase
    {
        private readonly ProyectofinalpwContext _ProyectoPWcontext;

        public CasoController(ProyectofinalpwContext proyectoPWcontext)
        {
            _ProyectoPWcontext = proyectoPWcontext;
        }

        [HttpGet("Lista")]

        public async Task<IActionResult> Listado()
        {
            var responseAPI = new ResponseAPI<List<CasoDTO>>();

            var ListaCaso = new List<CasoDTO>();

            try
            {
                foreach (var item in await _ProyectoPWcontext.Casos.Include(C => C.Cliente).Include(T => T.Tipo).Include(A => A.Abogado).Include(E => E.Estado).ToListAsync())
                {
                    ListaCaso.Add(new CasoDTO
                    {
                        Id = item.Id,
                        Fecha = item.Fecha,
                        ClienteId = item.ClienteId,
                        TipoId = item.TipoId,
                        Latitud = item.Latitud,
                        Longitud = item.Longitud,
                        Descripcion = item.Descripcion,
                        AbogadoId = item.AbogadoId,
                        EstadoId = item.EstadoId,
                        Cliente = new ClienteDTO
                        {
                            Id = item.ClienteId,
                            Nombre = item.Cliente.Nombre,
                            Apellido = item.Cliente.Apellido,
                            Cedula = item.Cliente.Cedula,
                            Correo = item.Cliente.Correo,
                            Telefono = item.Cliente.Telefono,
                            Celular = item.Cliente.Celular,
                            Direccion = item.Cliente.Direccion,
                            EstadoCivil =  item.Cliente.EstadoCivil
                        },
                        Tipo = new TipoCasoDTO
                        {
                            Id = item.Tipo.Id,
                            Tipo = item.Tipo.Tipo
                        },
                        Abogado = new AbogadoDTO 
                        {

                            Id = item.Abogado.Id,
                            Nombre = item.Abogado.Nombre
                        },
                        Estado = new EstadoCasoDTO
                        {
                            Id = item.Estado.Id,
                            Estado = item.Estado.Estado
                        }
                    });
                }

                responseAPI.EsCorrecto = true;
                responseAPI.Valor = ListaCaso;
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
            var responseAPI = new ResponseAPI<CasoDTO>();

            var Caso = new CasoDTO();

            try
            {

                var dbCaso = await _ProyectoPWcontext.Casos.FirstOrDefaultAsync(c => c.Id == id);

                if (dbCaso != null)
                {
                    Caso.Id = dbCaso.Id;
                    Caso.Fecha = dbCaso.Fecha;
                    Caso.ClienteId = dbCaso.ClienteId;
                    Caso.TipoId = dbCaso.TipoId;
                    Caso.Latitud = dbCaso.Latitud;
                    Caso.Longitud = dbCaso.Longitud;
                    Caso.Descripcion = dbCaso.Descripcion;
                    Caso.AbogadoId = dbCaso.AbogadoId;
                    Caso.EstadoId = dbCaso.EstadoId;

                    responseAPI.EsCorrecto = true;
                    responseAPI.Valor = Caso;
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

        public async Task<IActionResult> Agregar(CasoDTO caso)
        {
            var responseAPI = new ResponseAPI<int>();

            try
            {

                var dbCaso = new Caso
                {
                   Fecha = caso.Fecha,
                   ClienteId = caso.ClienteId,
                   TipoId = caso.TipoId,
                   Latitud = caso.Latitud,
                   Longitud = caso.Longitud,
                   Descripcion = caso.Descripcion,
                   AbogadoId = caso.AbogadoId,
                   EstadoId = caso.EstadoId,
                };

                _ProyectoPWcontext.Add(dbCaso);
                await _ProyectoPWcontext.SaveChangesAsync();

                if(dbCaso.Id != 0)
                {
                    responseAPI.EsCorrecto = true;
                    responseAPI.Valor = dbCaso.Id;
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

        public async Task<IActionResult> Editar(CasoDTO caso, int id)
        {
            var responseAPI = new ResponseAPI<int>();

            try
            {

                var dbCaso = await _ProyectoPWcontext.Casos.FirstOrDefaultAsync( c => c.Id == id);

                if (dbCaso != null)
                {
                    dbCaso.Fecha = caso.Fecha;
                    dbCaso.ClienteId = caso.ClienteId;
                    dbCaso.TipoId = caso.TipoId;
                    dbCaso.Latitud = caso.Latitud;
                    dbCaso.Longitud = caso.Longitud;
                    dbCaso.Descripcion = caso.Descripcion;
                    dbCaso.AbogadoId = caso.AbogadoId;
                    dbCaso.EstadoId = caso.EstadoId;

                    _ProyectoPWcontext.Update(dbCaso);
                    await _ProyectoPWcontext.SaveChangesAsync();

                    responseAPI.EsCorrecto = true;
                    responseAPI.Valor = dbCaso.Id;


                }
                else
                {
                    responseAPI.EsCorrecto = false;
                    responseAPI.Mensaje = "Caso no encontrado";
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
//Usuario no es permitido eliminar se mantiene como historial por lo tanto no lo creamos, ni siquiera al admin, si se quiere eliminar tendra que ser en la base de datos por politicas juridiciales.

