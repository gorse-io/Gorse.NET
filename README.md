# Gorse.NET

[![CI](https://github.com/gorse-io/Gorse.NET/actions/workflows/ci.yml/badge.svg)](https://github.com/gorse-io/Gorse.NET/actions/workflows/ci.yml)

.NET SDK for gorse recommender system

## Usage

```c#
using Gorse.NET;

var client = new Gorse("http://127.0.0.1:8087", "api_key");

client.InsertFeedback(new Feedback[]
{
    new Feedback{FeedbackType="star", UserId="bob", ItemId="vuejs:vue", Timestamp="2022-02-24"},
    new Feedback{FeedbackType="star", UserId="bob", ItemId="d3:d3", Timestamp="2022-02-25"},
    new Feedback{FeedbackType="star", UserId="bob", ItemId="dogfalo:materialize", Timestamp="2022-02-26"},
    new Feedback{FeedbackType="star", UserId="bob", ItemId="mozilla:pdf.js", Timestamp="2022-02-27"},
    new Feedback{FeedbackType="star", UserId="bob", ItemId="moment:moment", Timestamp="2022-02-28"},
});

client.GetRecommend("10");
```
