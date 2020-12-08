using System;
using System.Collections.Generic;
using System.Text;
using Discord;

namespace DiscordBot.Host
{
	public static class Emojis
	{
		public static Emoji ThumbsUp = new Emoji("👍");
		public static Emoji ThumbsDown = new Emoji("👎");
		public static Emoji NotSure = new Emoji("🤷");

		//TODO: Figure out how to increment two code unicode literals
		//This pains me
		public static Emoji[] Numbers = new []
		{
			new Emoji("0⃣"),
			new Emoji("1⃣"),
			new Emoji("2⃣"),
			new Emoji("3⃣"),
			new Emoji("4⃣"),
			new Emoji("5⃣"),
			new Emoji("6⃣"),
			new Emoji("7⃣"),
			new Emoji("8⃣"),
			new Emoji("9⃣"),
		};
	}
}
