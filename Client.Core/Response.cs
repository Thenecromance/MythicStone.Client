using System.Text.Json.Serialization;

namespace Client.Core;

public class Message
{
    [JsonPropertyName("level")] public int Level { get; set; }
    [JsonPropertyName("content")] public string Content { get; set; }
}

public class Response<T>
{
    [JsonPropertyName("message")] public Message? Message { get; set; }
    [JsonPropertyName("data")] public T? Data { get; set; }
}