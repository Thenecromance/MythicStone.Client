using System.Text.Json.Serialization;

namespace Client.UI.Model.BlackList;

public class SuspendPlayer
{
    [JsonPropertyName("user_name")] public string Name { get; set; }
    [JsonPropertyName("realm")] public string? Realm { get; set; }
    [JsonPropertyName("reason")] public string Reason { get; set; }
}

public class BlackListOperationResult
{
    [JsonPropertyName("success")] public bool Success { get; set; }
    [JsonPropertyName("message")] public string? Message { get; set; }
}