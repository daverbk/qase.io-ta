using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DiplomaProject.Models;

public record TestCase
{
    [JsonPropertyName("id")] public long Id { get; set; }

    [JsonPropertyName("description")] public string Description { get; set; } = null!;

    [JsonPropertyName("preconditions")] public string Preconditions { get; set; } = null!;

    [JsonPropertyName("postconditions")] public string Postconditions { get; set; } = null!;

    [JsonPropertyName("title")] public string Title { get; set; } = null!;
}
