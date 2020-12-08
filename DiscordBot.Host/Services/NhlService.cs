using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Header = System.Collections.Generic.KeyValuePair<string, string>;

namespace DiscordBot.Host.Services
{
	//This class was bolted together and is shitty. Hence why we dont use the passed in client
	public class NhlService : HttpBaseService
	{
		private const string ApiUrl = "https://statsapi.web.nhl.com/api/v1/";

		public NhlService(HttpClient client) : base(client)
		{

		}

		public async Task<List<Game>> GetTodaysGames()
		{
			var response = await GetApiResultAsync<ScheduleResponse>(ApiUrl + "schedule",
				new Header("startDate", DateTime.Today.ToString("yyyy-MM-dd")),
				new Header("endDate", DateTime.Today.ToString("yyyy-MM-dd")),
				new Header("hydrate", "team,linescore,broadcasts(all),tickets,game(content(media(epg)),seriesSummary),metadata,seriesSummary(series)"));
			return response.dates.Single().games;
		}

		#region Json2Csharp
		public class About
		{
			public int eventIdx { get; set; }
			public int eventId { get; set; }
			public int period { get; set; }
			public string periodType { get; set; }
			public string ordinalNum { get; set; }
			public string periodTime { get; set; }
			public string periodTimeRemaining { get; set; }
			public DateTime dateTime { get; set; }
			public Goals goals { get; set; }
		}
		public class BoxScore
		{
			public TeamState home { get; set; }
			public TeamState away { get; set; }
		}
		public class Conference
		{
			public int id { get; set; }
			public string name { get; set; }
			public string link { get; set; }
		}
		public class Content
		{
			public string link { get; set; }
		}
		public class Coordinates
		{
			public double x { get; set; }
			public double y { get; set; }
		}
		public class Date
		{
			public string date { get; set; }
			public int totalItems { get; set; }
			public int totalEvents { get; set; }
			public int totalGames { get; set; }
			public int totalMatches { get; set; }
			public List<Game> games { get; set; }
			public List<object> events { get; set; }
			public List<object> matches { get; set; }
		}
		public class Division
		{
			public int id { get; set; }
			public string name { get; set; }
			public string link { get; set; }
		}
		public class Franchise
		{
			public int franchiseId { get; set; }
			public string teamName { get; set; }
			public string link { get; set; }
		}
		public class Game
		{
			public int gamePk { get; set; }
			public string link { get; set; }
			public string gameType { get; set; }
			public string season { get; set; }
			public DateTime gameDate { get; set; }
			public Status status { get; set; }
			public Teams teams { get; set; }
			public Linescore linescore { get; set; }
			public List<ScoringPlay> scoringPlays { get; set; }
			public Venue venue { get; set; }
			public Content content { get; set; }
			public SeriesSummary seriesSummary { get; set; }
		}
		public class Goals
		{
			public int away { get; set; }
			public int home { get; set; }
		}
		public class IntermissionInfo
		{
			public int intermissionTimeRemaining { get; set; }
			public int intermissionTimeElapsed { get; set; }
			public bool inIntermission { get; set; }
		}
		public class LeagueRecord
		{
			public int wins { get; set; }
			public int losses { get; set; }
			public int ot { get; set; }
			public string type { get; set; }
		}
		public class Linescore
		{
			public int currentPeriod { get; set; }
			public string currentPeriodOrdinal { get; set; }
			public string currentPeriodTimeRemaining { get; set; }
			public List<Period> periods { get; set; }
			public ShootoutInfo shootoutInfo { get; set; }
			public BoxScore teams { get; set; }
			public string powerPlayStrength { get; set; }
			public bool hasShootout { get; set; }
			public IntermissionInfo intermissionInfo { get; set; }
			public PowerPlayInfo powerPlayInfo { get; set; }
		}
		class MediaWallItem
		{
			public string id { get; set; }
			public string type { get; set; }
			public string kicker { get; set; }
			public string appears { get; set; }
			public string expires { get; set; }
			public string target { get; set; }
			public string gameId { get; set; }
			public string urlText { get; set; }
			public string url { get; set; }
			public string blurb { get; set; }
			//public object image { get; set; }
			//public object relatedLinks { get; set; }
		}
		class MediaWallResponse
		{
			public string title { get; set; }
			public List<MediaWallItem> items { get; set; }
		}
		public class Names
		{
			public string matchupName { get; set; }
			public string matchupShortName { get; set; }
			public string teamAbbreviationA { get; set; }
			public string teamAbbreviationB { get; set; }
			public string seriesSlug { get; set; }
		}
		public class Period
		{
			public string periodType { get; set; }
			public DateTime startTime { get; set; }
			public int num { get; set; }
			public string ordinalNum { get; set; }
			public TeamState home { get; set; }
			public TeamState away { get; set; }
		}
		public class Player
		{
			public PlayerInfo player { get; set; }
			public string playerType { get; set; }
			public int seasonTotal { get; set; }
		}
		public class PlayerInfo
		{
			public int id { get; set; }
			public string fullName { get; set; }
			public string link { get; set; }
		}
		public class PowerPlayInfo
		{
			public int situationTimeRemaining { get; set; }
			public int situationTimeElapsed { get; set; }
			public bool inSituation { get; set; }
		}
		public class Result
		{
			public string @event { get; set; }
			public string eventCode { get; set; }
			public string eventTypeId { get; set; }
			public string description { get; set; }
			public string secondaryType { get; set; }
			public Strength strength { get; set; }
			public bool emptyNet { get; set; }
		}
		public class Round
		{
			public int number { get; set; }
		}
		public class ScheduleResponse
		{
			public string copyright { get; set; }
			public int totalItems { get; set; }
			public int totalEvents { get; set; }
			public int totalGames { get; set; }
			public int totalMatches { get; set; }
			public int wait { get; set; }
			public List<Date> dates { get; set; }
		}
		public class ScoringPlay
		{
			public List<Player> players { get; set; }
			public Result result { get; set; }
			public About about { get; set; }
			public Coordinates coordinates { get; set; }
			public Team team { get; set; }
		}
		public class Series
		{
			public int seriesNumber { get; set; }
			public string seriesCode { get; set; }
			public Names names { get; set; }
			//public CurrentGame currentGame { get; set; }
			//public Conference3 conference { get; set; }
			public Round round { get; set; }
			//public List<MatchupTeam> matchupTeams { get; set; }
		}
		public class SeriesSummary
		{
			public int gamePk { get; set; }
			public int gameNumber { get; set; }
			public string gameLabel { get; set; }
			public bool necessary { get; set; }
			public int gameCode { get; set; }
			public DateTime gameTime { get; set; }
			public string seriesStatus { get; set; }
			public string seriesStatusShort { get; set; }
			public Series series { get; set; }
		}
		public class ShootoutInfo
		{
			public TeamShootoutScore away { get; set; }
			public TeamShootoutScore home { get; set; }
		}
		public class Status
		{
			public string abstractGameState { get; set; }
			public string codedGameState { get; set; }
			public string detailedState { get; set; }
			public string statusCode { get; set; }
			public bool startTimeTBD { get; set; }
		}
		public class Strength
		{
			public string code { get; set; }
			public string name { get; set; }
		}
		public class Team
		{
			public int id { get; set; }
			public string name { get; set; }
			public string link { get; set; }
			public Venue venue { get; set; }
			public string abbreviation { get; set; }
			public string teamName { get; set; }
			public string locationName { get; set; }
			public string firstYearOfPlay { get; set; }
			public Division division { get; set; }
			public Conference conference { get; set; }
			public Franchise franchise { get; set; }
			public string shortName { get; set; }
			public string officialSiteUrl { get; set; }
			public int franchiseId { get; set; }
			public bool active { get; set; }
		}
		public class Teams
		{
			public TeamStats away { get; set; }
			public TeamStats home { get; set; }

			public override string ToString()
			{
				return away.team.abbreviation + " @ " + home.team.abbreviation;
			}
		}
		public class TeamShootoutScore
		{
			public int scores { get; set; }
			public int attempts { get; set; }
		}
		class TeamsResponse
		{
			public string copyright { get; set; }
			public List<Team> teams { get; set; }
		}
		public class TeamState
		{
			public Team team { get; set; }
			public int goals { get; set; }
			public int shotsOnGoal { get; set; }
			public string rinkSide { get; set; }
			public bool goaliePulled { get; set; }
			public int numSkaters { get; set; }
			public bool powerPlay { get; set; }
		}
		public class TeamStats
		{
			public LeagueRecord leagueRecord { get; set; }
			public int score { get; set; }
			public Team team { get; set; }
		}
		public class TimeZone
		{
			public string id { get; set; }
			public int offset { get; set; }
			public string tz { get; set; }
		}
		public class Venue
		{
			public string name { get; set; }
			public string link { get; set; }
			public string city { get; set; }
			public TimeZone timeZone { get; set; }
		}
		#endregion
	}
}
