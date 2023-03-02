using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using Newtonsoft.Json;
using DSharpPlus.Interactivity.Extensions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System;

namespace YujiCanary.Comandos
{
   public class Cargos : BaseCommandModule
    {
        [Command("autorole")]
        [Description("Ativa ou desativa a função de autorole")]
        public async Task Autorole(CommandContext ctx, [Description("enable ou disable")] string option, [Description("Cargo que será entregue para novos membros")] DiscordRole role = null)
        {
            // Verifica se o usuário tem permissão para gerenciar cargos
            if (!ctx.Member.Permissions.HasPermission(Permissions.ManageRoles))
            {
                await ctx.RespondAsync("Você não tem permissão para gerenciar cargos neste servidor.");
                return;
            }

            // Verifica se o usuário informou a opção corretamente
            if (option.ToLower() != "enable" && option.ToLower() != "disable")
            {
                await ctx.RespondAsync("Opção inválida. Use `enable` ou `disable`.");
                return;
            }

            // Ativa a função de autorole
            if (option.ToLower() == "enable")
            {
                // Verifica se o usuário informou o cargo corretamente
                if (role == null)
                {
                    await ctx.RespondAsync("Por favor, mencione o cargo ou informe o ID do cargo que deseja que seja entregue para novos membros.");
                    return;
                }

                // Salva o ID do cargo no banco de dados para uso futuro
                // (substitua "seuBancoDeDados" pelo nome do seu banco de dados)
                using (var db = new seuBancoDeDados())
                {
                    var guild = db.Guilds.FirstOrDefault(g => g.Id == ctx.Guild.Id);
                    if (guild == null)
                    {
                        guild = new Guild { Id = ctx.Guild.Id, AutoroleRoleId = role.Id };
                        db.Guilds.Add(guild);
                    }
                    else
                    {
                        guild.AutoroleRoleId = role.Id;
                    }
                    db.SaveChanges();
                }

                await ctx.RespondAsync($"Função de autorole ativada. O cargo {role.Mention} será entregue para novos membros.");

                // Registra um evento para entregar o cargo para novos membros
                // (substitua "discord" pelo seu objeto DiscordClient)
                discord.GuildMemberAdded += async (s, e) =>
                {
                    using (var db = new seuBancoDeDados())
                    {
                        var guild = db.Guilds.FirstOrDefault(g => g.Id == e.Guild.Id);
                        if (guild != null && guild.AutoroleRoleId != null)
                        {
                            var member = await e.Guild.GetMemberAsync(e.Member.Id);
                            var autoroleRole = e.Guild.GetRole(guild.AutoroleRoleId.Value);
                            if (autoroleRole != null)
                            {
                                await member.GrantRoleAsync(autoroleRole);
                            }
                        }
                    }
                };
            }
            // Desativa a função de autorole
            else
            {
                // Remove o registro do ID do cargo do banco de dados
                // (substitua "seuBancoDeDados" pelo nome do seu banco de dados)
                using (var db = new seuBancoDeDados())
                {
                    var guild = db.Guilds.FirstOrDefault(g => g.Id == ctx.Guild.Id);
                    if (guild != null)
                    {
                        guild.AutoroleRoleId =
        
    }
                }
