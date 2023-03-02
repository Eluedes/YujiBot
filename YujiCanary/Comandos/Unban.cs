using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using DSharpPlus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus.Exceptions;

namespace YujiCanary.Comandos
{
    public class Unban : BaseCommandModule
    {
        [Command("unban")]
        [Description("Comando para desbanir o usuario")]
        [RequirePermissions(Permissions.BanMembers)]
        public async Task UnbanCommand(CommandContext ctx, DiscordUser user, [RemainingText] string reason)
        {
            if (user == null)
            {
                if (string.IsNullOrWhiteSpace(reason))
                {
                    await ctx.RespondAsync("Você precisa fornecer um motivo para desbanir o usuário.");
                    return;
                }

                try
                {
                    await ctx.Guild.UnbanMemberAsync(user, reason);
                    await ctx.RespondAsync($"O usuário {user.Username}#{user.Discriminator} foi desbanido com sucesso.");
                }
                catch (NotFoundException)
                {
                    await ctx.RespondAsync($"Não foi possível encontrar o usuário {user.Username}#{user.Discriminator} para desbanir.");
                }
                catch (UnauthorizedException)
                {
                    await ctx.RespondAsync($"Você não tem permissão para desbanir o usuário {user.Username}#{user.Discriminator}.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    await ctx.RespondAsync($"Ocorreu um erro ao desbanir o usuário {user.Username}#{user.Discriminator}.");
                }

            }
        }
    }
}