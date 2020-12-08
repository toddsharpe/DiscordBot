using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace DiscordBot.Host.Modules
{
	//https://discord.foxbot.me/latest/guides/commands/intro.html#modules
	public class DefaultModule : ModuleBase<SocketCommandContext>
	{
		//Commands
		[Command("ping")]
		public Task PingAsync() => ReplyAsync("pong!");

		//Binary
		//Questions will not be
		[Command("poll")]
		[Alias("p")]
		public async Task PollAsync([Remainder] string poll)
		{
			await Context.Message.AddReactionAsync(Emojis.ThumbsUp);
			await Context.Message.AddReactionAsync(Emojis.ThumbsDown);
			await Context.Message.AddReactionAsync(Emojis.NotSure);
		}

		//Would be nice if the parses were smarter but for now we can parse the options from the question
		//Expected input:
		//!question Where to eat?
		//Bills
		//Novilhos
		//Outback
		[Command("question")]
		[Alias("q")]
		public async Task QuestionAsync([Remainder] string questionOptions)
		{
			string[] parts = questionOptions.Split('\n');

			//Needs question and at least an option
			if ((parts.Length < 2) || (parts.Length > 10))
				return;

			//Quick loop to verify "#. Options" format, but lets relax that
			//Disabling, doesn't seem necessary
			/*
			for (int i = 1; i < parts.Length; i++)
			{
				string[] parse = parts[i].Split('.', 2);

				if ((parse.Length != 2) || (!int.TryParse(parse[0], out int number)) || (number != i))
					return;
			}
			*/

			//Now just mark the incoming message with options
			for (int i = 1; i < parts.Length; i++)
			{
				await Context.Message.AddReactionAsync(Emojis.Numbers[i]);
			}
		}
	}

	//builder.WithAuthor(author);
	//builder.WithDescription("Some massive description");
	//builder.WithImageUrl("https://www.google.com/images/branding/googlelogo/1x/googlelogo_color_272x92dp.png");
	//builder.AddField("Cost", "3", true);    // true - for inline
	//builder.AddField("HP", "665", true);
	//builder.AddField("DPS", "42", true);
	//builder.AddField("Hit Speed", "1.5sec", true);
	//builder.AddField("SlowDown", "35%", true);
	//builder.AddField("AOE", "63", true);
	//builder.WithThumbnailUrl("https://www.google.com/images/branding/googlelogo/1x/googlelogo_color_272x92dp.png");
}
