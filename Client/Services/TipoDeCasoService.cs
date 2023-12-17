using PROYECTOFINALPW.Shared;
using System.Net.Http.Json;

namespace PROYECTOFINALPW.Client.Services
{
    public class TipoDeCasoService : ItipoDeCaso
    {
        private readonly HttpClient _httpClient;

        public TipoDeCasoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<TipoCasoDTO>> Lista()
        {
            var resultado = await _httpClient.GetFromJsonAsync<ResponseAPI<List<TipoCasoDTO>>>("api/TipoCaso/Lista");

            if (resultado!.EsCorrecto)
            {
                return resultado.Valor!;
            }
            else
            {
                throw new Exception(resultado.Mensaje);
            }
        }

        public async Task<TipoCasoDTO> Buscar(int id)
        {
            var resultado = await _httpClient.GetFromJsonAsync<ResponseAPI<TipoCasoDTO>>($"api/TipoCaso/Buscar/{id}");

            if (resultado!.EsCorrecto)
            {
                return resultado.Valor!;
            }
            else
            {
                throw new Exception(resultado.Mensaje);
            }
        }

        public async Task<int> Agregar(TipoCasoDTO tipo)
        {
            var resultado = await _httpClient.PostAsJsonAsync("api/TipoCaso/Agregar", tipo);
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

        public async Task<int> Editar(TipoCasoDTO tipo)
        {
            var resultado = await _httpClient.PutAsJsonAsync($"api/TipoCaso/Editar/{tipo.Id}", tipo);
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
            var resultado = await _httpClient.DeleteAsync($"api/TipoCaso/Eliminar/{id}");
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
