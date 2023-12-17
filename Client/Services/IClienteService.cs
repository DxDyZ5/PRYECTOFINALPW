using PROYECTOFINALPW.Shared;

namespace PROYECTOFINALPW.Client.Services
{
    public interface IClienteService
    {
        Task<List<ClienteDTO>> Lista();

        Task<ClienteDTO> Buscar(int id);
        Task<int> Agregar(ClienteDTO cliente);
        Task<int> Editar(ClienteDTO cliente);
    }
}
