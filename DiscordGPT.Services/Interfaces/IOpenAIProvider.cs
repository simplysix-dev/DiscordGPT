using OpenAI_API.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordGPT.Services.Interfaces
{
    public interface IOpenAIProvider
    {
        Conversation CreateNewConversation();
    }
}
