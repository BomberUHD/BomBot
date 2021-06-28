using BombBot.Events;
using DSharpPlus;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BombBot
{
    class BombBot
    {
        public DiscordClient discord;
        private IServiceProvider service;
        public BombBot()
        {
            discord = new DiscordClient(new DiscordConfiguration()
            {
                Token = "yourmom",
                TokenType = TokenType.Bot,
                Intents = DiscordIntents.AllUnprivileged
            });
            service = createServiceProvider();
        }
        public async Task MainAsync()
        {
            var dcEventHandler = service.GetRequiredService<DiscordEventHandler>();
            await dcEventHandler.setupAsync();
            await discord.ConnectAsync();
            await Task.Delay(-1);
        }
        public ServiceProvider createServiceProvider()
        {
            ServiceProvider services = new ServiceCollection()
                .AddSingleton(this)
                .AddSingleton(discord)
                .AddSingleton<DiscordEventHandler>()
                .BuildServiceProvider();
            return services;
        }
    }
}
