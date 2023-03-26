using Microsoft.AspNetCore.Mvc;
using RedisExchangeAPI.WebAPI.Service;
using StackExchange.Redis;

namespace RedisExchangeAPI.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ListTypeController : ControllerBase
{
    private readonly RedisService _redisService;
    private readonly IDatabase db;
    private string listKey = "isimler";

    public ListTypeController(RedisService redisService)
    {
        _redisService = redisService;
        db = _redisService.GetDb(0);
    }

    [HttpPost("addlist")]
    public IActionResult AddList([FromBody] string value)
    {
        
        db.ListRightPush(listKey, value);
        
        return Ok();
    }

    [HttpGet]
    public IActionResult ShowData()
    {
        List<string> nameList = new List<string>();

        if (db.KeyExists(listKey))
        {
            db.ListRange(listKey).ToList().ForEach(x =>
            {
                nameList.Add(x.ToString());
            });

            return Ok(nameList);
        }
        return NotFound();
    }
}