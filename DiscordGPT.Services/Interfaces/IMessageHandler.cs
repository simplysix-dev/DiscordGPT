using Discord.WebSocket;

namespace DiscordGPT.Services.Interfaces
{
    public interface IMessageHandler
    {
        Task HandleMessageAsync(SocketMessage message);
    }
}