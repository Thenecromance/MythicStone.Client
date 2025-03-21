using System.Text.Json.Serialization;

namespace Client.UI.Model.PlayerModel;

public class Detail
{
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("info")]
    public PlayerInfo Info { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("best_score")]
    public PlayerBestScore BestScore { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("current_period")]
    public PeriodRating PeriodRating { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("current")]
    public DungeonRating DungeonRating { get; set; }
}