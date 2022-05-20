using System.Text.Json.Serialization;

namespace DiplomaProject.Models;

public record Project
{
    [JsonPropertyName("title")] public string Title { get; set; } = null!;

    [JsonPropertyName("code")] public string Code { get; set; } = null!;

    [JsonPropertyName("description")] public string Description { get; set; } = null!;

    [JsonPropertyName("access")] public string Access { get; set; } = null!;
}
