using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DiplomaProject.Models;

public record GroupSelection<T>
{
    [JsonPropertyName("total")] public int Total { get; set; }

    [JsonPropertyName("filtered")] public int Filtered { get; set; }

    [JsonPropertyName("count")] public int Count { get; set; }

    [JsonPropertyName("entities")] public List<T>? Entities { get; set; } = new();
}
