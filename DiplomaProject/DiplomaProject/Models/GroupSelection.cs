using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DiplomaProject.Models;

public record GroupSelection<T>
{
    [JsonPropertyName("count")] public int Count { get; set; }

    [JsonPropertyName("entities")] public List<T>? Entities { get; set; } = new();
}
