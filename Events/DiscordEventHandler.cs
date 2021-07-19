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
            if (e.Message.Content.ToLower().StartsWith("§remove"))
            {
                if (e.MentionedRoles.Count != 2)
                {
                    await e.Message.RespondAsync("Invalid Syntax: §remove `hasthisRole` `getsremovedRole`");
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
            //string memberlist = "";
            //var members = GetMembers(s, e);

            //var hasthisRole = e.MentionedRoles.First<DiscordRole>();
            //var getsremovedRole = e.MentionedRoles.Last<DiscordRole>();

            //foreach (DiscordMember member in members.Result)
            //{
            //    var allRoles = member.Roles.ToList<DiscordRole>();

            //    if (allRoles.Contains(hasthisRole) && allRoles.Contains(getsremovedRole))
            //    {
            //        string commandissued = string.Format("Remove Role Command issued by {0}", e.Author.Username);
            //        await member.RevokeRoleAsync(e.MentionedRoles.Last<DiscordRole>(), commandissued);

            //        if (!member.IsBot)
            //            memberlist += "\n" + member.Username;
            //    }
            //}
            //string response = string.Format("Removed {0} from these Users:{1}", getsremovedRole.Mention, memberlist);
            //await e.Message.RespondAsync(response);
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