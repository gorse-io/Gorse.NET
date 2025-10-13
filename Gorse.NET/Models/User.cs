using System.Text.Json;

namespace Gorse.NET.Models;

public class User
{
    public string UserId { set; get; } = "";
    public string[] Labels { set; get; } = Array.Empty<string>();
    public string Comment { get; set; } = "";
    public override bool Equals(object? obj) => obj is User other && this.Equals(other);

    public bool Equals(User user) => UserId.Equals(user.UserId) && Enumerable.SequenceEqual(Labels, user.Labels);

    public override int GetHashCode() => (UserId, Labels).GetHashCode();

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}