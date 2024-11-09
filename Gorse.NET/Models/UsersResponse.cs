namespace Gorse.NET.Models;

public class UsersResponse
{
    public string Cursor { get; set; } = "";
    public List<User> Users { get; set; } = new();
}