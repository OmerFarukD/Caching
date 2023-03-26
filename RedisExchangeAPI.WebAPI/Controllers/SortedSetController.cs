using Microsoft.AspNetCore.Mvc;
using RedisExchangeAPI.WebAPI.Models;
using RedisExchangeAPI.WebAPI.Service;
using StackExchange.Redis;

namespace RedisExchangeAPI.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SortedSetController : Controller
{
    private readonly RedisService _redisService;
    private readonly IDatabase db;
    private string sortedKey = "sortedlistKey";

    public SortedSetController(RedisService redisService)
    {
        _redisService = redisService;
        db = _redisService.GetDb(2);
    }

    [HttpPost("addDbSorted")]
    public IActionResult AddDbSorted([FromBody] SortedListModel sortedListModel)
    {
        db.SortedSetAdd(sortedKey, sortedListModel.Name, sortedListModel.Score);
        return Ok();
    }
    
}