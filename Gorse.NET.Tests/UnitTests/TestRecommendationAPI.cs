
using StackExchange.Redis;

namespace Gorse.NET.Tests;

public partial class Tests
{
    [Test]
    public void TestRecommend()
    {
        var db = redis.GetDatabase();
        db.SortedSetAdd("offline_recommend/user321", new SortedSetEntry[]
        {
            new SortedSetEntry("book789", 1),
            new SortedSetEntry("movie456", 2),
            new SortedSetEntry("game321", 3)
        });

        // Get recommendations for user "user321"
        var items = client.GetRecommend("user321");

        // Assert that the items are returned in descending order of rank (highest rank first)
        Assert.That(items, Is.EqualTo(new string[] { "game321", "movie456", "book789" }));
    }

    [Test]
    public void TestRecommend_Returns_Null()
    {
        // Attempt to get recommendations for a user with no data
        var items = client.GetRecommend("user999");

        // Assert that the recommendation result is null for this user
        Assert.IsNull(items);
    }

    [Test]
    public async Task TestRecommendAsync()
    {
        var db = redis.GetDatabase();

        // Add some sorted set entries for user "user4321" with ranked items
        await db.SortedSetAddAsync("offline_recommend/user4321", new SortedSetEntry[]
        {
        new SortedSetEntry("book789", 1),
        new SortedSetEntry("movie456", 2),
        new SortedSetEntry("game321", 3)
        });

        // Get the recommendations for user "user4321"
        var items = await client.GetRecommendAsync("user4321");

        // Assert that the items are returned in the correct order based on the ranking
        Assert.That(items, Is.EqualTo(new string[] { "game321", "movie456", "book789" }));
    }

    [Test]
    public async Task TestRecommendAsync_Returns_Null()
    {
        // Attempt to get recommendations for a user with no data
        var items = await client.GetRecommendAsync("user999");

        // Assert that the recommendation result is null for this user
        Assert.IsNull(items);
    }

    [Test]
    public void TestUserNeighbors()
    {
        // Get neighbors for a user with a meaningful UserId
        var users = client.GetUserNeighbors("user456");

        // Assert that the neighbors list is not null
        Assert.IsNotNull(users);
    }

    [Test]
    public async Task TestUserNeighborsAsync()
    {
        // Get neighbors for a user with a meaningful UserId
        var users = await client.GetUserNeighborsAsync("user789");

        // Assert that the neighbors list is not null
        Assert.IsNotNull(users);
    }

}
