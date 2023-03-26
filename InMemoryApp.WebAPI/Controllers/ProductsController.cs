using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace InMemoryApp.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : Controller
{

    private readonly IMemoryCache _memoryCache;

    public ProductsController(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    [HttpPost]
    public IActionResult SetMessage()
    {
        if (! _memoryCache.TryGetValue("zaman", out string zamancache))
        {
            _memoryCache.Set<string>("zaman", DateTime.Now.ToString());
        }

        
        return Ok();
    }

    [HttpGet]
    public IActionResult GetMessage()
    {
       var data= _memoryCache.Get<string>("zaman");

       return Ok(data);
    }
}