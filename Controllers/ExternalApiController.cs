using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class ExternalApiController : ControllerBase
{
    private readonly ExternalApiService _externalApiService;

    public ExternalApiController(ExternalApiService externalApiService)
    {
        _externalApiService = externalApiService;
    }

    [HttpGet("products")]
    public async Task<IActionResult> GetProducts()
    {
        var data = await _externalApiService.GetProductsDataAsync();
        return Ok(data);
    }
}
