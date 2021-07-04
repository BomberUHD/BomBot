using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BomBot.Events
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
            if (e.Message.Content.ToLower().StartsWith("!r"))
            {
                if (e.MentionedRoles.Count != 2)
                {
                    await e.Message.RespondAsync("Invalid amount of Roles mentioned.");
                }
                else
                {
                    await RemoveRoles(s, e);
                }
            }
        }
        public async Task ReactionAddedHandler(DiscordClient s, MessageReactionAddEventArgs e)
        {
            String temp = String.Format("Detected {0} Reaction from {1} in <#{2}>\n {3}", e.Emoji.Name, e.User.Mention, e.Channel.Id, e.Emoji.Url);
            await e.Message.Channel.SendMessageAsync(temp);
        }

        public async Task RemoveRoles(DiscordClient s, MessageCreateEventArgs e)
        {
            List<DiscordRole> x = e.MentionedRoles.ToList<DiscordRole>();
            DiscordRole hasthisRole = x.First();
            DiscordRole getsremovedRole = x.Last();
            int i = 0;
            string memberlist = "";

            var members = GetMembers(s, e);
            foreach (DiscordMember member in members.Result)
            {

                if (member.IsBot)
                {
                    var allRoles = member.Roles.ToList<DiscordRole>();
                    if (allRoles.Contains(e.MentionedRoles.First<DiscordRole>()) && allRoles.Contains(e.MentionedRoles.Last<DiscordRole>()))
                    {
                        string temp = String.Format("User {0} contains {1} & {2}", member.Username, e.MentionedRoles.First<DiscordRole>().Mention, e.MentionedRoles.Last<DiscordRole>().Mention);
                        await e.Message.RespondAsync(temp);
                    }
                }
                else
                {
                    memberlist += "\n" + member.Username;
                }

            }
            //await e.Message.RespondAsync(memberlist + "\nTotal Users: " + members.Result.Count());

        }

        public async Task<IReadOnlyCollection<DiscordMember>> GetMembers(DiscordClient s, MessageCreateEventArgs e)
        {
            var members = e.Guild.GetAllMembersAsync().Result;
            return members;
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