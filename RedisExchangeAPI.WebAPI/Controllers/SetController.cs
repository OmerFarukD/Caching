using Microsoft.AspNetCore.Mvc;
using RedisExchangeAPI.WebAPI.Service;
using StackExchange.Redis;

namespace RedisExchangeAPI.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SetController : Controller
{
    private readonly RedisService _redisService;

    private IDatabase db;
    private string setkey = "isimler_set";
    
    public SetController(RedisService redisService)
    {
        _redisService = redisService;
        db = _redisService.GetDb(0);
    }

    [HttpPost("addset")]
    public IActionResult AddSet([FromBody] string value)
    {
        if (!db.KeyExists(setkey))
        {
            db.KeyExpire(setkey,DateTime.Now.AddMinutes(1));
        }
        db.SetAdd(setkey, value);
        return Ok();
    }

    [HttpGet("show")]
    public IActionResult Show()
    {
        HashSet<string> hashSet = new HashSet<string>();
        if (db.KeyExists(setkey))
        {
            db.SetMembers(setkey).ToList().ForEach(x =>
            {
                hashSet.Add(x.ToString());
            });
        }
        return Ok(hashSet);
    }
}