using System;
using System.Collections.Generic;
using System.Text;

namespace DiscordBot.Host.Models
{
	public class StockQuery
	{
		public string Symbol { get; set; }
		public string CompanyName { get; set; }
		public StockQueryType Type { get; set; }
		public double Open { get; set; }
		public double Close { get; set; }
		public double High { get; set; }
		public double Low { get; set; }
		public double Change { get; set; }
		public double ChangePercent { get; set; }
	}
}
