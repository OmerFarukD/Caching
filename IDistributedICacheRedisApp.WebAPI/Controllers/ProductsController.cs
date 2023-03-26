using IDistributedICacheRedisApp.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace IDistributedICacheRedisApp.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IDistributedCache _distributedCache;

    public ProductsController(IDistributedCache distributedCache)
    {
        _distributedCache = distributedCache;
    }


    [HttpPost("setdb")]
    public IActionResult SetDb([FromBody] AddDbDto addDbDto)
    {
        DistributedCacheEntryOptions distributedCacheEntryOptions = new DistributedCacheEntryOptions();
        distributedCacheEntryOptions.AbsoluteExpiration = DateTime.Now.AddMinutes(1);
        _distributedCache.SetString(addDbDto.Key, addDbDto.Value, distributedCacheEntryOptions);
        return Ok("Tamam");
    }

    [HttpDelete("removedb")]
    public IActionResult RemoveDb([FromBody] RemoveDbDto removeDbDto)
    {
        _distributedCache.Remove(removeDbDto.Key);
        return Ok("Silindi.");
    }

    [HttpPost("SetDbComplextypeClass")]
    public async Task<IActionResult> SetDbComplextypeClass([FromBody] Product product)
    {
        DistributedCacheEntryOptions distributedCacheEntryOptions = new DistributedCacheEntryOptions();
        distributedCacheEntryOptions.AbsoluteExpiration = DateTime.Now.AddMinutes(2);
        string jsonproduct = JsonConvert.SerializeObject(product);
        await _distributedCache.SetStringAsync("product:", jsonproduct, distributedCacheEntryOptions);

        var data = await _distributedCache.GetStringAsync("product:");

        return Ok(data);
    }
    
}