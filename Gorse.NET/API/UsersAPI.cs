using Gorse.NET.Models;
using RestSharp;

namespace Gorse.NET;

public partial class Gorse
{
    public Result InsertUser(User user)
    {
        return _client.Request<Result, User>(Method.Post, "api/user", user)!;
    }

    public Task<Result> InsertUserAsync(User user)
    {
        return _client.RequestAsync<Result, User>(Method.Post, "api/user", user)!;
    }

    public Result InsertUsers(List<User> users)
    {
        return _client.Request<Result, List<User>>(Method.Post, "api/users", users)!;
    }

    public Task<Result> InsertUsersAsync(List<User> users)
    {
        return _client.RequestAsync<Result, List<User>>(Method.Post, "api/users", users)!;
    }

    public User GetUser(string userId)
    {
        return _client.Request<User, Object>(Method.Get, "api/user/" + userId, null)!;
    }

    public Task<User> GetUserAsync(string userId)
    {
        return _client.RequestAsync<User, Object>(Method.Get, "api/user/" + userId, null)!;
    }

    public UsersResponse GetUsers(int n, string cursor = "")
    {
        return _client.Request<UsersResponse, Object>(Method.Get, $"api/users?n={n}&cursor={cursor}", null)!;
    }

    public Task<UsersResponse> GetUsersAsync(int n, string cursor = "")
    {
        return _client.RequestAsync<UsersResponse, Object>(Method.Get, $"api/users?n={n}&cursor={cursor}", null)!;
    }

    public Result DeleteUser(string userId)
    {
        return _client.Request<Result, Object>(Method.Delete, "api/user/" + userId, null)!;
    }

    public Task<Result> DeleteUserAsync(string userId)
    {
        return _client.RequestAsync<Result, Object>(Method.Delete, "api/user/" + userId, null)!;
    }

    public Result UpdateUser(string userId, User userToUpdate)
    {
        return _client.Request<Result, Object>(Method.Patch, $"api/user/{userId}", userToUpdate)!;
    }

    public Task<Result> UpdateUserAsync(string userId, User userToUpdate)
    {
        return _client.RequestAsync<Result, Object>(Method.Patch, $"api/user/{userId}", userToUpdate)!;
    }
}