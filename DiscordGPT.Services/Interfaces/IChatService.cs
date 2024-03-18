using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordGPT.Services.Interfaces
{
    public interface IChatService
    {
        Task ProcessMessageAsync(string userId, string message, ISocketMessageChannel channel, params string[] imageUrls);
    }
}
