using System.Text.Json;
using Gorse.NET.Models;

namespace Gorse.NET.Tests;

public partial class Tests
{
    [Test]
    public void TestItems()
    {
        var item = new Item
        {
            ItemId = "2000",
            IsHidden = true,
            Labels = new Dictionary<string, object>
            {
                { "embedding", new List<double> { 0.1, 0.2, 0.3 } }
            },
            Categories = new[] { "Comedy", "Animation" },
            TimeStamp = DateTime.UtcNow,
            Comment = "Minions (2015)",
        };
        var rowAffected = client.InsertItem(item);
        Assert.That(rowAffected.RowAffected, Is.EqualTo(1));
        var resp = client.GetItem("2000");
        Assert.That(resp.ItemId, Is.EqualTo(item.ItemId));
        Assert.That(resp.IsHidden, Is.EqualTo(item.IsHidden));
        Assert.That(JsonSerializer.Serialize(resp.Labels), Is.EqualTo(JsonSerializer.Serialize(item.Labels)));
        Assert.That(resp.Categories, Is.EqualTo(item.Categories));
        Assert.That(resp.Comment, Is.EqualTo(item.Comment));

        var deleteAffect = client.DeleteItem("2000");
        Assert.That(deleteAffect.RowAffected, Is.EqualTo(1));
        try
        {
            resp = client.GetItem("2000");
            Assert.Fail("Expected exception not thrown");
        }
        catch (Exception ex)
        {
            Assert.That(ex.Message, Is.EqualTo("2000: item not found"));
        }
    }

    [Test]
    public async Task TestItemsAsync()
    {
        var item = new Item
        {
            ItemId = "2000",
            IsHidden = true,
            Labels = new Dictionary<string, object>
            {
                { "embedding", new List<double> { 0.1, 0.2, 0.3 } }
            },
            Categories = new[] { "Comedy", "Animation" },
            TimeStamp = DateTime.UtcNow,
            Comment = "Minions (2015)",
        };
        var rowAffected = await client.InsertItemAsync(item);
        Assert.That(rowAffected.RowAffected, Is.EqualTo(1));
        var resp = await client.GetItemAsync("2000");
        Assert.That(resp.ItemId, Is.EqualTo(item.ItemId));
        Assert.That(resp.IsHidden, Is.EqualTo(item.IsHidden));
        Assert.That(JsonSerializer.Serialize(resp.Labels), Is.EqualTo(JsonSerializer.Serialize(item.Labels)));
        Assert.That(resp.Categories, Is.EqualTo(item.Categories));
        Assert.That(resp.Comment, Is.EqualTo(item.Comment));

        var deleteAffect = await client.DeleteItemAsync("2000");
        Assert.That(deleteAffect.RowAffected, Is.EqualTo(1));
        try
        {
            resp = await client.GetItemAsync("2000");
            Assert.Fail("Expected exception not thrown");
        }
        catch (Exception ex)
        {
            Assert.That(ex.Message, Is.EqualTo("2000: item not found"));
        }
    }
}
