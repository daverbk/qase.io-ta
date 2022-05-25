using System.Text.Json.Serialization;

namespace DiplomaProject.Models;

public record TestCase
{
    [JsonPropertyName("id")] public long Id { get; set; }

    [JsonPropertyName("title")] public string Title { get; set; } = null!;
}
