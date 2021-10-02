using System.Collections.Generic;
using Newtonsoft.Json;

namespace Jana.Services
{
    public class Filter
    {
        [JsonProperty("whitelist")]
        public ulong[] ChannelWhitelist { get; set; }

        [JsonProperty("guild_user_map")]
        public Dictionary<ulong, ulong[]> GuildUserMap { get; set; }
    }
}
