using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiscordBot.Host
{
	//Modules seem to be reconstructed for every call, store global program state here
	public static class State
	{
		public static Dictionary<ulong, SocketTextChannel> NhlChannels = new Dictionary<ulong, SocketTextChannel>();
	}
}
