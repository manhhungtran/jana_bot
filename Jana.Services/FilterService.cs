using System.Linq;
using Discord;
using Discord.WebSocket;
using System.IO;
using Jana.Services;

namespace Jana.Services
{
    public class FilterService
    {
        private readonly Filter _filter;

        public FilterService()
        {
            // TODO: pull filter from database
        }

        public bool IsWhitelisted(IChannel channel)
        {
            return _filter.ChannelWhitelist.Any(v => channel.Id == v);
        }

        public bool IsElevated(SocketGuildUser user)
        {
            if (!_filter.GuildUserMap.TryGetValue(user.Guild.Id, out ulong[] users))
            {
                return false;
            }

            return users.Any(u => u == user.Id);
        }
    }
}
