using System.Text.Json.Serialization;

namespace Client.Core;

public class Authorization
{
    [JsonPropertyName("uid")] public string UID { get; set; }
    [JsonPropertyName("token")] public string Token { get; set; }
}