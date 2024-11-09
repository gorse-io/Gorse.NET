
using Gorse.NET.Models;

namespace Gorse.NET.Tests;

public partial class Tests
{
    [Test]
    public void TestFeedback()
    {
        // Insert feedback for two users and items
        var feedbacks = new Feedback[]
        {
        new Feedback { FeedbackType = "read", UserId = "user123", ItemId = "item789", Timestamp = "2022-11-20T13:55:27Z" },
        new Feedback { FeedbackType = "read", UserId = "user123", ItemId = "item456", Timestamp = "2022-11-20T13:55:27Z" },
        };

        // Insert the feedback into the system
        var rowAffected = client.InsertFeedback(feedbacks);
        Assert.That(rowAffected.RowAffected, Is.EqualTo(2));

        // Verify initial insertion by retrieving feedbacks for a specific user and item
        var userItemFeedbacks = client.GetUserItemFeedbacks(feedbacks[0].UserId, feedbacks[0].ItemId);
        Assert.That(userItemFeedbacks.Feedback.Count, Is.EqualTo(2));  // Two feedback entries for the same user

        // Verify feedback retrieval by feedback type
        var itemFeedbacks = client.GetFeedbacksWithFeedbackType(feedbacks[0].FeedbackType);
        Assert.That(itemFeedbacks.Feedback.Count, Is.EqualTo(2));  // Expect two "read" feedbacks

        // Verify Get Feedbacks With Feedback Type method
        itemFeedbacks = client.GetFeedbacksWithFeedbackType("read");
        Assert.That(itemFeedbacks.Feedback.Count, Is.EqualTo(2));  // Two "read" feedbacks
        itemFeedbacks = client.GetFeedbacksWithFeedbackType("liked");
        Assert.That(itemFeedbacks.Feedback.Count, Is.EqualTo(0));  // No "liked" feedbacks

        // Verify Get User Item Feedbacks With Feedback Type method
        var feedback = client.GetUserItemFeedbacksWithFeedbackType(feedbacks[0].UserId, feedbacks[0].ItemId, feedbacks[0].FeedbackType);
        Assert.That(feedback.UserId, Is.EqualTo(feedbacks[0].UserId));
        Assert.That(feedback.ItemId, Is.EqualTo(feedbacks[0].ItemId));
        Assert.That(feedback.FeedbackType, Is.EqualTo(feedbacks[0].FeedbackType));

        // Delete the feedbacks as per the original test
        rowAffected = client.DeleteUserItemFeedbacksWithFeedbackType(feedbacks[0].UserId, feedbacks[0].ItemId, feedbacks[0].FeedbackType);
        Assert.That(rowAffected.RowAffected, Is.EqualTo(1));  // Ensure one feedback entry is deleted

        rowAffected = client.DeleteUserItemFeedbacksWithFeedbackType(feedbacks[1].UserId, feedbacks[1].ItemId, feedbacks[1].FeedbackType);
        Assert.That(rowAffected.RowAffected, Is.EqualTo(1));  // Ensure second feedback entry is deleted
    }

    [Test]
    public async Task TestFeedbackAsync()
    {
        // Insert feedback for two users and items
        var feedbacks = new Feedback[]
        {
        new Feedback { FeedbackType = "read", UserId = "user123", ItemId = "item789", Timestamp = "2022-11-20T13:55:27Z" },
        new Feedback { FeedbackType = "read", UserId = "user123", ItemId = "item456", Timestamp = "2022-11-20T13:55:27Z" },
        };

        // Insert the feedback asynchronously into the system
        var rowAffected = await client.InsertFeedbackAsync(feedbacks);
        Assert.That(rowAffected.RowAffected, Is.EqualTo(2));  // Ensure two feedbacks are inserted

        // Verify initial insertion by retrieving feedbacks for a specific user and item
        var userItemFeedbacks = await client.GetUserItemFeedbacksAsync(feedbacks[0].UserId, feedbacks[0].ItemId);
        Assert.That(userItemFeedbacks.Feedback.Count, Is.EqualTo(2));  // Two feedback entries for the same user

        // Verify feedback retrieval by feedback type
        var itemFeedbacks = await client.GetFeedbacksWithFeedbackTypeAsync(feedbacks[0].FeedbackType);
        Assert.That(itemFeedbacks.Feedback.Count, Is.EqualTo(2));  // Expect two "read" feedbacks

        // Verify Get Feedbacks With Feedback Type method
        itemFeedbacks = await client.GetFeedbacksWithFeedbackTypeAsync("read");
        Assert.That(itemFeedbacks.Feedback.Count, Is.EqualTo(2));  // Two "read" feedbacks
        itemFeedbacks = await client.GetFeedbacksWithFeedbackTypeAsync("liked");
        Assert.That(itemFeedbacks.Feedback.Count, Is.EqualTo(0));  // No "liked" feedbacks

        // Verify Get User Item Feedbacks With Feedback TypeAsync method
        var feedback = await client.GetUserItemFeedbacksWithFeedbackTypeAsync(feedbacks[0].UserId, feedbacks[0].ItemId, feedbacks[0].FeedbackType);
        Assert.That(feedback.UserId, Is.EqualTo(feedbacks[0].UserId));
        Assert.That(feedback.ItemId, Is.EqualTo(feedbacks[0].ItemId));
        Assert.That(feedback.FeedbackType, Is.EqualTo(feedbacks[0].FeedbackType));

        // Delete the feedbacks as per the original test
        rowAffected = await client.DeleteUserItemFeedbacksWithFeedbackTypeAsync(feedbacks[0].UserId, feedbacks[0].ItemId, feedbacks[0].FeedbackType);
        Assert.That(rowAffected.RowAffected, Is.EqualTo(1));  // Ensure one feedback entry is deleted

        rowAffected = await client.DeleteUserItemFeedbacksWithFeedbackTypeAsync(feedbacks[1].UserId, feedbacks[1].ItemId, feedbacks[1].FeedbackType);
        Assert.That(rowAffected.RowAffected, Is.EqualTo(1));  // Ensure second feedback entry is deleted
    }

    [Test]
    public void TestFeedbackWithCursor()
    {
        // Insert feedback for two users and items
        var feedbacks = new Feedback[]
        {
        new Feedback
        {
            FeedbackType = "read",
            UserId = "user123",
            ItemId = "book789",
            Timestamp = DateTime.UtcNow.ToString()
        },
        new Feedback
        {
            FeedbackType = "liked",
            UserId = "user123",
            ItemId = "movie456",
            Timestamp = DateTime.UtcNow.ToString()
        },
        };

        var feedbackOne = feedbacks[0];
        var feedbackTwo = feedbacks[1];

        // Insert feedbacks into the system
        var rowAffected = client.InsertFeedback(feedbacks);
        Assert.That(rowAffected.RowAffected, Is.EqualTo(2));  // Ensure two feedbacks are inserted

        // Get feedback for the first user and item, limit the number of items returned
        var returnedFeedbacks = client.GetUserItemFeedbacks(feedbacks[0].UserId, feedbackOne.ItemId, n: 10);
        Assert.That(returnedFeedbacks.Feedback.Count, Is.GreaterThanOrEqualTo(1));  // Expect at least one feedback

        // Retrieve a batch of feedbacks using a cursor
        returnedFeedbacks = client.GetFeedbacks(1);  // Get the first page of feedbacks
        returnedFeedbacks = client.GetFeedbacks(1, returnedFeedbacks.Cursor);  // Get next page of feedbacks
        Assert.That(returnedFeedbacks.Feedback.Count, Is.EqualTo(1));  // Ensure only one feedback is returned on the second page

        // Delete feedback entries
        rowAffected = client.DeleteUserItemFeedbacks(feedbackOne.UserId, feedbackOne.ItemId);
        Assert.That(rowAffected.RowAffected, Is.EqualTo(1));  // Ensure one feedback entry is deleted

        rowAffected = client.DeleteUserItemFeedbacks(feedbackTwo.UserId, feedbackTwo.ItemId);
        Assert.That(rowAffected.RowAffected, Is.EqualTo(1));  // Ensure second feedback entry is deleted

        // Confirm deletion by checking if feedback is gone
        foreach (var userItem in feedbacks.Select(f => new { f.UserId, f.ItemId }))
        {
            returnedFeedbacks = client.GetUserItemFeedbacks(userItem.UserId, userItem.ItemId);
            Assert.That(returnedFeedbacks.Feedback.Count, Is.EqualTo(0));  // No feedback should remain for this user-item pair
        }
    }

    [Test]
    public async Task TestFeedbackWithCursorAsync()
    {
        // Insert feedback for two users and items
        var feedbacks = new Feedback[]
        {
        new Feedback
        {
            FeedbackType = "read",
            UserId = "user123",
            ItemId = "book789",
            Timestamp = DateTime.UtcNow.ToString()
        },
        new Feedback
        {
            FeedbackType = "liked",
            UserId = "user456",
            ItemId = "movie456",
            Timestamp = DateTime.UtcNow.ToString()
        },
        };

        // Insert feedback entries
        var rowAffected = await client.InsertFeedbackAsync(feedbacks);
        Assert.That(rowAffected.RowAffected, Is.EqualTo(2));  // Ensure two feedbacks are inserted

        // Retrieve a batch of feedbacks using pagination (cursor-based)
        var returnedFeedbacks = await client.GetFeedbacksAsync(1);  // Get first page of feedbacks

        // Get the next page of feedbacks
        returnedFeedbacks = await client.GetFeedbacksAsync(1, returnedFeedbacks.Cursor);  // Get next page using cursor
        Assert.That(returnedFeedbacks.Feedback.Count, Is.EqualTo(1));  // Ensure only one feedback is returned on the second page

        // Delete feedback entries for each user-item pair
        foreach (var userItem in feedbacks.Select(f => new { f.UserId, f.ItemId }))
        {
            rowAffected = await client.DeleteUserItemFeedbacksAsync(userItem.UserId, userItem.ItemId);
            Assert.That(rowAffected.RowAffected, Is.EqualTo(1));  // Ensure one feedback entry is deleted
        }

        // Confirm deletion by ensuring no feedback exists for each user-item pair
        foreach (var userItem in feedbacks.Select(f => new { f.UserId, f.ItemId }))
        {
            returnedFeedbacks = await client.GetUserItemFeedbacksAsync(userItem.UserId, userItem.ItemId);
            Assert.That(returnedFeedbacks.Feedback.Count, Is.EqualTo(0));  // No feedback should remain for this user-item pair
        }
    }
}
