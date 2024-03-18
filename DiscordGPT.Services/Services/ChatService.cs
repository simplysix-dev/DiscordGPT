using Discord.Rest;
using Discord.WebSocket;
using DiscordGPT.Services.Interfaces;
using OpenAI_API.Chat;
using System;
using System.Collections.Concurrent;
using System.Text;
using static OpenAI_API.Chat.ChatMessage;

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

        public async Task ProcessMessageAsync(string userId, string message, ISocketMessageChannel channel, params string[] imageUrls)
        {
            var conversation = _userConversations.GetOrAdd(userId, _ => _openAIProvider.CreateNewConversation());
            conversation.AppendUserInput(message, imageUrls.Select(x => ImageInput.FromImageUrl(x)).ToArray());

            string accumulatedResponse = "";
            RestUserMessage discordMessage = null;
            var updateTask = Task.CompletedTask;

            await foreach (var res in conversation.StreamResponseEnumerableFromChatbotAsync())
            {
                accumulatedResponse += res;

                if (updateTask.IsCompleted)
                {
                    updateTask = Task.Run(async () =>
                    {
                        await Task.Delay(2000);

                        if (discordMessage == null)
                        {
                            discordMessage = await channel.SendMessageAsync(accumulatedResponse);
                        }
                        else
                        {
                            if (accumulatedResponse.Length >= 2000)
                            {
                                var cutOffPoint = FindLastWordBoundary(accumulatedResponse, 2000);
                                var subMessage = accumulatedResponse.Substring(0, cutOffPoint);
                                await discordMessage.ModifyAsync(msg => msg.Content = subMessage);
                                accumulatedResponse = accumulatedResponse.Substring(cutOffPoint);
                                discordMessage = await channel.SendMessageAsync(accumulatedResponse);
                            }
                            else
                            {
                                await discordMessage.ModifyAsync(msg => msg.Content = accumulatedResponse);
                            }
                        }
                    });
                }
            }

            await updateTask;
        }

        private int FindLastWordBoundary(string message, int maxLength)
        {
            int lastSpace = message.LastIndexOf('.', maxLength);
            return lastSpace > 0 ? lastSpace + 1 : maxLength;
        }
    }
}