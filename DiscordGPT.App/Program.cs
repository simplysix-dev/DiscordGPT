using Discord.WebSocket;
using Discord;
using Microsoft.Extensions.DependencyInjection;
using DiscordGPT.Services.Services;
using DiscordGPT.Services.Providers;
using DiscordGPT.App.Bot;
using DiscordGPT.Services.Handlers;
using Microsoft.Extensions.Configuration;
using DiscordGPT.Services.Interfaces;

namespace DiscordGPT.App
{
    class Program
    {
        private IServiceProvider? _services;
        private static IConfigurationRoot Configuration;

        static void Main(string[] args)
            => new Program().MainAsync().GetAwaiter().GetResult();

        public async Task MainAsync()
        {
            _services = ConfigureServices();

            Configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

            var botClient = _services.GetRequiredService<IBotClient>();

            await botClient.LoginAsync(Configuration["DiscordBot:Token"]);
            await botClient.StartAsync();

            await Task.Delay(-1);
        }

        private IServiceProvider ConfigureServices()
        {
            return new ServiceCollection()
                .AddSingleton<IBotClient, BotClient>()
                .AddSingleton<IMessageHandler, MessageHandler>()
                .AddSingleton<IOpenAIProvider, OpenAIProvider>(provider => new OpenAIProvider(Configuration["OpenAI:ApiKey"]))
                .AddSingleton<IChatService, ChatService>()
                .BuildServiceProvider();
        }
    }
}
