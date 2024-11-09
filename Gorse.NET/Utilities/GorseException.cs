using System.Net;

namespace Gorse.NET.Utilities;

public class GorseException : Exception
{
    public HttpStatusCode StatusCode { set; get; }
    public new string? Message { set; get; }

    public GorseException(string? message, HttpStatusCode statusCode) : base(message)
    {
        Message = message;
        StatusCode = statusCode;
    }
}