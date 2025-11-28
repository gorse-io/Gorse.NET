using Gorse.NET.Models;

namespace Gorse.NET.Tests;

public partial class Tests
{
    [Test]
    public void TestFeedback()
    {
        var feedbacks = new Feedback[]
        {
            new Feedback
            {
                FeedbackType = "watch",
                UserId = "2000",
                ItemId = "1",
                Value = 1.0,
                Timestamp = DateTime.UtcNow.ToString("o"),
            },
            new Feedback
            {
                FeedbackType = "watch",
                UserId = "2000",
                ItemId = "1060",
                Value = 2.0,
                Timestamp = DateTime.UtcNow.ToString("o"),
            },
            new Feedback
            {
                FeedbackType = "watch",
                UserId = "2000",
                ItemId = "11",
                Value = 3.0,
                Timestamp = DateTime.UtcNow.ToString("o"),
            },
        };
        foreach (var fb in feedbacks)
        {
            client.DeleteUserItemFeedbacksWithFeedbackType(fb.FeedbackType, fb.UserId, fb.ItemId);
        }
        var rowAffected = client.InsertFeedback(feedbacks);
        Assert.That(rowAffected.RowAffected, Is.EqualTo(3));
    }

    [Test]
    public async Task TestFeedbackAsync()
    {
        var feedbacks = new Feedback[]
        {
            new Feedback
            {
                FeedbackType = "watch",
                UserId = "2000",
                ItemId = "1",
                Value = 1.0,
                Timestamp = DateTime.UtcNow.ToString("o"),
            },
            new Feedback
            {
                FeedbackType = "watch",
                UserId = "2000",
                ItemId = "1060",
                Value = 2.0,
                Timestamp = DateTime.UtcNow.ToString("o"),
            },
            new Feedback
            {
                FeedbackType = "watch",
                UserId = "2000",
                ItemId = "11",
                Value = 3.0,
                Timestamp = DateTime.UtcNow.ToString("o"),
            },
        };
        foreach (var fb in feedbacks)
        {
            await client.DeleteUserItemFeedbacksWithFeedbackTypeAsync(fb.FeedbackType, fb.UserId, fb.ItemId);
        }
        var rowAffected = await client.InsertFeedbackAsync(feedbacks);
        Assert.That(rowAffected.RowAffected, Is.EqualTo(3));
    }
}
