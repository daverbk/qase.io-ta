using System.Text.Json.Serialization;

namespace DiplomaProject.Models;

public record Defect
{
    [JsonPropertyName("id")] public long Id { get; set; }

    [JsonPropertyName("severity")] public string Severity { get; set; } = null!;

    [JsonPropertyName("actual_result")] public string ActualResult { get; set; } = null!;

    [JsonPropertyName("title")] public string Title { get; set; } = null!;
}
