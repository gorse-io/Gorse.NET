
using System.Net;
using Gorse.NET.Models;
using Gorse.NET.Utilities;

namespace Gorse.NET.Tests;

public partial class Tests
{
    [Test]
    public void TestItems()
    {
        // Insert a user
        var item = new Item
        {
            ItemId = "12345",
            Categories = new string[] { "Books", "History" },
            Comment = "Historical books",
            IsHidden = false,
            Labels = new string[] { "New", "Popular", "Discounted" },
            TimeStamp = DateTime.UtcNow
        };
        var rowAffected = client.InsertItem(item);
        Assert.That(rowAffected.RowAffected, Is.EqualTo(1));

        // Get the item
        var returnedItem = client.GetItem(item.ItemId);

        // Assert all fields except TimeStamp
        Assert.That(returnedItem.ItemId, Is.EqualTo(item.ItemId));
        Assert.That(returnedItem.Categories, Is.EqualTo(item.Categories));
        Assert.That(returnedItem.Comment, Is.EqualTo(item.Comment));
        Assert.That(returnedItem.IsHidden, Is.EqualTo(item.IsHidden));
        Assert.That(returnedItem.Labels, Is.EqualTo(item.Labels));

        item.Categories = new string[] { "Books", "Fiction" };
        item.Comment = "Non-fiction and fantasy";
        item.IsHidden = true;
        item.Labels = new string[] { "Bestseller", "Limited Edition" };
        item.TimeStamp = DateTime.UtcNow.AddDays(1);

        rowAffected = client.UpdateItem(item.ItemId, item);
        Assert.That(rowAffected.RowAffected, Is.EqualTo(1));

        // Get the item
        returnedItem = client.GetItem(item.ItemId);

        // Assert all fields except TimeStamp
        Assert.That(returnedItem.ItemId, Is.EqualTo(item.ItemId));
        Assert.That(returnedItem.Categories, Is.EqualTo(item.Categories));
        Assert.That(returnedItem.Comment, Is.EqualTo(item.Comment));
        Assert.That(returnedItem.IsHidden, Is.EqualTo(item.IsHidden));
        Assert.That(returnedItem.Labels, Is.EqualTo(item.Labels));

        // Delete this user.
        rowAffected = client.DeleteItem(item.ItemId);
        Assert.That(rowAffected.RowAffected, Is.EqualTo(1));
        try
        {
            item = client.GetItem(item.ItemId);
            Assert.Fail();
        }
        catch (GorseException e)
        {
            Assert.That(e.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }
    }

    [Test]
    public async Task TestItemsAsync()
    {
        // Insert a user
        var item = new Item
        {
            ItemId = "12345",
            Categories = new string[] { "Books", "History" },
            Comment = "Historical books",
            IsHidden = false,
            Labels = new string[] { "New", "Popular", "Discounted" },
            TimeStamp = DateTime.UtcNow
        };
        var rowAffected = await client.InsertItemAsync(item);
        Assert.That(rowAffected.RowAffected, Is.EqualTo(1));

        // Get the item
        var returnedItem = await client.GetItemAsync(item.ItemId);

        // Assert all fields except TimeStamp
        Assert.That(returnedItem.ItemId, Is.EqualTo(item.ItemId));
        Assert.That(returnedItem.Categories, Is.EqualTo(item.Categories));
        Assert.That(returnedItem.Comment, Is.EqualTo(item.Comment));
        Assert.That(returnedItem.IsHidden, Is.EqualTo(item.IsHidden));
        Assert.That(returnedItem.Labels, Is.EqualTo(item.Labels));

        item.Categories = new string[] { "Books", "Fiction" };
        item.Comment = "Non-fiction and fantasy";
        item.IsHidden = true;
        item.Labels = new string[] { "Bestseller", "Limited Edition" };
        item.TimeStamp = DateTime.UtcNow.AddDays(1);

        rowAffected = await client.UpdateItemAsync(item.ItemId, item);
        Assert.That(rowAffected.RowAffected, Is.EqualTo(1));

        // Get the item
        returnedItem = await client.GetItemAsync(item.ItemId);

        // Assert all fields except TimeStamp
        Assert.That(returnedItem.ItemId, Is.EqualTo(item.ItemId));
        Assert.That(returnedItem.Categories, Is.EqualTo(item.Categories));
        Assert.That(returnedItem.Comment, Is.EqualTo(item.Comment));
        Assert.That(returnedItem.IsHidden, Is.EqualTo(item.IsHidden));
        Assert.That(returnedItem.Labels, Is.EqualTo(item.Labels));

        // Delete this user.
        rowAffected = await client.DeleteItemAsync(item.ItemId);
        Assert.That(rowAffected.RowAffected, Is.EqualTo(1));
        try
        {
            item = await client.GetItemAsync(item.ItemId);
            Assert.Fail();
        }
        catch (GorseException e)
        {
            Assert.That(e.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }
    }

    [Test]
    public void TestItemsBatch()
    {
        var items = new List<Item>
    {
        new Item
        {
            ItemId = "12345",
            Categories = new string[] { "Books", "History" },
            Comment = "Historical books",
            IsHidden = false,
            Labels = new string[] { "New", "Popular", "Discounted" },
            TimeStamp = DateTime.UtcNow
        },
        new Item
        {
            ItemId = "67890",
            Categories = new string[] { "Movies", "Drama" },
            Comment = "Classic drama movies",
            IsHidden = false,
            Labels = new string[] { "Classic", "Editor’s Pick" },
            TimeStamp = DateTime.UtcNow
        }
    };

        var rowAffected = client.InsertItems(items);
        Assert.That(rowAffected.RowAffected, Is.EqualTo(2));

        var returnItems = client.GetItems(1); // Assume 1 is the batch size limit
        Assert.IsNotEmpty(returnItems.Items);
        Assert.That(returnItems.Items.Count, Is.EqualTo(1));

        var cursor = returnItems.Cursor;
        var secondBatch = client.GetItems(1, cursor);
        Assert.IsNotEmpty(secondBatch.Items);
        Assert.That(secondBatch.Items.Count, Is.EqualTo(1));

        // Delete items
        foreach (var item in items)
        {
            rowAffected = client.DeleteItem(item.ItemId);
            Assert.That(rowAffected.RowAffected, Is.EqualTo(1));
        }
        // Confirm deletion
        foreach (var item in items)
        {
            try
            {
                client.GetItem(item.ItemId);
                Assert.Fail($"Expected an exception when fetching a deleted item with ID {item.ItemId}.");
            }
            catch (GorseException e)
            {
                Assert.That(e.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
            }
        }
    }

    [Test]
    public async Task TestItemsBatchAsync()
    {
        var items = new List<Item>
    {
        new Item
        {
            ItemId = "12345",
            Categories = new string[] { "Books", "History" },
            Comment = "Historical books",
            IsHidden = false,
            Labels = new string[] { "New", "Popular", "Discounted" },
            TimeStamp = DateTime.UtcNow
        },
        new Item
        {
            ItemId = "67890",
            Categories = new string[] { "Movies", "Drama" },
            Comment = "Classic drama movies",
            IsHidden = false,
            Labels = new string[] { "Classic", "Editor’s Pick" },
            TimeStamp = DateTime.UtcNow
        }
    };

        var rowAffected = await client.InsertItemsAsync(items);
        Assert.That(rowAffected.RowAffected, Is.EqualTo(2));

        var returnItems = await client.GetItemsAsync(1); // Assume 1 is the batch size limit
        Assert.IsNotEmpty(returnItems.Items);
        Assert.That(returnItems.Items.Count, Is.EqualTo(1));

        var cursor = returnItems.Cursor;
        var secondBatch = await client.GetItemsAsync(1, cursor);
        Assert.IsNotEmpty(secondBatch.Items);
        Assert.That(secondBatch.Items.Count, Is.EqualTo(1));

        // Delete items
        foreach (var item in items)
        {
            rowAffected = await client.DeleteItemAsync(item.ItemId);
            Assert.That(rowAffected.RowAffected, Is.EqualTo(1));
        }
        // Confirm deletion
        foreach (var item in items)
        {
            try
            {
                await client.GetItemAsync(item.ItemId);
                Assert.Fail($"Expected an exception when fetching a deleted item with ID {item.ItemId}.");
            }
            catch (GorseException e)
            {
                Assert.That(e.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
            }
        }
    }
}
