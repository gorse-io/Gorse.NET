using Gorse.NET.Utilities;
using RestSharp;

namespace Gorse.NET;

public partial class Gorse
{
    private readonly RequestClient _client;
    public Gorse(string endpoint, string apiKey)
    {
        var restClient = new RestClient(endpoint);
        restClient.AddDefaultHeader("X-API-Key", apiKey);
        _client = new RequestClient(restClient);
    }
}
