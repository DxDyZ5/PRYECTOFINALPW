using PROYECTOFINALPW.Shared;

namespace PROYECTOFINALPW.Client.Services
{
    public interface ItipoDeCaso
    {
        Task<List<TipoCasoDTO>> Lista();
        Task<TipoCasoDTO> Buscar(int id);
        Task<int> Agregar(TipoCasoDTO tipo);
        Task<int> Editar(TipoCasoDTO tipo);
        Task<bool> Eliminar(int id);
    }
}
