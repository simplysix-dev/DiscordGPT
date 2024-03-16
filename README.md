# ChatGPT Discord Bot

The ChatGPT Discord Bot integrates OpenAI's powerful ChatGPT with Discord, allowing users to engage in conversations with ChatGPT directly through Discord direct messages. The bot acts as a bridge between Discord users and ChatGPT, providing an interactive and responsive chat experience.

## Features

- **Direct Messaging**: Users can start private conversations with ChatGPT by sending direct messages to the bot.
- **Conversation History**: The bot remembers past conversations within a chat with a user, and is able to maintain a conversation.

## Features in development

- **Image and File Support**: Allow the users to send images and files as part of the conversation.
- **Persistent Conversation History**: Implement a persistent store for conversation history, as user's conversations are currently stored in memory.

## Getting Started

To get the bot up and running on your local machine for development and testing purposes, follow these steps.

### Prerequisites

- .NET 8.0 SDK or later
- A Discord bot, with the corresponding token
- An OpenAI API key

## Setup

1. **Clone the Repository**
   Clone this repository to get started.

2. **appsettings.json**
   Create an `appsettings.json` in the root with the following structure:

   ```json
   {
     "DiscordBot": {
       "Token": "YOUR_DISCORD_BOT_TOKEN"
     },
     "OpenAI": {
       "ApiKey": "YOUR_OPENAI_API_KEY"
     }
   }
   ```

   Replace YOUR_DISCORD_BOT_TOKEN with your Discord bot's token and YOUR_OPENAI_API_KEY with your OpenAI API key.


3. **Start the application**
  Build and run the application. The bot will now be running and ready to interact with users.


## Usage
Users can start a conversation by sending direct messages to the bot on Discord. The bot uses ChatGPT to respond to user messages, facilitating an engaging chat experience.

## Contributing
Feel free to fork the repository, make improvements, and submit pull requests.

## License
This project is licensed under the MIT License.