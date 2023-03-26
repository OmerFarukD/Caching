using Microsoft.AspNetCore.Mvc;
using RedisExchangeAPI.WebAPI.Models;
using RedisExchangeAPI.WebAPI.Service;
using StackExchange.Redis;

namespace RedisExchangeAPI.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StringTypeController : ControllerBase
{
    private readonly RedisService _redisService;
    private readonly IDatabase db;

    public StringTypeController(RedisService redisService)
    {
        _redisService = redisService;
        db = _redisService.GetDb(0);

    }

    [HttpPost("setstring")]
    public IActionResult SetStringDb([FromBody] RedisStringModel redisStringModel)
    {
        db.StringSet(redisStringModel.Key, redisStringModel.Value);
        return Ok("Eklendi");
    }

    [HttpPost("getstring")]
    public IActionResult GetString([FromBody] GetStringModel getStringModel)
    {
        var data = db.StringGet(getStringModel.Key);
        return Ok(data.ToString());
    }
    
    [HttpPost("setint")]
    public IActionResult SetIntDb([FromBody] RedisIntModel redisModel)
    {
        db.StringSet(redisModel.Key, redisModel.Value);
        return Ok("Eklendi");
    }

    [HttpPost("getincrement")]
    public IActionResult GetIncrement([FromBody] GetStringModel getStringModel)
    {
        db.StringIncrement(getStringModel.Key,10);
        var data = db.StringGet(getStringModel.Key);
        if (data.HasValue) return Ok(data.ToString());

        return BadRequest();
    }
}