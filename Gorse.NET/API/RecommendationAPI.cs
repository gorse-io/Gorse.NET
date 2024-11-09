using Gorse.NET.Models;
using RestSharp;

namespace Gorse.NET;

public partial class Gorse
{
    public string[]? GetRecommend(string userId)
    {
        return _client.Request<string[], Object>(Method.Get, "api/recommend/" + userId, null);
    }

    public Task<string[]?> GetRecommendAsync(string userId)
    {
        return _client.RequestAsync<string[], Object>(Method.Get, "api/recommend/" + userId, null);
    }

    public List<UserScore> GetUserNeighbors(string userId, int n = 100, int offset = 0)
    {
        return _client.Request<List<UserScore>, object>(Method.Get, $"api/user/{userId}/neighbors?n={n}&offset={offset}", null)!;
    }

    public Task<List<UserScore>> GetUserNeighborsAsync(string userId, int n = 100, int offset = 0)
    {
        return _client.RequestAsync<List<UserScore>, object>(Method.Get, $"api/user/{userId}/neighbors?n={n}&offset={offset}", null)!;
    }
}