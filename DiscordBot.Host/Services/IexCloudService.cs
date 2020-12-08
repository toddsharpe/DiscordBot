using DiscordBot.Host.Framework;
using DiscordBot.Host.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.Host.Services
{
	//https://iexcloud.io/docs/api/
	public class IexCloudService : HttpBaseService, IStockService
	{
		private readonly AppSettings _appSettings;

		public IexCloudService(IOptions<AppSettings> appSettings, HttpClient client) : base(client)
		{
			_appSettings = appSettings?.Value ?? throw new ArgumentNullException(nameof(appSettings));
		}

		public async Task<StockQuery> GetStockPrice(string ticker)
		{
			string url = $"https://cloud.iexapis.com/stable/stock/{ticker}/batch?types=quote&token={_appSettings.IexApiSecret}";
			RootObject response = await this.GetApiResultAsync<RootObject>(url);
			return new StockQuery
			{
				Symbol = response.quote.symbol,
				CompanyName = response.quote.companyName,
				Type = Enum.Parse<StockQueryType>(response.quote.calculationPrice, true),
				High = response.quote.high,
				Low = response.quote.low,
				Close = response.quote.close,
				Open = response.quote.open,
				Change = response.quote.change,
				ChangePercent = response.quote.changePercent * 100
			};
		}

		public class Quote
		{
			public string symbol { get; set; }
			public string companyName { get; set; }
			public string primaryExchange { get; set; }
			public string calculationPrice { get; set; }
			public double open { get; set; }
			public long openTime { get; set; }
			public string openSource { get; set; }
			public double close { get; set; }
			public long closeTime { get; set; }
			public string closeSource { get; set; }
			public double high { get; set; }
			public long highTime { get; set; }
			public string highSource { get; set; }
			public double low { get; set; }
			public long lowTime { get; set; }
			public string lowSource { get; set; }
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
			public double oddLotDelayedPrice { get; set; }
			public long oddLotDelayedPriceTime { get; set; }
			public double extendedPrice { get; set; }
			public double extendedChange { get; set; }
			public double extendedChangePercent { get; set; }
			public long extendedPriceTime { get; set; }
			public double previousClose { get; set; }
			public int previousVolume { get; set; }
			public double change { get; set; }
			public double changePercent { get; set; }
			public int volume { get; set; }
			public object iexMarketPercent { get; set; }
			public object iexVolume { get; set; }
			public int avgTotalVolume { get; set; }
			public object iexBidPrice { get; set; }
			public object iexBidSize { get; set; }
			public object iexAskPrice { get; set; }
			public object iexAskSize { get; set; }
			public object iexOpen { get; set; }
			public object iexOpenTime { get; set; }
			public double iexClose { get; set; }
			public long iexCloseTime { get; set; }
			public long marketCap { get; set; }
			public double? peRatio { get; set; }
			public double week52High { get; set; }
			public double week52Low { get; set; }
			public double ytdChange { get; set; }
			public long lastTradeTime { get; set; }
			public bool isUSMarketOpen { get; set; }
		}

		public class RootObject
		{
			public Quote quote { get; set; }
		}
	}
}
