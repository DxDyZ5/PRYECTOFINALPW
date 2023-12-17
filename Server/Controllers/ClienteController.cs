using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PROYECTOFINALPW.Server.Models;
using PROYECTOFINALPW.Shared;

namespace PROYECTOFINALPW.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly ProyectofinalpwContext _ProyectoPWcontext;

        public ClienteController(ProyectofinalpwContext proyectoPWcontext)
        {
            _ProyectoPWcontext = proyectoPWcontext;
        }

        [HttpGet("Lista")]
        public async Task<IActionResult> Listado()
        {
            var responseAPI = new ResponseAPI<List<ClienteDTO>>();

            var ListaCliente = new List<ClienteDTO>();

            try
            {
                foreach (var item in await _ProyectoPWcontext.Clientes.ToListAsync())
                {
                    ListaCliente.Add(new ClienteDTO
                    {
                        Id = item.Id,
                        Nombre = item.Nombre,
                        Apellido = item.Apellido,
                        Cedula = item.Cedula,
                        Correo = item.Correo,
                        Telefono = item.Telefono,
                        Celular = item.Celular,
                        Direccion = item.Direccion,
                        EstadoCivil = item.EstadoCivil

                    });
                }

                responseAPI.EsCorrecto = true;
                responseAPI.Valor = ListaCliente;
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
            var responseAPI = new ResponseAPI<ClienteDTO>();

            var Cliente = new ClienteDTO();

            try
            {

                var dbCliente = await _ProyectoPWcontext.Clientes.FirstOrDefaultAsync(c => c.Id == id);

                if (dbCliente != null)
                {
                    Cliente.Id = dbCliente.Id;
                    Cliente.Nombre = dbCliente.Nombre;
                    Cliente.Apellido = dbCliente.Apellido;
                    Cliente.Cedula = dbCliente.Cedula;
                    Cliente.Correo = dbCliente.Correo;
                    Cliente.Telefono = dbCliente.Telefono;
                    Cliente.Celular = dbCliente.Celular;
                    Cliente.Direccion = dbCliente.Direccion;
                    Cliente.EstadoCivil = dbCliente.EstadoCivil;

                    responseAPI.EsCorrecto = true;
                    responseAPI.Valor = Cliente;
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

        public async Task<IActionResult> Agregar(ClienteDTO cliente)
        {
            var responseAPI = new ResponseAPI<int>();

            try
            {

                var dbCliente = new Cliente
                {
                    Nombre = cliente.Nombre,
                    Apellido = cliente.Apellido,
                    Cedula = cliente.Cedula,
                    Correo = cliente.Correo,
                    Telefono = cliente.Telefono,
                    Celular = cliente.Celular,
                    Direccion = cliente.Direccion,
                    EstadoCivil = cliente.EstadoCivil,
                };

                _ProyectoPWcontext.Add(dbCliente);
                await _ProyectoPWcontext.SaveChangesAsync();

                if (dbCliente.Id != 0)
                {
                    responseAPI.EsCorrecto = true;
                    responseAPI.Valor = dbCliente.Id;
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

        public async Task<IActionResult> Editar(ClienteDTO cliente, int id)
        {
            var responseAPI = new ResponseAPI<int>();

            try
            {

                var dbCliente = await _ProyectoPWcontext.Clientes.FirstOrDefaultAsync(c => c.Id == id);

                if (dbCliente != null)
                {
                    dbCliente.Nombre = cliente.Nombre;
                    dbCliente.Apellido = cliente.Apellido;
                    dbCliente.Cedula = cliente.Cedula;
                    dbCliente.Correo = cliente.Correo;
                    dbCliente.Telefono = cliente.Telefono;
                    dbCliente.Celular = cliente.Celular;
                    dbCliente.Direccion = cliente.Direccion;
                    dbCliente.EstadoCivil = cliente.EstadoCivil;

                    _ProyectoPWcontext.Update(dbCliente);
                    await _ProyectoPWcontext.SaveChangesAsync();

                    responseAPI.EsCorrecto = true;
                    responseAPI.Valor = dbCliente.Id;


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
