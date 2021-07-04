using BomBot.Events;
using DSharpPlus;
using DSharpPlus.EventArgs;
using Emzi0767.Utilities;
using System;
using System.Threading.Tasks;

namespace BomBot
{
    class Program
    {

        static async Task Main(string[] args)
        {
            await new BomBot().MainAsync();
        }
    }
}
