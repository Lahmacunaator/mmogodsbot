using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Discord.Events;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using Microsoft.Extensions.Logging;

namespace Discord;

public class DiscordBot
{
        internal DiscordClient Client { get; private set; }
        private CommandsNextExtension Commands { get; set; }
        private string? Token { get; set; }

        /// <summary>
        /// Constructor for DiscordBot instance with Configuration options.
        /// </summary>
        /// <param name="options"></param>
        public DiscordBot()
        {
            Token = Environment.GetEnvironmentVariable("DISCORD_TOKEN");
            Client = new DiscordClient(new DiscordConfiguration
            {
                Token = Token,
                TokenType = TokenType.Bot,
                Intents = DiscordIntents.All,
#if DEBUG
                MinimumLogLevel = LogLevel.Trace
#else
                MinimumLogLevel = LogLevel.Information
#endif
            });

            // next, let's hook some events, so we know
            // what's going on
            Client.Ready += DiscordClientEvents.Ready;
            Client.GuildAvailable += DiscordClientEvents.GuildAvailable;
            Client.GuildDownloadCompleted += DiscordClientEvents.GuildDownloadCompleted;
            Client.ClientErrored += DiscordClientEvents.Error;

            List<string> prefixes = new List<string>();
            prefixes.Add("+");
            Commands = Client.UseCommandsNext(new CommandsNextConfiguration
            {
                StringPrefixes = prefixes,
                EnableMentionPrefix = true,
                EnableDms = true
            });

            // Hook into command events
            Commands.CommandExecuted += DiscordCommandEvents.OnCommandExecutedAsync;
            Commands.CommandErrored += DiscordCommandEvents.OnCommandErroredAsync;
        }
        /// <summary>
        /// Start the bot on a thread.
        /// </summary>
        /// <param name="stoppingToken"></param>
        public async Task StartAsync(CancellationToken stoppingToken)
        {
            await Client
           .ConnectAsync()
           .ConfigureAwait(false);
            
            await Task.Delay(-1, stoppingToken)
                .ConfigureAwait(false);
        }
        
        /// <summary>
        /// Disconnects client instance from the discord websocket gateway.
        /// </summary>
        public async Task StopAsync()
        {
            await Client.DisconnectAsync().ConfigureAwait(false);
        }
}