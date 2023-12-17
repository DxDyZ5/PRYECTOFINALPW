using PROYECTOFINALPW.Shared;
using System.Net.Http.Json;

namespace PROYECTOFINALPW.Client.Services
{
    public class AbogadoService : IAbogadoService
    {
        private readonly HttpClient _httpClient;

        public AbogadoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

     

        public async Task<List<AbogadoDTO>> Lista()
        {
            var resultado = await _httpClient.GetFromJsonAsync<ResponseAPI<List<AbogadoDTO>>>("api/Abogado/Lista");

            if (resultado!.EsCorrecto)
            {
                return resultado.Valor!;
            }
            else
            {
                throw new Exception(resultado.Mensaje);
            }
        }

        public async Task<AbogadoDTO> Buscar(int id)
        {
            var resultado = await _httpClient.GetFromJsonAsync<ResponseAPI<AbogadoDTO>>($"api/Abogado/Buscar/{id}");

            if (resultado!.EsCorrecto)
            {
                return resultado.Valor!;
            }
            else
            {
                throw new Exception(resultado.Mensaje);
            }
        }

        public async Task<int> Agregar(AbogadoDTO abogado)
        {
            var resultado = await _httpClient.PostAsJsonAsync("api/Abogado/Agregar", abogado);
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

        public async Task<int> Editar(AbogadoDTO abogado)
        {
            var resultado = await _httpClient.PutAsJsonAsync($"api/Abogado/Editar/{abogado.Id}", abogado);
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
            var resultado = await _httpClient.DeleteAsync($"api/Abogado/Eliminar/{id}");
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
