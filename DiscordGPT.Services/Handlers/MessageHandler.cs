using Discord.WebSocket;
using Discord;
using DiscordGPT.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiscordGPT.Services.Interfaces;

namespace DiscordGPT.Services.Handlers
{
    public class MessageHandler : IMessageHandler
    {
        private readonly IChatService _chatService;

        public MessageHandler(IChatService chatService)
        {
            _chatService = chatService;
        }

        public async Task HandleMessageAsync(SocketMessage message)
        {
            if (message is not SocketUserMessage userMessage) return;

            if (message.Author.IsBot) return;

            if (message.Channel is IDMChannel)
            {
                var response = await _chatService.ProcessMessageAsync(message.Author.Id.ToString(), message.Content);
                await message.Channel.SendMessageAsync(response);
            }
        }
    }
}
