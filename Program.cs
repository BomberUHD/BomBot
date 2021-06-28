using BombBot.Events;
using DSharpPlus;
using DSharpPlus.EventArgs;
using Emzi0767.Utilities;
using System;
using System.Threading.Tasks;

namespace BombBot
{
    class Program
    {

        static async Task Main(string[] args)
        {
            await new BombBot().MainAsync();
        }
    }
}
