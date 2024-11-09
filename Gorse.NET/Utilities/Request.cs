using System.Text.Json;
using RestSharp;

namespace Gorse.NET.Utilities;
public class RequestClient
{
    private readonly RestClient _client;
    public RequestClient(RestClient client)
    {
        _client = client;
    }

    public RetType? Request<RetType, ReqType>(Method method, string resource, ReqType? req) where ReqType : class
    {
        var request = new RestRequest(resource, method);
        if (req != null)
        {
            request.AddJsonBody(req);
        }
        var response = _client.Execute(request);
        if (!response.IsSuccessStatusCode)
        {
            throw new GorseException(message: response.Content, statusCode: response.StatusCode);
        }
        // Handle case where response content is null
        if (response.Content == null)
        {
            return default;
        }
        // Deserialize response content to the expected type
        try
        {
            return JsonSerializer.Deserialize<RetType>(response.Content);
        }
        catch (JsonException jsonEx) // Specific error handling for JSON deserialization
        {
            throw new GorseException(
                message: $"Deserialization failed: {jsonEx}. \nResponse content: {response.Content}. \nStatus code: {response.StatusCode}",
                statusCode: response.StatusCode
            );
        }
        catch (Exception ex) // General error handling for any other exceptions
        {
            throw new GorseException(
                message: $"An error occurred while processing the response: {ex}. \nResponse content: {response.Content}. \nStatus code: {response.StatusCode}",
                statusCode: response.StatusCode
            );
        }
    }

    public async Task<RetType?> RequestAsync<RetType, ReqType>(Method method, string resource, ReqType? req) where ReqType : class
    {
        var request = new RestRequest(resource, method);
        if (req != null)
        {
            request.AddJsonBody(req);
        }
        var response = await _client.ExecuteAsync(request);
        if (!response.IsSuccessStatusCode)
        {
            throw new GorseException(message: response.Content, statusCode: response.StatusCode);
        }
        // Handle case where response content is null
        if (response.Content == null)
        {
            return default;
        }
        // Deserialize response content to the expected type
        try
        {
            return JsonSerializer.Deserialize<RetType>(response.Content);
        }
        catch (JsonException jsonEx) // Specific error handling for JSON deserialization
        {
            throw new GorseException(
                message: $"Deserialization failed: {jsonEx}. \nResponse content: {response.Content}. \nStatus code: {response.StatusCode}",
                statusCode: response.StatusCode
            );
        }
        catch (Exception ex) // General error handling for any other exceptions
        {
            throw new GorseException(
                message: $"An error occurred while processing the response: {ex}. \nResponse content: {response.Content}. \nStatus code: {response.StatusCode}",
                statusCode: response.StatusCode
            );
        }
    }
}