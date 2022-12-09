namespace Gorse.NET;

using RestSharp;
using System.Text.Json;

public class User
{
    public string UserId { set; get; }
    public string[] Labels { set; get; }
}

public class Result
{
    public int RowAffected { set; get; }
}

public class Gorse
{
    private RestClient client;

    public Gorse(string endpoint, string apiKey)
    {
        this.client = new RestClient(endpoint);
        this.client.AddDefaultHeader("X-API-Key", apiKey);
    }

    public Result InsertUser(User user)
    {
        var request = new RestRequest("api/user");
        request.AddJsonBody(user);
        var response = client.Post(request);
        return JsonSerializer.Deserialize<Result>(response.Content);
    }
}
