namespace Gorse.NET.Models;

public class Item
{
    public string ItemId { get; set; } = "";
    public string[] Categories { get; set; } = Array.Empty<string>();
    public string Comment { get; set; } = "";
    public bool IsHidden { get; set; }
    public object? Labels { set; get; }
    public DateTime TimeStamp { get; set; } = DateTime.UtcNow;
}