namespace Gorse.NET.Models;

public class ItemsResponse
{
    public string Cursor { get; set; } = "";
    public List<Item> Items { get; set; } = new();
}
