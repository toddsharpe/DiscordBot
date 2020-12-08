using System;
using System.Collections.Generic;
using System.Text;

namespace DiscordBot.Host.Models
{
	class CalendarEvent
	{
		public DateTime Start { get; set; }
		public DateTime End { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string Location { get; set; }
	}
}
