using System.Text.Json.Serialization;

namespace Client.UI.Model.PlayerModel
{
    public class PlayerBestScore
    {
        /// <summary>
        /// 卷毛卷毛哒
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// 布兰卡德
        /// </summary>
        [JsonPropertyName("realm")]
        public string Realm { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonPropertyName("season")]
        public int Season { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonPropertyName("score")]
        public int Score { get; set; }
    }
}