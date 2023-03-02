using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.CommandsNext.Converters;
using DSharpPlus.CommandsNext.Entities;
using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YujiCanary.Comandos
{
    public class ComandosBasicos : BaseCommandModule
    {
        [Command("ajuda")]
        [Description("Comando para ajudar nossos usuarios")]
        public async Task AjudaCommand(CommandContext ctx)
        {
            var embed = new DiscordEmbedBuilder
            {
                Title = "Comandos",
                Description = "Aqui estão todos os comandos disponíveis:",
                Color = new DiscordColor("#00ff00")
            };

            foreach (var command in ctx.CommandsNext.RegisteredCommands.Values)
            {
                embed.AddField(command.Name, command.Description);
            }

            await ctx.RespondAsync(embed: embed.Build());
        }

    }
}
