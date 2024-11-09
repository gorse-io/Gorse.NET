
using System.Net;
using Gorse.NET.Models;
using Gorse.NET.Utilities;

namespace Gorse.NET.Tests;

public partial class Tests
{

    [Test]
    public void TestUserLifecycle()
    {
        // Insert a new user
        var user = new User
        {
            UserId = "user123",
            Labels = new string[] { "outgoing", "optimistic", "creative" },
            Comment = "Initial user creation",
            Subscribe = new string[] { "news", "updates", "offers" }
        };
        var rowAffected = client.InsertUser(user);
        Assert.That(rowAffected.RowAffected, Is.EqualTo(1));

        // Retrieve this newly inserted user
        var returnUser = client.GetUser("user123");
        Assert.That(returnUser, Is.EqualTo(user));

        // Update the user with new details
        user.Labels = new string[] { "introverted", "thoughtful", "analytical" };
        user.Comment = "Updated user profile";
        user.Subscribe = new string[] { "promotions", "alerts", "newsletters" };
        rowAffected = client.UpdateUser(user.UserId, user);
        Assert.That(rowAffected.RowAffected, Is.EqualTo(1));

        // Retrieve the updated user
        returnUser = client.GetUser("user123");
        Assert.That(returnUser, Is.EqualTo(user));

        // Delete the user
        rowAffected = client.DeleteUser("user123");
        Assert.That(rowAffected.RowAffected, Is.EqualTo(1));

        // Try to retrieve the deleted user, should throw an exception
        try
        {
            returnUser = client.GetUser("user123");
            Assert.Fail();
        }
        catch (GorseException e)
        {
            Assert.That(e.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));  // Check for "not found" error
        }
    }

    [Test]
    public async Task TestUserLifecycleAsync()
    {
        // Insert a new user with personality labels
        var user = new User
        {
            UserId = "user123",
            Labels = new string[] { "outgoing", "optimistic", "creative" }
        };
        var rowAffected = await client.InsertUserAsync(user);
        Assert.That(rowAffected.RowAffected, Is.EqualTo(1));

        // Retrieve this newly inserted user
        var returnUser = await client.GetUserAsync("user123");
        Assert.That(returnUser, Is.EqualTo(user));

        // Update the user with new details
        user.Labels = new string[] { "introverted", "thoughtful", "analytical" };
        user.Comment = "Updated user profile";  // Updated comment
        user.Subscribe = new string[] { "promotions", "alerts", "newsletters" };
        rowAffected = await client.UpdateUserAsync(user.UserId, user);
        Assert.That(rowAffected.RowAffected, Is.EqualTo(1));

        // Retrieve the updated user
        returnUser = await client.GetUserAsync("user123");
        Assert.That(returnUser, Is.EqualTo(user));

        // Delete the user
        rowAffected = await client.DeleteUserAsync("user123");
        Assert.That(rowAffected.RowAffected, Is.EqualTo(1));

        // Try to retrieve the deleted user, should throw an exception
        try
        {
            returnUser = await client.GetUserAsync("user123");
            Assert.Fail();
        }
        catch (GorseException e)
        {
            Assert.That(e.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }
    }


    [Test]
    public void TestUsersBatch()
    {
        // Insert multiple users with personality labels
        var users = new List<User>(){
            new User{
                UserId = "user123",
                Labels = new string[] { "outgoing", "optimistic", "creative" },
                Comment = "Initial user creation",
                Subscribe = new string[] { "news", "updates", "offers" }
            },
            new User{
                UserId = "user456",
                Labels = new string[] { "introverted", "thoughtful", "analytical" },
                Comment = "Second user creation",
                Subscribe = new string[] { "promotions", "alerts", "newsletters" }
            },
        };

        // Insert users in batch
        var rowAffected = client.InsertUsers(users);
        Assert.That(rowAffected.RowAffected, Is.EqualTo(2));

        // Get the first user
        var returnUsers = client.GetUsers(1);
        Assert.IsNotEmpty(returnUsers.Users);

        // Get the second user using a cursor
        returnUsers = client.GetUsers(1, returnUsers.Cursor);
        Assert.IsNotEmpty(returnUsers.Users);

        // Delete users
        foreach (var userId in users.Select(u => u.UserId))
        {
            rowAffected = client.DeleteUser(userId);
            Assert.That(rowAffected.RowAffected, Is.EqualTo(1));
        }

        // Confirm deletion of users
        foreach (var userId in users.Select(u => u.UserId))
        {
            try
            {
                client.GetUser(userId);
                Assert.Fail();
            }
            catch (GorseException e)
            {
                Assert.That(e.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
            }
        }
    }

    [Test]
    public async Task TestUsersBatchAsync()
    {
        // Insert multiple users with personality labels
        var users = new List<User>(){
            new User{
                UserId = "user123",
                Labels = new string[] { "outgoing", "optimistic", "creative" },
                Comment = "Initial user creation",
                Subscribe = new string[] { "news", "updates", "offers" }
            },
            new User{
                UserId = "user456",  // Descriptive UserId
                Labels = new string[] { "introverted", "thoughtful", "analytical" },
                Comment = "Second user creation",
                Subscribe = new string[] { "promotions", "alerts", "newsletters" }
            },
        };

        // Insert users in batch
        var rowAffected = await client.InsertUsersAsync(users);
        Assert.That(rowAffected.RowAffected, Is.EqualTo(2));

        // Get the first user
        var returnUsers = await client.GetUsersAsync(1);
        Assert.IsNotEmpty(returnUsers.Users);

        // Get the second user using a cursor
        returnUsers = await client.GetUsersAsync(1, returnUsers.Cursor);
        Assert.IsNotEmpty(returnUsers.Users);

        // Delete the first user
        rowAffected = await client.DeleteUserAsync("user123");
        Assert.That(rowAffected.RowAffected, Is.EqualTo(1));

        // Delete the second user
        rowAffected = await client.DeleteUserAsync("user456");
        Assert.That(rowAffected.RowAffected, Is.EqualTo(1));

        // Confirm deletion of users
        foreach (var userId in users.Select(u => u.UserId))
        {
            try
            {
                await client.GetUserAsync(userId);  // Async call to ensure proper behavior
                Assert.Fail();
            }
            catch (GorseException e)
            {
                Assert.That(e.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));  // Ensure users are deleted
            }
        }
    }
}
