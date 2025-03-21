using System.Text.Json.Serialization;

namespace Client.UI.Model.PlayerModel
{
    public class PlayerInfo
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonPropertyName("realm")]
        public string Realm { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonPropertyName("gender")]
        public bool Gender { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonPropertyName("faction")]
        public bool Faction { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonPropertyName("race")]
        public int Race { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonPropertyName("class")]
        public int Class { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonPropertyName("spec")]
        public int Spec { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonPropertyName("level")]
        public int Level { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonPropertyName("item_level")]
        public int ItemLevel { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonPropertyName("best_rating")]
        public double BestRating { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonPropertyName("last_login_time")]
        public long LastLoginTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonPropertyName("rating")]
        public double Rating { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonPropertyName("op_score")]
        public int OpScore { get; set; }

        public string UUID { get; set; }
    }
}