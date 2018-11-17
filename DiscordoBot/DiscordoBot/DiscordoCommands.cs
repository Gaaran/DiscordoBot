using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity;

namespace DiscordoBot
{
    class DiscordoCommands
    {
        
        IReadOnlyList<DiscordMessage> readMessages = new List<DiscordMessage>();


        [Command("purge")]
        public async Task Purge(CommandContext commandContext, int getPurgeNumber = 0)
        {

            //await commandContext.RespondAsync($"purge last : {getPurgeNumber} messages");

            readMessages = await commandContext.Channel.GetMessagesAsync(getPurgeNumber);

            await commandContext.Channel.DeleteMessagesAsync(readMessages);
            await commandContext.RespondAsync($"purge last : {getPurgeNumber} messages, nikez vous");
        }

        [Command("master")]
        public async Task Master(CommandContext commandContext)
        {
            if (commandContext.User.IsCurrent)
            {
                await commandContext.RespondAsync($"Yes");
            }
            else
                await commandContext.RespondAsync($"No");
        }
    }
}