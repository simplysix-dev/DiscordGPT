using Discord.WebSocket;
using Discord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiscordGPT.Services.Interfaces;

namespace DiscordGPT.App.Bot
{
    public interface IBotClient
    {
        Task LoginAsync(string token);
        Task StartAsync();
        Task StopAsync();
    }

    public class BotClient : IBotClient
    {
        private readonly DiscordSocketClient _client;

        public BotClient(IMessageHandler messageHandler)
        {
            _client = new DiscordSocketClient();
            _client.MessageReceived += messageHandler.HandleMessageAsync;
        }

        public async Task LoginAsync(string token)
        {
            await _client.LoginAsync(TokenType.Bot, token);
        }

        public async Task StartAsync()
        {
            await _client.StartAsync();
        }

        public Task StopAsync()
        {
            return _client.StopAsync();
        }
    }
}
