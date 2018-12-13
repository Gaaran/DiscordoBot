using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity;
using DSharpPlus.VoiceNext;


namespace DiscordoBot
{
    class DiscordoCommands
    {
        
        IReadOnlyList<DiscordMessage> readMessages = new List<DiscordMessage>();
        IEnumerable<DiscordRole> discordRoles;

        [Command("purge")]
        public async Task Purge(CommandContext commandContext, int getPurgeNumber = 0)
        {
            discordRoles = commandContext.Member.Roles;
            
            if (commandContext.User.Discriminator == "9008")
            {
                //await commandContext.RespondAsync($"purge last : {getPurgeNumber} messages");

                readMessages = await commandContext.Channel.GetMessagesAsync(getPurgeNumber +1 );

                await commandContext.Channel.DeleteMessagesAsync(readMessages);
                await commandContext.RespondAsync($"purged last : {getPurgeNumber} messages, nikez vous");
            }
        }

        [Command("test")]
        public async Task Test(CommandContext commandContext)
        {
            
            discordRoles = commandContext.Member.Roles;
            
            //discordRoles.GetEnumerator();
            //if ( )
            //{

            //}
            await commandContext.RespondAsync($"test : ");
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

        //voice shit
        [Command("join")]
        public async Task JoinVocal(CommandContext commandContext, DiscordChannel channel = null)
        {
            VoiceNextClient voiceNext = commandContext.Client.GetVoiceNextClient();

            if (voiceNext == null)
            {
                await commandContext.RespondAsync("voiceNext == null;");
                return;
            }
            VoiceNextConnection voiceConnection = voiceNext.GetConnection(commandContext.Guild);
            if (voiceConnection != null)
            {
                await commandContext.RespondAsync("already connected");
                return;
            }
            else
            {
                await commandContext.RespondAsync("bug bug bug");
            }

            DiscordVoiceState voiceState = commandContext.Member.VoiceState;
            if (voiceState.Channel == null && channel == null)
            {
                await commandContext.RespondAsync("you're not in a voice channel");
                return;
            }

            if (channel == null)
            {
                channel = voiceState.Channel;
            }

            await commandContext.RespondAsync($"Connected to {channel.Name}");
            voiceConnection = await voiceNext.ConnectAsync(channel);
        }

        [Command("leave"), Description("Leaves a voice channel.")]
        public async Task Leave(CommandContext ctx)
        {
            VoiceNextClient vnext = ctx.Client.GetVoiceNextClient();
            if (vnext == null)
            {
                // not enabled
                await ctx.RespondAsync("VNext is not enabled or configured.");
                return;
            }

            // check whether we are connected
            
            VoiceNextConnection vnc = vnext.GetConnection(ctx.Guild);
            if (vnc == null)
            {
                // not connected
                await ctx.RespondAsync("Not connected in this guild.");
                return;
            }

            // disconnect
            vnc.Disconnect();
            await ctx.RespondAsync("Disconnected");
        }

        [Command("kill")]
        public async Task Kill(CommandContext ctx)
        {
            await ctx.RespondAsync(ctx.Guild.ToString());
            await Program.discord.DisconnectAsync();
            await Task.Delay(-1);
        }
    }
}