using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BombBot.Events
{
    public class DiscordEventHandler
    {
        public DiscordClient discord { get; set; }
        public DiscordEventHandler(IServiceProvider service)
        {
            discord = service.GetRequiredService<DiscordClient>();
        }
        public async Task MessageCreatedHandler(DiscordClient s, MessageCreateEventArgs e)
        {
            if (e.Message.Content.ToLower().StartsWith("ping"))
                await e.Message.RespondAsync("pong!");
            if (e.Message.Content.ToLower().StartsWith("!removerole"))
            {
                if (e.MentionedRoles.Count != 2)
                {
                    await e.Message.RespondAsync("Invalid amount of Roles mentioned.");
                }
                else
                {
                    List<DiscordRole> x = e.MentionedRoles.ToList<DiscordRole>();
                    DiscordRole hasthisRole = x.First();
                    DiscordRole getsremovedRole = x.Last();
                    string temp = String.Format("Detected two Roles. {0} & {1}", hasthisRole.Mention, getsremovedRole.Mention);

                    await GetMember(s, e);
                    
                }
            }
        }
        public async Task ReactionAddedHandler(DiscordClient s, MessageReactionAddEventArgs e)
        {
            String temp = String.Format("Detected <:test:{0}> Reaction in <#{1}>\n {2}", e.Emoji.Id, e.Channel.Id, e.Emoji.Url);
            await e.Message.Channel.SendMessageAsync(temp);
        }

        public async Task GetMember(DiscordClient s, MessageCreateEventArgs e)
        {
            await e.Guild.GetAllMembersAsync();
        }

        #region addEventHandler
        public async Task setupAsync()
        {
            discord.MessageCreated += MessageCreatedHandler;
            discord.MessageReactionAdded += ReactionAddedHandler;
        }
        #endregion

    }
}