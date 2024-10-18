namespace Gorse.NET;

using RestSharp;
using System.Net;
using System.Text.Json;

public class User
{
    public string UserId { set; get; } = "";
    public string[] Labels { set; get; } = Array.Empty<string>();

    public override bool Equals(object? obj) => obj is User other && this.Equals(other);

    public bool Equals(User user) => UserId.Equals(user.UserId) && Enumerable.SequenceEqual(Labels, user.Labels);

    public override int GetHashCode() => (UserId, Labels).GetHashCode();

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}

public class UserScore
{
    public string UserId { get; set; }
    public double Score { get; set; }
}

public class Feedback
{
    public string FeedbackType { set; get; } = "";
    public string UserId { set; get; } = "";
    public string ItemId { set; get; } = "";
    public string Timestamp { set; get; } = "";

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}

public class Result
{
    public int RowAffected { set; get; }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}

public class GorseException : Exception
{
    public HttpStatusCode StatusCode { set; get; }
    public new string? Message { set; get; }
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
        return Request<Result, User>(Method.Post, "api/user", user);
    }

    public Task<Result> InsertUserAsync(User user)
    {
        return RequestAsync<Result, User>(Method.Post, "api/user", user);
    }

    public User GetUser(string userId)
    {
        return Request<User, Object>(Method.Get, "api/user/" + userId, null);
    }

    public Task<User> GetUserAsync(string userId)
    {
        return RequestAsync<User, Object>(Method.Get, "api/user/" + userId, null);
    }

    public Result DeleteUser(string userId)
    {
        return Request<Result, Object>(Method.Delete, "api/user/" + userId, null);
    }

    public Task<Result> DeleteUserAsync(string userId)
    {
        return RequestAsync<Result, Object>(Method.Delete, "api/user/" + userId, null);
    }

    public Result InsertFeedback(Feedback[] feedbacks)
    {
        return Request<Result, Feedback[]>(Method.Post, "api/feedback", feedbacks);
    }

    public Task<Result> InsertFeedbackAsync(Feedback[] feedbacks)
    {
        return RequestAsync<Result, Feedback[]>(Method.Post, "api/feedback", feedbacks);
    }

    public string[] GetRecommend(string userId)
    {
        return Request<string[], Object>(Method.Get, "api/recommend/" + userId, null);
    }

    public Task<string[]> GetRecommendAsync(string userId)
    {
        return RequestAsync<string[], Object>(Method.Get, "api/recommend/" + userId, null);
    }
    public List<UserScore> GetUserNeighbors(string userId)
    {
        return Request<List<UserScore>, Object>(Method.Get, @"api/user/{userId}/neighbors", null);
    }

    public Task<List<UserScore>> GetUserNeighborsAsync(string userId)
    {
        return RequestAsync<List<UserScore>, Object>(Method.Get, @"api/user/{userId}/neighbors", null);
    }

    public RetType Request<RetType, ReqType>(Method method, string resource, ReqType? req) where ReqType : class
    {
        var request = new RestRequest(resource, method);
        if (req != null)
        {
            request.AddJsonBody(req);
        }
        var response = client.Execute(request);
        if (response.StatusCode != HttpStatusCode.OK)
        {
            throw new GorseException
            {
                StatusCode = response.StatusCode,
                Message = response.Content
            };
        }
        else if (response.Content == null)
        {
            throw new GorseException
            {
                StatusCode = HttpStatusCode.InternalServerError,
                Message = "unexcepted empty response"
            };
        }
        RetType? ret = JsonSerializer.Deserialize<RetType>(response.Content);
        if (ret == null)
        {
            throw new GorseException
            {
                StatusCode = HttpStatusCode.InternalServerError,
                Message = "unexcepted null response"
            };
        }
        return ret;
    }

    public async Task<RetType> RequestAsync<RetType, ReqType>(Method method, string resource, ReqType? req) where ReqType : class
    {
        var request = new RestRequest(resource, method);
        if (req != null)
        {
            request.AddJsonBody(req);
        }
        var response = await client.ExecuteAsync(request);
        if (response.StatusCode != HttpStatusCode.OK)
        {
            throw new GorseException
            {
                StatusCode = response.StatusCode,
                Message = response.Content
            };
        }
        else if (response.Content == null)
        {
            throw new GorseException
            {
                StatusCode = HttpStatusCode.InternalServerError,
                Message = "unexcepted empty response"
            };
        }
        RetType? ret = JsonSerializer.Deserialize<RetType>(response.Content);
        if (ret == null)
        {
            throw new GorseException
            {
                StatusCode = HttpStatusCode.InternalServerError,
                Message = "unexcepted null response"
            };
        }
        return ret;
    }
}
