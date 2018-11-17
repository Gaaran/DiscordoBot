using System;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;

namespace DiscordoBot
{
    class Program
    {

        public static DiscordClient discord;
        static CommandsNextModule commands;
        //static DiscordMessage message;

        static void Main(string[] args)
        {
            MainAsync(args).ConfigureAwait(false).GetAwaiter().GetResult();
        }


        static async Task MainAsync(string[] args)
        {

            discord = new DiscordClient(new DiscordConfiguration
            {
                UseInternalLogHandler = true,
                LogLevel = LogLevel.Debug,
                Token = DiscordoBotToken.myBotToken,
                TokenType = TokenType.Bot
            });

            commands = discord.UseCommandsNext(new CommandsNextConfiguration
            {
                StringPrefix = "!"
            });
            
            commands.RegisterCommands<DiscordoCommands>();
            
            await discord.ConnectAsync();
            await Task.Delay(-1);
        }
    }
}
