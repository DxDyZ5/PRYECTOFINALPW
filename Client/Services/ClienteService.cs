using PROYECTOFINALPW.Shared;
using System.Net.Http.Json;

namespace PROYECTOFINALPW.Client.Services
{
    public class ClienteService : IClienteService
    {
        private readonly HttpClient _httpClient;

        public ClienteService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ClienteDTO>> Lista()
        {
            var resultado = await _httpClient.GetFromJsonAsync<ResponseAPI<List<ClienteDTO>>>("api/Cliente/Lista");

            if(resultado!.EsCorrecto)
            {
                return resultado.Valor!;
            }
            else
            {
                throw new Exception(resultado.Mensaje);
            }
        }

        public async Task<ClienteDTO> Buscar(int id)
        {
            var resultado = await _httpClient.GetFromJsonAsync<ResponseAPI<ClienteDTO>>($"api/Cliente/Buscar/{id}");

            if (resultado!.EsCorrecto)
            {
                return resultado.Valor!;
            }
            else
            {
                throw new Exception(resultado.Mensaje);
            }
        }

        public async Task<int> Agregar(ClienteDTO cliente)
        {
            var resultado = await _httpClient.PostAsJsonAsync("api/Cliente/Agregar", cliente);
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


        public async Task<int> Editar(ClienteDTO cliente)
        {
            var resultado = await _httpClient.PutAsJsonAsync($"api/Cliente/Editar/{cliente.Id}", cliente);
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
