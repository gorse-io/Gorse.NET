using System.Text.Json;
using Gorse.NET.Models;

namespace Gorse.NET.Tests;

public partial class Tests
{

    [Test]
    public void TestUsers()
    {
        User user = new User
        {
            UserId = "1000",
            Labels = new { gender = "M", occupation = "engineer" },
            Comment = "zhenghaoz",
        };
        var rowAffected = client.InsertUser(user);
        Assert.That(rowAffected.RowAffected, Is.EqualTo(1));
        var resp = client.GetUser("1000");
        Assert.That(resp.UserId, Is.EqualTo(user.UserId));
        Assert.That(JsonSerializer.Serialize(resp.Labels), Is.EqualTo(JsonSerializer.Serialize(user.Labels)));
        Assert.That(resp.Comment, Is.EqualTo(user.Comment));

        var deleteAffect = client.DeleteUser("1000");
        Assert.That(deleteAffect.RowAffected, Is.EqualTo(1));
        try
        {
            resp = client.GetUser("1000");
            Assert.Fail("Expected exception not thrown");
        }
        catch (Exception ex)
        {
            Assert.That(ex.Message, Is.EqualTo("1000: user not found"));
        }
    }

    [Test]
    public async Task TestUsersAsync()
    {
        User user = new User
        {
            UserId = "1000",
            Labels = new { gender = "M", occupation = "engineer" },
            Comment = "zhenghaoz",
        };
        var rowAffected = await client.InsertUserAsync(user);
        Assert.That(rowAffected.RowAffected, Is.EqualTo(1));
        var resp = await client.GetUserAsync("1000");
        Assert.That(resp.UserId, Is.EqualTo(user.UserId));
        Assert.That(JsonSerializer.Serialize(resp.Labels), Is.EqualTo(JsonSerializer.Serialize(user.Labels)));
        Assert.That(resp.Comment, Is.EqualTo(user.Comment));

        var deleteAffect = await client.DeleteUserAsync("1000");
        Assert.That(deleteAffect.RowAffected, Is.EqualTo(1));
        try
        {
            resp = await client.GetUserAsync("1000");
            Assert.Fail("Expected exception not thrown");
        }
        catch (Exception ex)
        {
            Assert.That(ex.Message, Is.EqualTo("1000: user not found"));
        }
    }
}
