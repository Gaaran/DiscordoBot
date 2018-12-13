using System;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using DSharpPlus.VoiceNext;
using DSharpPlus.VoiceNext.Codec;

namespace DiscordoBot
{
    class Program
    {

        public static DiscordClient discord;
        static CommandsNextModule commands;
        //static DiscordMessage message;
        public static VoiceNextClient voiceNextClient;

        static void Main(string[] args)
        {
            MainAsync(args).ConfigureAwait(false).GetAwaiter().GetResult();
        }


        static async Task MainAsync(string[] args)
        {

            discord = new DiscordClient(new DiscordConfiguration
            {
                Token = DiscordoBotToken.myBotToken,
                TokenType = TokenType.Bot
            });

            commands = discord.UseCommandsNext(new CommandsNextConfiguration
            {
                StringPrefix = "!"
            });
            
            commands.RegisterCommands<DiscordoCommands>();

            VoiceNextConfiguration vNextConfiguration = new VoiceNextConfiguration
            {
                VoiceApplication = VoiceApplication.Music
            };

            voiceNextClient = discord.UseVoiceNext(vNextConfiguration);
            
            await discord.ConnectAsync();
            await Task.Delay(-1);
        }
    }
}
