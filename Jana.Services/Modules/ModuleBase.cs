using Discord;
using Discord.Commands;
using Jana.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jana.Services.Modules
{
    public class ModuleBase : ModuleBase<SocketCommandContext>
    {
        //public static readonly Emoji TagNotFound = EmojiExtensions.FromText("mag_right");
        //public static readonly Emoji Pass = EmojiExtensions.FromText("ok_hand");
        //public static readonly Emoji Fail = EmojiExtensions.FromText("octagonal_sign");
        //public static readonly Emoji Removed = EmojiExtensions.FromText("put_litter_in_its_place");

        public JanaContext Database { get; set; }
    }
}
