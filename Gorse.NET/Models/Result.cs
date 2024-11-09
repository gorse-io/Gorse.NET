using System.Text.Json;

namespace Gorse.NET.Models;

public class Result
{
    public int RowAffected { set; get; }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}