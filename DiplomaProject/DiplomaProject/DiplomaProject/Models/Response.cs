using System.Text.Json.Serialization;

namespace DiplomaProject.Models;

public record Response<T>
{
    [JsonPropertyName("status")] public bool Status { get; set; }

    [JsonPropertyName("result")] public T Result { get; set; } = default!;
}
