using Discord;
using Discord.Commands;
using Discord.WebSocket;
using FileContextCore;
using Jana.Data;
using Jana.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Jana_bot.Main
{

    public class Program
    {
        public static void Main(string[] args) => new Program().MainAsync().GetAwaiter().GetResult();

        public DiscordSocketClient Client { get; set; }

        public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
                .Build();

        public async Task MainAsync()
        {
            Client = new DiscordSocketClient();

            var services = ConfigureServices();
            await services.GetRequiredService<CommandHandlingService>().InitializeAsync(services);

            await Client.LoginAsync(TokenType.Bot, Configuration["token"]);
            await Client.StartAsync();

            await Task.Delay(-1);
        }

        private IServiceProvider ConfigureServices()
        {
            return new ServiceCollection()
                .AddSingleton(Client)
                .AddSingleton<CommandService>()
                .AddSingleton<CommandHandlingService>()
                .AddSingleton<FilterService>()
                .AddSingleton(Configuration)
                .AddDbContext<JanaContext>(options =>
                {
                    options.UseFileContextDatabase();
                }, ServiceLifetime.Transient)
                .BuildServiceProvider();
        }
    }
}
