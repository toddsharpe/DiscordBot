using Discord;
using Discord.Commands;
using Discord.WebSocket;
using DiscordBot.Host.Services;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace DiscordBot.Host.Modules
{
	public class SportsModule : ModuleBase<SocketCommandContext>
	{
		//This should be an interface but idk how to do that with dependency injection
		public NhlService NhlService { get; set; }

		private Timer _updateTimer;
		public SportsModule()
		{
			_updateTimer = new Timer
			{
				Interval = (DateTime.Now.AddDays(1).Date.AddHours(1) - DateTime.Now).TotalMilliseconds,
			};
			_updateTimer.Elapsed += _updateTimer_Elapsed;
			_updateTimer.Start();
		}

		private async void _updateTimer_Elapsed(object sender, ElapsedEventArgs e)
		{
			string topic = String.Empty;
			var games = await NhlService.GetTodaysGames();
			int i = 0;
			foreach (var game in games)
			{
				//there's definitely a built in way to do the string format without ternary
				topic += game.teams.ToString() + " " + game.gameDate.ToLocalTime().ToString(game.gameDate.Minute == 0 ? "h tt" : "h:mm tt") + (i % 2 == 0 ? ", " : "\r\n");
				i++;
			}
			topic = topic.Substring(0, topic.Length - 2);

			foreach (var channel in State.NhlChannels.Values)
			{
				try
				{
					await channel.ModifyAsync(p => p.Topic = topic);
				}
				catch (HttpRequestException ex)
				{
					await ReplyAsync(ex.Message);
				}
			}

			_updateTimer.Interval = (DateTime.Now.AddDays(1).Date.AddHours(1) - DateTime.Now).TotalMilliseconds;
		}

		[Command("nhl subscribe")]
		public async Task Subscribe()
		{
			SocketTextChannel channel = Context.Channel as SocketTextChannel;
			if (State.NhlChannels.ContainsKey(channel.Id))
			{
				await ReplyAsync("Channel already added");
			}
			else
			{
				State.NhlChannels.Add(channel.Id, channel);
				await ReplyAsync("Channel will receive topic updates of NHL games");
			}
		}

		[Command("nhl update")]
		public async Task NhlUpdate()
		{
			await Task.Run(() => _updateTimer_Elapsed(null, null));
		}
	}
}
