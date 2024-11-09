namespace Gorse.NET.Models;

public class FeedbacksResponse
{
    public string Cursor { get; set; } = "";
    public List<Feedback> Feedback { get; set; } = new();
}