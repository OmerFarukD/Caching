using Microsoft.AspNetCore.Mvc;
using RedisExchangeAPI.WebAPI.Models;
using RedisExchangeAPI.WebAPI.Service;
using StackExchange.Redis;

namespace RedisExchangeAPI.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HashTypeController : Controller
{
    private readonly RedisService _redisService;
    private readonly IDatabase _database;

    private string hashKey = "hashKey";
    
    public HashTypeController(RedisService redisService)
    {
        _redisService = redisService;
        _database = _redisService.GetDb(4);
    }

    [HttpPost("add")]
    public IActionResult Add([FromBody] HashModel hashModel)
    {
        _database.HashSet(hashKey, hashModel.Key, hashModel.Value);
        return Ok();
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        Dictionary<string, string> dictionary = new();

        if (_database.KeyExists(hashKey))
        {
            _database.HashGetAll(hashKey).ToList().ForEach(x =>
            {
                dictionary.Add(x.Name.ToString(),x.Value.ToString());
            });
        }

        return Ok(dictionary);
    }
}