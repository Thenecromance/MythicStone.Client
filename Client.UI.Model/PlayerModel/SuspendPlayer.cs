using System.Text.Json.Serialization;

namespace Client.UI.Model.PlayerModel;

public class SuspendPlayer
{
    [JsonPropertyName("user_name")] public string Name { get; set; }
    [JsonPropertyName("realm")] public string Realm { get; set; }
    [JsonPropertyName("reason")] public string Reason { get; set; }
}