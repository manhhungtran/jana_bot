﻿using System;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace Jana.Services
{
    public class CommandHandlingService
    {
        private readonly CommandService _commands;
        private readonly IConfiguration _config;
        private readonly DiscordSocketClient _discord;
        private IServiceProvider _provider;
        private FilterService _filterService;

        public CommandHandlingService(IServiceProvider provider, DiscordSocketClient discord, CommandService commands, IConfiguration config, FilterService filterService)
        {
            _commands = commands;
            _config = config;
            _discord = discord;
            _provider = provider;
            _filterService = filterService;

            _discord.MessageReceived += MessageReceived;
        }

        public async Task InitializeAsync(IServiceProvider provider)
        {
            _provider = provider;
            await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), provider);
        }

        private async Task MessageReceived(SocketMessage rawMessage)
        {
            // Ignore system messages and messages from bots
            if (rawMessage is not SocketUserMessage message)
            {
                return;
            }

            if (message.Source != MessageSource.User)
            {
                return;
            }

            if (!_filterService.IsWhitelisted(message.Channel))
            {
                return;
            }

            int argPos = 0;
            if (!(message.HasMentionPrefix(_discord.CurrentUser, ref argPos) || message.HasStringPrefix(_config["prefix"], ref argPos)))
            {
                return;
            }

            var context = new SocketCommandContext(_discord, message);
            var result = await _commands.ExecuteAsync(context, argPos, _provider);

            if (result.Error.HasValue && result.Error.Value != CommandError.UnknownCommand)
            {
                await context.Channel.SendMessageAsync(result.ToString());
            }
        }
    }
}
