using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

public class ExternalApiService
{
    private readonly HttpClient _httpClient;
    private readonly string _baseUrl;

    public ExternalApiService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _baseUrl = configuration["ExternalApi:BaseUrl"] ?? throw new ArgumentNullException("External API Base URL not configured.");
    }

    public async Task<string> GetProductsDataAsync()
{
    var response = await _httpClient.GetAsync($"{_baseUrl}"); // Se conecta a https://api.tienda-ejemplo.com/products
    response.EnsureSuccessStatusCode();
    return await response.Content.ReadAsStringAsync();
}

}
