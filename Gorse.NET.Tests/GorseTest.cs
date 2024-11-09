using StackExchange.Redis;

namespace Gorse.NET.Tests;

public partial class Tests
{
    private const string ENDPOINT = "http://127.0.0.1:8088";
    private const string API_KEY = "zhenghaoz";

    private Gorse client = new Gorse(ENDPOINT, API_KEY);
    private ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(
        new ConfigurationOptions
        {
            EndPoints = { "127.0.0.1:6379" }
        });
}
