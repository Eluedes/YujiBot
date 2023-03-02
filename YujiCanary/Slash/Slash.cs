using DSharpPlus.Entities;
using DSharpPlus;
using DSharpPlus.SlashCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus.SlashCommands.Attributes;

namespace YujiCanary.Slash
{
    public class SlashCommands : ApplicationCommandModule
    {
        [SlashCommand("ping", "Teste de Sobrevivencia do bot.")]
        public async Task TestCommand(InteractionContext ctx)
        {
            await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().WithContent("pong"));
        }

        [SlashCommand("ban", "Bans a user")]
        [SlashRequirePermissions(Permissions.BanMembers)]
        public async Task Ban(InteractionContext ctx, [Option("user", "User to ban")] DiscordUser user,
    [Choice("None", 0)]
    [Choice("1 dia", 1)]
    [Choice("1 semana", 7)]
    [Option("deletedays", "Coloque o tempo de banimento")] long deleteDays = 0)
        {
            await ctx.Guild.BanMemberAsync(user.Id, (int)deleteDays);
            await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().WithContent($"{user.Username} Banido"));
        }
    }
}
