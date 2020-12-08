using System.Net.Http;
using System.Threading.Tasks;
using Discord.Commands;
using DiscordBot.Host.Models;
using DiscordBot.Host.Services;

namespace DiscordBot.Host.Modules
{
	public class StockModule : ModuleBase<SocketCommandContext>
	{
		//This should be an interface but idk how to do that with dependency injection
		public IexCloudService StockService { get; set; }

		//Commands
		[Command("stock")]
		public async Task StockQueryAsync([Remainder] string ticker)
		{
			try
			{
				StockQuery query = await StockService.GetStockPrice(ticker);
				await ReplyAsync($"{query.Symbol}: {query.Close} {query.Change} ({query.ChangePercent}%)");
			}
			catch (HttpRequestException e)
			{
				await ReplyAsync(e.Message);
			}
		}
	}
}
