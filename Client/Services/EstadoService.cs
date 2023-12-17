using PROYECTOFINALPW.Shared;
using System.Net.Http.Json;

namespace PROYECTOFINALPW.Client.Services
{
    public class EstadoService : IEstadoSerivce
    {
        private readonly HttpClient _httpClient;

        public EstadoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<EstadoCasoDTO>> Lista()
        {
            var resultado = await _httpClient.GetFromJsonAsync<ResponseAPI<List<EstadoCasoDTO>>>("api/EstadoCaso/Lista");

            if (resultado!.EsCorrecto)
            {
                return resultado.Valor!;
            }
            else
            {
                throw new Exception(resultado.Mensaje);
            }
        }

        public async Task<EstadoCasoDTO> Buscar(int id)
        {
            var resultado = await _httpClient.GetFromJsonAsync<ResponseAPI<EstadoCasoDTO>>($"api/EstadoCaso/Buscar/{id}");

            if (resultado!.EsCorrecto)
            {
                return resultado.Valor!;
            }
            else
            {
                throw new Exception(resultado.Mensaje);
            }
        }
        public async Task<int> Agregar(EstadoCasoDTO estado)
        {
            var resultado = await _httpClient.PostAsJsonAsync("api/EstadoCaso/Agregar", estado);
            var response = await resultado.Content.ReadFromJsonAsync<ResponseAPI<int>>();

            if (response!.EsCorrecto)
            {
                return response.Valor!;
            }
            else
            {
                throw new Exception(response.Mensaje);
            }
        }

        public async Task<int> Editar(EstadoCasoDTO estado)
        {
            var resultado = await _httpClient.PutAsJsonAsync($"api/EstadoCaso/Editar/{estado.Id}", estado);
            var response = await resultado.Content.ReadFromJsonAsync<ResponseAPI<int>>();

            if (response!.EsCorrecto)
            {
                return response.Valor!;
            }
            else
            {
                throw new Exception(response.Mensaje);
            }
        }

        public async Task<bool> Eliminar(int id)
        {
            var resultado = await _httpClient.DeleteAsync($"api/EstadoCaso/Eliminar/{id}");
            var response = await resultado.Content.ReadFromJsonAsync<ResponseAPI<int>>();

            if (response!.EsCorrecto)
            {
                return response.EsCorrecto!;
            }
            else
            {
                throw new Exception(response.Mensaje);
            }
        }
    }
}
