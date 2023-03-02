using System;
using System.Reflection;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using YujiCanary.Slash;
using DSharpPlus.Interactivity.Extensions;

namespace YujiCanary
{
     class Program
    {
        static void Main(string[] args)
        {
            MainAsync().GetAwaiter().GetResult();
        }

      static async Task MainAsync()
        {
            var discord = new DiscordClient(new DiscordConfiguration()
            {
                Token = "TOUR_TOKEN",
                TokenType = TokenType.Bot,
                Intents = DiscordIntents.AllUnprivileged | DiscordIntents.MessageContents,
                ReconnectIndefinitely = true,
                GatewayCompressionLevel = GatewayCompressionLevel.Stream,
                AutoReconnect = true,
                UseRelativeRatelimit = true,
               
            });

            var commands = discord.UseCommandsNext(new CommandsNextConfiguration()
            {
                StringPrefixes = new[] { "!" },
                EnableDefaultHelp = false,
                EnableDms = false,
              
            });

            commands.RegisterCommands(Assembly.GetExecutingAssembly());

            var slash = discord.UseSlashCommands();
            slash.RegisterCommands<SlashCommands>();

            await discord.ConnectAsync(new DiscordActivity("!ajuda", ActivityType.Playing), UserStatus.Idle);
            await Task.Delay(-1);
        }

    }
}
