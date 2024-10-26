using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;
using Trabajo_Final.Models;

public class ExternalApiService
{
    private readonly HttpClient _httpClient;

    public ExternalApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    // Método para obtener datos de productos desde una API externa
    public async Task<List<Producto>> GetProductsDataAsync()
    {
        HttpResponseMessage response = await _httpClient.GetAsync("posts"); // Cambiado a "posts"
        response.EnsureSuccessStatusCode();

        var data = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<List<Producto>>(data) ?? new List<Producto>();
    }


    // Método para obtener datos de clientes desde una API externa
    public async Task<List<Cliente>> GetClientesDataAsync()
    {
        HttpResponseMessage response = await _httpClient.GetAsync("users"); // Cambiado a "users"
        response.EnsureSuccessStatusCode();

        var data = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<List<Cliente>>(data) ?? new List<Cliente>();
    }
}
