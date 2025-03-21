using System.Text.Json.Serialization;

namespace Client.UI.Model.PlayerModel;

public class DungeonScoresItem
{
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("dungeon_id")]
    public int DungeonId { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("season")]
    public int Season { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("completed_timestamp")]
    public long CompletedTimestamp { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("duration")]
    public int Duration { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("is_completed")]
    public bool IsCompleted { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("keystone_level")]
    public int KeystoneLevel { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("rating")]
    public double Rating { get; set; }
}