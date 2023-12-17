using PROYECTOFINALPW.Shared;
using System.Net.Http.Json;

namespace PROYECTOFINALPW.Client.Services
{
    public class CasoService : ICasoService
    {
        private readonly HttpClient _httpClient;

        public CasoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<CasoDTO>> Lista()
        {
            var resultado = await _httpClient.GetFromJsonAsync<ResponseAPI<List<CasoDTO>>>("api/Caso/Lista");

            if (resultado!.EsCorrecto)
            {
                return resultado.Valor!;
            }
            else
            {
                throw new Exception(resultado.Mensaje);
            }
        }

        public async Task<CasoDTO> Buscar(int id)
        {
            var resultado = await _httpClient.GetFromJsonAsync<ResponseAPI<CasoDTO>>($"api/Caso/Buscar/{id}");

            if (resultado!.EsCorrecto)
            {
                return resultado.Valor!;
            }
            else
            {
                throw new Exception(resultado.Mensaje);
            }
        }

        public async Task<int> Agregar(CasoDTO caso)
        {
            var resultado = await _httpClient.PostAsJsonAsync("api/Caso/Agregar", caso);
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

        public async Task<int> Editar(CasoDTO caso)
        {
            var resultado = await _httpClient.PutAsJsonAsync($"api/Caso/Editar/{caso.Id}", caso);
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

    }
}
