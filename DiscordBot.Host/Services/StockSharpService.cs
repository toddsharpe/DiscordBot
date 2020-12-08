using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using DiscordBot.Host.Framework;
using DiscordBot.Host.Models;

namespace DiscordBot.Host.Services
{
	public class StockSharpService : HttpBaseService, IStockService
	{
		public StockSharpService(HttpClient client) : base(client)
		{

		}

		public async Task<StockQuery> GetStockPrice(string ticker)
		{
			string url = $"https://api.iextrading.com/1.0/stock/{ticker}/quote";
			Debug.WriteLine(url);
			QueryResponse response = await this.GetApiResultAsync<QueryResponse>(url);

			return new StockQuery
			{
				Symbol = response.symbol,
				CompanyName = response.companyName,
				Type = Enum.Parse<StockQueryType>(response.calculationPrice, true),
				High = response.high,
				Low = response.low,
				Close = response.close,
				Open = response.open
			};
		}

		private class QueryResponse
		{
			public string symbol { get; set; }
			public string companyName { get; set; }
			public string primaryExchange { get; set; }
			public string sector { get; set; }
			public string calculationPrice { get; set; }
			public double open { get; set; }
			public long openTime { get; set; }
			public double close { get; set; }
			public long closeTime { get; set; }
			public double high { get; set; }
			public double low { get; set; }
			public double latestPrice { get; set; }
			public string latestSource { get; set; }
			public string latestTime { get; set; }
			public long latestUpdate { get; set; }
			public int latestVolume { get; set; }
			public object iexRealtimePrice { get; set; }
			public object iexRealtimeSize { get; set; }
			public object iexLastUpdated { get; set; }
			public double delayedPrice { get; set; }
			public long delayedPriceTime { get; set; }
			public double extendedPrice { get; set; }
			public double extendedChange { get; set; }
			public double extendedChangePercent { get; set; }
			public long extendedPriceTime { get; set; }
			public double previousClose { get; set; }
			public double change { get; set; }
			public double changePercent { get; set; }
			public object iexMarketPercent { get; set; }
			public object iexVolume { get; set; }
			public int avgTotalVolume { get; set; }
			public object iexBidPrice { get; set; }
			public object iexBidSize { get; set; }
			public object iexAskPrice { get; set; }
			public object iexAskSize { get; set; }
			public long marketCap { get; set; }
			public double peRatio { get; set; }
			public double week52High { get; set; }
			public double week52Low { get; set; }
			public double ytdChange { get; set; }
		}
	}
}
