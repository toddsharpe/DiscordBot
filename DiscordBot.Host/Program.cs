using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using DiscordBot.Host.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DiscordBot.Host
{
	class Program
	{
		public static void Main(string[] args) => new Program().MainAsync().GetAwaiter().GetResult();

		public async Task MainAsync()
		{
			// create service collection
			var services = new ServiceCollection();
			ConfigureServices(services);

			// create service provider
			var serviceProvider = services.BuildServiceProvider();

			//Load token
			string token = File.ReadAllText("Token.txt");

			// You should dispose a service provider created using ASP.NET
			// when you are finished using it, at the end of your app's lifetime.
			// If you use another dependency injection framework, you should inspect
			// its documentation for the best way to do this.
			using (serviceProvider)
			{
				var client = serviceProvider.GetRequiredService<DiscordSocketClient>();

				client.Log += LogAsync;
				serviceProvider.GetRequiredService<CommandService>().Log += LogAsync;

				// Tokens should be considered secret data and never hard-coded.
				// We can read from the environment variable to avoid hardcoding.
				await client.LoginAsync(TokenType.Bot, token);
				await client.StartAsync();

				// Here we initialize the logic required to register our commands.
				await serviceProvider.GetRequiredService<CommandHandlingService>().InitializeAsync();

				await Task.Delay(-1);
			}
		}

		private Task LogAsync(LogMessage msg)
		{
			Console.WriteLine(msg.ToString());
			return Task.CompletedTask;
		}

		private static void ConfigureServices(IServiceCollection services)
		{
			// configure logging
			services.AddLogging(builder =>
			{
				builder.AddConsole();
				builder.AddDebug();
			});

			// build config
			var configuration = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json", optional: false)
				.AddEnvironmentVariables()
				.Build();

			services.Configure<AppSettings>(configuration.GetSection("App"));

			// add services:
			services.AddSingleton<DiscordSocketClient>();
			services.AddSingleton<CommandService>();
			services.AddSingleton<CommandHandlingService>();
			services.AddSingleton<HttpClient>();
			services.AddSingleton<NhlService>();
			services.AddSingleton<IexCloudService>();
		}
	}
}
