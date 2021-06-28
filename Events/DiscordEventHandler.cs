using DSharpPlus;
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
        }
        public async Task setupAsync()
        {
            discord.MessageCreated += MessageCreatedHandler;
        }

    }
}