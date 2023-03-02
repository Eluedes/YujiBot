using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YujiCanary.Comandos
{
    public class Ban : BaseCommandModule
    {
        [Command("ban")]
        [Description("Comando para banir o usuario")]
        [RequirePermissions(Permissions.BanMembers)]
        public async Task BanCommand(CommandContext ctx, DiscordMember member, int days = 0, int weeks = 0, int months = 0, [RemainingText] string reason = null)
        {
            // Calcula o tempo total em dias
            int totalDays = days + (weeks * 7) + (months * 30);

            // Calcula a data de expiração
            DateTime expirationDate = DateTime.UtcNow.AddDays(totalDays);

            // Bane o membro do servidor
            await ctx.Guild.BanMemberAsync(member, totalDays, reason);

            // Envia uma mensagem confirmando o banimento
            await ctx.RespondAsync($"O usuário {member.Username}#{member.Discriminator} foi banido por {totalDays} dias{(string.IsNullOrEmpty(reason) ? "" : $" com o motivo: {reason}")}. O banimento expira em {expirationDate:d} às {expirationDate:t} (UTC).");
        }
    }
}
