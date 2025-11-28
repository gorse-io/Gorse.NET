namespace Gorse.NET.Models;

public class Feedback
{
    public string FeedbackType { set; get; } = "";
    public string UserId { set; get; } = "";
    public string ItemId { set; get; } = "";
    public double Value { set; get; } = 0.0;
    public string Timestamp { set; get; } = "";
}