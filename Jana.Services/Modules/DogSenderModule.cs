using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jana.Services.Modules
{
    public class DogSenderModule : ModuleBase
    {
        [Command("dog pls")]
        public async Task SendDogAsync()
        {
            await ReplyAsync("Soon there will be doggo pictures ayyy :)");
        }
    }
}
