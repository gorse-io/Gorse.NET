using System.Text.Json;

namespace Gorse.NET.Models;

public class User
{
    public string UserId { set; get; } = "";
    public object? Labels { set; get; }
    public string Comment { get; set; } = "";
}