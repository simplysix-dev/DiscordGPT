using DiscordGPT.Services.Interfaces;
using OpenAI_API;
using OpenAI_API.Chat;
using OpenAI_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordGPT.Services.Providers
{
    public class OpenAIProvider : IOpenAIProvider
    {
        private readonly string _apiKey;
        private readonly OpenAIAPI _openAI;

        public OpenAIProvider(string apiKey)
        {
            _apiKey = apiKey;
            _openAI = new OpenAIAPI(apiKey);
        }

        public Conversation CreateNewConversation()
        {
            var conversation = _openAI.Chat.CreateConversation();

            conversation.Model = Model.GPT4_Vision;
            conversation.RequestParameters.Temperature = 0.7;
            conversation.RequestParameters.MaxTokens = 4096;

            conversation.AppendSystemMessage("You are a helpful Discord Bot called DiscordGPT. A user starts a conversation with you via direct messages, and you will assist them with their queries.");

            return conversation;
        }
    }
}
