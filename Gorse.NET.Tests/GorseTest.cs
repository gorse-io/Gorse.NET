namespace Gorse.NET.Tests;

using System.Net;

public class Tests
{
    private const string ENDPOINT = "http://127.0.0.1:8088";
    private const string API_KEY = "zhenghaoz";

    private Gorse client = new Gorse(ENDPOINT, API_KEY);

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
}
