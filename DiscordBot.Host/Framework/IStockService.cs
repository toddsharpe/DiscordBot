using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DiscordBot.Host.Models;

namespace DiscordBot.Host.Framework
{
	public interface IStockService
	{
		Task<StockQuery> GetStockPrice(string ticker);
	}
}
