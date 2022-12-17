namespace Gorse.NET.Tests;

using StackExchange.Redis;
using System.Net;

public class Tests
{
    private const string ENDPOINT = "http://127.0.0.1:8088";
    private const string API_KEY = "zhenghaoz";

    private Gorse client = new Gorse(ENDPOINT, API_KEY);
    private ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(
        new ConfigurationOptions
        {
            EndPoints = { "127.0.0.1:6379" }
        });

    [Test]
    public void TestUsers()
    {
        // Insert a user
        var user = new User { UserId = "1", Labels = new string[] { "a", "b", "c" } };
        var rowAffected = client.InsertUser(user);
        Assert.That(rowAffected.RowAffected, Is.EqualTo(1));
        // Get this user.
        var returnUser = client.GetUser("1");
        Assert.That(returnUser, Is.EqualTo(user));
        // Delete this user.
        rowAffected = client.DeleteUser("1");
        Assert.That(rowAffected.RowAffected, Is.EqualTo(1));
        try
        {
            returnUser = client.GetUser("1");
            Assert.Fail();
        } catch (GorseException e)
        {
            Assert.That(e.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }
    }

    [Test]
    public void TestFeedback()
    {
        // Insert feedback
        var feedbacks = new Feedback[]
        {
            new Feedback{FeedbackType="read", UserId="10", ItemId="3", Timestamp="2022-11-20T13:55:27Z"},
            new Feedback{FeedbackType="read", UserId="10", ItemId="4", Timestamp="2022-11-20T13:55:27Z"},
        };
        var rowAffected = client.InsertFeedback(feedbacks);
        Assert.That(rowAffected.RowAffected, Is.EqualTo(2));
    }

    [Test]
    public void TestRecommend()
    {
        var db = redis.GetDatabase();
        db.SortedSetAdd("offline_recommend/10", new SortedSetEntry[]
        {
            new SortedSetEntry("10", 1),
            new SortedSetEntry("20", 2),
            new SortedSetEntry("30", 3)
        });
        var items = client.GetRecommend("10");
        Assert.That(items, Is.EqualTo(new string[] { "10", "20", "30" }));
    }
}
