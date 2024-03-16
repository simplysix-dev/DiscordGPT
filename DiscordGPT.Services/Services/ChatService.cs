using DiscordGPT.Services.Interfaces;
using DiscordGPT.Services.Providers;
using OpenAI_API.Chat;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordGPT.Services.Services
{
    public class ChatService : IChatService
    {
        private readonly IOpenAIProvider _openAIProvider;
        private readonly ConcurrentDictionary<string, Conversation> _userConversations = new ConcurrentDictionary<string, Conversation>();

        public ChatService(IOpenAIProvider openAIProvider)
        {
            _openAIProvider = openAIProvider;
            _userConversations = new ConcurrentDictionary<string, Conversation>();
        }

        public async Task<string> ProcessMessageAsync(string userId, string message)
        {
            var conversation = _userConversations.GetOrAdd(userId, _ => _openAIProvider.CreateNewConversation());

            conversation.AppendUserInput(message);

            string response = await conversation.GetResponseFromChatbotAsync();

            return response;
        }
    }
}
