using PROYECTOFINALPW.Shared;

namespace PROYECTOFINALPW.Client.Services
{
    public interface ICasoService
    {
        Task<List<CasoDTO>> Lista();
        Task<CasoDTO> Buscar(int id);
        Task<int> Agregar(CasoDTO caso);
        Task<int> Editar(CasoDTO caso);

        

    }
}
