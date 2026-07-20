using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace MALClient.XShared.JsonModels.MAL
{
    internal class SeasonEntry
    {
        [JsonPropertyName("id")]
        public long? MalId { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("main_picture")]
        public MainPicture Picture { get; set; }

        [JsonPropertyName("num_episodes")]
        public long? Episodes { get; set; }

        [JsonPropertyName("mean")]
        public double? Score { get; set; }

        [JsonPropertyName("genres")]
        public ICollection<Genre> Genres { get; set; }

        [JsonPropertyName("num_list_users")]
        public long? MembersCount { get; set; }

        [JsonPropertyName("start_season")]
        public Season StartSeason { get; set; }
    }
}
