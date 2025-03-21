using System.Text.Json.Serialization;

namespace Client.UI.Model.PlayerModel;

public class DungeonRating
{
    [JsonPropertyName("dungeon_id")] public int DungeonId { get; set; }

    [JsonPropertyName("completed_timestamp")]
    public long CompleteTime { get; set; }

    [JsonPropertyName("duration")] public int Duration { get; set; }
    [JsonPropertyName("is_completed")] public bool IsComplete { get; set; }
    [JsonPropertyName("keystone_level")] public int KeystoneLevel { get; set; }
    [JsonPropertyName("rating")] public double Rating { get; set; }
}

public class PeriodRating
{
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("period")]
    public int Period { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("season")]
    public int Season { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("dungeons")]
    public List<DungeonRating> Dungeons { get; set; }
}