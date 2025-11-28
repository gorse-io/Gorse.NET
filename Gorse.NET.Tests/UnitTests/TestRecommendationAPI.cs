using Gorse.NET.Models;

namespace Gorse.NET.Tests;

public partial class Tests
{
    [Test]
    public void TestRecommend()
    {
        client.InsertUser(new Models.User { UserId = "3000" });
        var recommendations = client.GetRecommend("3000");
        Assert.That(recommendations, Is.Not.Null);
        Assert.That(recommendations[0], Is.EqualTo("315"));
        Assert.That(recommendations[1], Is.EqualTo("1432"));
        Assert.That(recommendations[2], Is.EqualTo("918"));
    }

    [Test]
    public async Task TestRecommendAsync()
    {
        await client.InsertUserAsync(new User { UserId = "4000" });
        var recommendations = await client.GetRecommendAsync("4000");
        Assert.That(recommendations, Is.Not.Null);
        Assert.That(recommendations[0], Is.EqualTo("315"));
        Assert.That(recommendations[1], Is.EqualTo("1432"));
        Assert.That(recommendations[2], Is.EqualTo("918"));
    }
}
