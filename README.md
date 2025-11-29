# Gorse.NET

[![CI](https://github.com/gorse-io/Gorse.NET/actions/workflows/ci.yml/badge.svg)](https://github.com/gorse-io/Gorse.NET/actions/workflows/ci.yml)
[![Nuget (with prereleases)](https://img.shields.io/nuget/vpre/Gorse.NET)](https://www.nuget.org/packages/Gorse.NET/)
[![Nuget](https://img.shields.io/nuget/dt/Gorse.NET)](https://www.nuget.org/packages/Gorse.NET/)

.NET SDK for gorse recommender system

## Install

- Install via .NET CLI:

```bash
dotnet add package Gorse.NET
```

- Install via NuGet Package Manager:

```bash
NuGet\Install-Package Gorse.NET
```

## Usage

```c#
using Gorse.NET;
using Gorse.NET.Models;

var client = new Gorse("http://127.0.0.1:8087", "api_key");

// Insert a user
client.InsertUser(new User
{
    UserId = "100",
    Labels = new { gender = "M", occupation = "engineer" },
    Comment = "user comment",
});

// Insert an item
client.InsertItem(new Item
{
    ItemId = "200",
    IsHidden = false,
    Labels = new Dictionary<string, object>
    {
        { "embedding", new List<double> { 0.1, 0.2, 0.3 } }
    },
    Categories = new[] { "Comedy", "Animation" },
    TimeStamp = DateTime.UtcNow,
    Comment = "item comment",
});

// Insert feedback
client.InsertFeedback(new Feedback[]
{
    new Feedback{FeedbackType="star", UserId="100", ItemId="200", Timestamp=DateTime.UtcNow.ToString("yyyy-MM-dd")},
    new Feedback{FeedbackType="like", UserId="100", ItemId="200", Timestamp=DateTime.UtcNow.ToString("yyyy-MM-dd")},
});

// Get recommendations
client.GetRecommend("100");
```
