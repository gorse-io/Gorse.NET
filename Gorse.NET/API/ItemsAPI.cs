using Gorse.NET.Models;
using RestSharp;

namespace Gorse.NET;

public partial class Gorse
{
    public Result InsertItem(Item item)
    {
        return _client.Request<Result, Item>(Method.Post, "api/item", item)!;
    }

    public Task<Result> InsertItemAsync(Item item)
    {
        return _client.RequestAsync<Result, Item>(Method.Post, "api/item", item)!;
    }

    public Result InsertItems(List<Item> items)
    {
        return _client.Request<Result, List<Item>>(Method.Post, "api/items", items)!;
    }

    public Task<Result> InsertItemsAsync(List<Item> items)
    {
        return _client.RequestAsync<Result, List<Item>>(Method.Post, "api/items", items)!;
    }

    public Item GetItem(string itemId)
    {
        return _client.Request<Item, Object>(Method.Get, "api/item/" + itemId, null)!;
    }

    public Task<Item> GetItemAsync(string itemId)
    {
        return _client.RequestAsync<Item, Object>(Method.Get, "api/item/" + itemId, null)!;
    }

    public ItemsResponse GetItems(int n, string cursor = "")
    {
        return _client.Request<ItemsResponse, Object>(Method.Get, $"api/items?n={n}&cursor={cursor}", null)!;
    }

    public Task<ItemsResponse> GetItemsAsync(int n, string cursor = "")
    {
        return _client.RequestAsync<ItemsResponse, Object>(Method.Get, $"api/items?n={n}&cursor={cursor}", null)!;
    }

    public Result DeleteItem(string itemId)
    {
        return _client.Request<Result, Object>(Method.Delete, "api/item/" + itemId, null)!;
    }

    public Task<Result> DeleteItemAsync(string itemId)
    {
        return _client.RequestAsync<Result, Object>(Method.Delete, "api/item/" + itemId, null)!;
    }

    public Result UpdateItem(string itemId, Item itemToUpdate)
    {
        return _client.Request<Result, Object>(Method.Patch, $"api/item/{itemId}", itemToUpdate)!;
    }

    public Task<Result> UpdateItemAsync(string itemId, Item itemToUpdate)
    {
        return _client.RequestAsync<Result, Object>(Method.Patch, $"api/item/{itemId}", itemToUpdate)!;
    }
}