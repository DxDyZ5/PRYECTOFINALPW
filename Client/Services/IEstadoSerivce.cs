using PROYECTOFINALPW.Shared;

namespace PROYECTOFINALPW.Client.Services
{
    public interface IEstadoSerivce
    {
        Task<List<EstadoCasoDTO>> Lista();
        Task<EstadoCasoDTO> Buscar(int id);
        Task<int> Agregar(EstadoCasoDTO estado);
        Task<int> Editar(EstadoCasoDTO estado);
        Task<bool> Eliminar(int id);
    }
}
