using StackExchange.Redis;

namespace RedisExchangeAPI.WebAPI.Service;

public class RedisService
{
    private readonly string _host;
    private readonly int _port;
    private ConnectionMultiplexer _redis;

    public RedisService(string host, int port)
    {
        _host = host;
        _port = port;
    }

    public void Connect()
    {
        var configstring = $"{_host}:{_port}";
        _redis=ConnectionMultiplexer.Connect(configstring);
    }

    public IDatabase GetDb(int database)
    {
        return _redis.GetDatabase(database);
    }
}