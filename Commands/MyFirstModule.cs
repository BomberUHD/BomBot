using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BomBot.Commands
{
	public class MyFirstModule : BaseCommandModule
	{
		[Command("greet")]
		public async Task GreetCommand(CommandContext ctx, DiscordMember member)
		{
			await ctx.RespondAsync($"Greetings, {member.Mention}! Testing CommandsNext");
		}

		[Command("greet")]
		public async Task GreetCommand(CommandContext ctx, DiscordRole role)
		{
			await ctx.TriggerTypingAsync();
			await ctx.RespondAsync($"Greetings, {role.Mention}! Testing CommandsNext");
		}
	}
}
