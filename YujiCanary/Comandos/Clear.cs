using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YujiCanary.Comandos
{
    public class Clear : BaseCommandModule
    {
        [Command("clear")]
        [Description("Apagar as mensagens do chat. Sendo que demora 5s para serem apagadas e voce deve colocar quantas voce quer apagar.")]
        [RequirePermissions(Permissions.ManageMessages)]
        public async Task ClearCommand(CommandContext ctx, int count)
        {
            // Verifica se o número de mensagens fornecido é maior que 0 e menor ou igual a 100.
            if (count <= 0 || count > 100)
            {
                await ctx.RespondAsync("Por favor, forneça um número entre 1 e 100.");
                return;
            }

            var messages = await ctx.Channel.GetMessagesAsync(count);

            // Filtra as mensagens com mais de um mês de idade e avisa o usuário.
            var filteredMessages = messages.Where(x => (DateTimeOffset.Now - x.Timestamp).Days <= 30).ToList();
            if (filteredMessages.Count != messages.Count)
            {
                await ctx.RespondAsync($"Algumas mensagens com mais de um mês de idade não serão excluídas.");
            }

            // Aguarda 5 segundos antes de excluir as mensagens.
            await Task.Delay(5000);

            // Exclui as mensagens restantes.
            await ctx.Channel.DeleteMessagesAsync(filteredMessages);

            // Exibe a mensagem de confirmação.
            var response = await ctx.RespondAsync($"Foram excluídas {filteredMessages.Count} mensagens.");
            await Task.Delay(5000);
            await response.DeleteAsync();
        }


    }
}
