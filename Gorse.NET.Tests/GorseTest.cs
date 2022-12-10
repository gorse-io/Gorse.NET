namespace Gorse.NET.Tests;

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
        Assert.AreEqual(rowAffected.RowAffected, 1);
    }
}
