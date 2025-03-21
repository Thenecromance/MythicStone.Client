using System.Text.Json.Serialization;

namespace Client.UI.Model.Dungeon;

public class DungeonInfo
{
    // {
    //     "name": "暴富矿区！！",
    //     "level_up": [
    //     1980000,
    //     1584000,
    //     1188000
    //         ],
    //     "short_cut": "https://render.worldofwarcraft.com/us/zones/the-motherlode-small.jpg"
    // }

    [JsonPropertyName("name")] public string Name { set; get; }
    [JsonPropertyName("id")] public int Id { set; get; }
    [JsonPropertyName("level_up")] public List<int> LevelUp { set; get; }
    [JsonPropertyName("short_cut")] public string ShortCut { set; get; }
}