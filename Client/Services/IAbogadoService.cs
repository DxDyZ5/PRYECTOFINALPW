using PROYECTOFINALPW.Shared;

namespace PROYECTOFINALPW.Client.Services
{
    public interface IAbogadoService
    {
        Task<List<AbogadoDTO>> Lista();

        Task<AbogadoDTO> Buscar(int id);
        Task<int> Agregar(AbogadoDTO abogado);
        Task<int> Editar(AbogadoDTO abogado);
        Task<bool> Eliminar(int id);
    }
}
