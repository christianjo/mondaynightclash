using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
//using SQLite;

namespace mondaynightclash
{

	public interface IDataService
	{
		Task<List<Player>> GetMockPlayers();
		Task<ObservableCollection<Player>> GetObservablePlayers();
		void CreatePractice(List<Player> PlayerList);
		List<Team> GetMockTeams();
		void SetupTeamsForPractice();
		ObservableCollection<Team> GetMockTeamsAndPlayers();
	}

	public class DataService : IDataService
	{

		public static List<Player> Players;
		public static List<Player> ActivePlayers;
		public static ObservableCollection<Team> Teams;

	//	private readonly SQLiteAsyncConnection conn;

		public string StatusMessage { get; set; }

		public DataService()
		{
			AddPlayers();
		}

		public DataService(string dbPath)
		{
			//conn = new SQLiteAsyncConnection(dbPath);
			//conn.CreateTableAsync<Player>().Wait();
		}

/*		public async Task CreatePlayer(Player player)
		{
			try
			{
				if (string.IsNullOrWhiteSpace(player.Name))
					throw new Exception("Name is required");

				// Insert/update contact.
				var result = await conn.InsertOrReplaceAsync(player).ConfigureAwait(continueOnCapturedContext: false);
				StatusMessage = $"{result} record(s) added [Player Name: {player.Name}])";
			}
			catch (Exception ex)
			{
				StatusMessage = $"Failed to create player: {player.Name}. Error: {ex.Message}";
			}
		}*/

		public ObservableCollection<Team> GetMockTeamsAndPlayers()
		{
			return Teams;
		}

		/*public Task<List<Player>> GetAll()
		{
			// Return a list of bills saved to the Bill table in the database.
			//return conn.Table<Player>().ToListAsync();
		}*/

		public List<Team> GetMockTeams()
		{
			return new List<Team>{
				new Team{ Name = "Rødt"},
				new Team{ Name = "Blå"},
				new Team{ Name = "Grønn"}
			};

		}
		public async Task<List<Player>> GetMockPlayers()
		{
			await Task.Delay(TimeSpan.FromSeconds(1));
			return Players;
		}

		public async Task<ObservableCollection<Player>> GetObservablePlayers()
		{
			await Task.Delay(TimeSpan.FromSeconds(2));
			return new ObservableCollection<Player> {
					new Player{ Name = "Kent", TeamColor = "Red", IsSelected= true },
					new Player{ Name = "Christian", TeamColor = "Blue",IsSelected= true },
					new Player{ Name = "Liam", TeamColor = "Green",IsSelected= true }
			};
		}

		public void CreatePractice(List<Player> PlayerList)
		{
			ActivePlayers = PlayerList.FindAll(p => p.IsSelected).ToList();
			SetupTeamsForPractice();

		}

		public void SetupTeamsForPractice()
		{
			var players = ActivePlayers.OrderByDescending(p => p.OverallScore).ToList();

			//Fordeler alle spillere i 3x5 grupper.
			var groupsOfThree = SplitIntoEvenGroups(players.Count, 3);
			var rankTopToBottom = new List<List<Player>>();
			var oldAmount = 0;
			foreach (var amount in groupsOfThree)
			{
				var skippedPlayers = players.Skip(oldAmount).Take(amount).ToList();
				if (oldAmount != 0)
				{
					skippedPlayers.Reverse();
				}
				rankTopToBottom.Add(skippedPlayers);
				oldAmount += amount;
			}

			//Fordele grupperingen på 3x5 til 3 lag.
			var actualGroups = SplitIntoEvenGroups(players.Count, 5).Count;
			var team1 = new Team("Rødt Lag", "Red");
			var team2 = new Team("Blått Lag", "Blue");
			var team3 = new Team("Grønt Lag", "Green");

			foreach (var _group in rankTopToBottom)
			{
				var count = actualGroups;

				foreach (var player in _group)
				{
					switch (count)
					{
						case 1:
							team1.Add(player);
							break;
						case 2:
							team2.Add(player);
							break;
						case 3:
							team3.Add(player);
							break;
					}
					count--;
				}
			}

			//Todo: Crap code. Fiks så man automatisk får riktig antall teams.
			if (team3.Count() == 0)
			{
				Teams = new ObservableCollection<Team>{
					team1,team2
				};
			}
			else
			{
				Teams = new ObservableCollection<Team>{
					team1,team2, team3
				};
			}
		}


		public int AmountOfGroups(int amount, int maxPlayers)
		{
			int amountGroups = amount / maxPlayers;

			if ((amountGroups * maxPlayers) < amount)
			{
				amountGroups++;
			}

			return amountGroups;
		}

		public List<int> SplitIntoEvenGroups(int amount, int maxPlayers)
		{
			int amountGroups = AmountOfGroups(amount, maxPlayers);

			int groupsLeft = amountGroups;
			List<int> result = new List<int>();
			while (amount > 0)
			{
				int nextGroupValue = amount / groupsLeft;
				if (nextGroupValue * groupsLeft < amount)
				{
					nextGroupValue++;
				}
				result.Add(nextGroupValue);
				groupsLeft--;
				amount -= nextGroupValue;
			}
			return result;
		}


		void AddPlayers()
		{
			Players = new List<Player>{
				new Player{ Name = "Kent", TeamColor = "Blue" ,IsSelected = true , Rating=3, TotalAssists= 11, TotalGamePoints= 30, TotalGoals=20, TotalOwnGoals=0, TotalPractices= 17 },
					new Player{ Name = "Christian" , TeamColor = "Blue" , IsSelected = true, Rating=2, TotalAssists= 23, TotalGamePoints= 30, TotalGoals=20, TotalOwnGoals=0, TotalPractices= 27},
					new Player{ Name = "Liam", TeamColor = "Blue" ,IsSelected = true , Rating=3, TotalAssists= 2, TotalGamePoints= 30, TotalGoals=20, TotalOwnGoals=0, TotalPractices= 47},
					new Player{ Name = "Per", TeamColor = "Blue" , IsSelected = true , Rating=1, TotalAssists= 13, TotalGamePoints= 30, TotalGoals=20, TotalOwnGoals=0, TotalPractices= 11},
					new Player{ Name = "Olav" , Rating=3, TotalAssists= 10, TotalGamePoints= 30, TotalGoals=20, TotalOwnGoals=0, TotalPractices= 17},
					new Player{ Name = "Pål" , TeamColor = "Blue" ,IsSelected = true, Rating=4, TotalAssists= 32, TotalGamePoints= 30, TotalGoals=20, TotalOwnGoals=0, TotalPractices= 19},
					new Player{ Name = "Kenneth",TeamColor = "Red" , IsSelected = true , Rating=5, TotalAssists= 10, TotalGamePoints= 30, TotalGoals=20, TotalOwnGoals=0, TotalPractices= 25},
					new Player{ Name = "Sivert",TeamColor = "Red" , IsSelected = true , Rating=3, TotalAssists= 10, TotalGamePoints= 30, TotalGoals=20, TotalOwnGoals=0, TotalPractices= 25},
					new Player{ Name = "Svein" , Rating=3, TotalAssists= 11, TotalGamePoints= 30, TotalGoals=20, TotalOwnGoals=0, TotalPractices= 17},
					new Player{ Name = "Albin" ,TeamColor = "Red" , IsSelected = true, Rating=2, TotalAssists= 10, TotalGamePoints= 30, TotalGoals=20, TotalOwnGoals=0, TotalPractices= 17},
					new Player{ Name = "Wilmer", TeamColor = "Red" ,IsSelected = true , Rating=2, TotalAssists= 43, TotalGamePoints= 30, TotalGoals=20, TotalOwnGoals=0, TotalPractices= 18},
					new Player{ Name = "Martin", TeamColor = "Green",IsSelected = true , Rating=5, TotalAssists= 10, TotalGamePoints= 30, TotalGoals=20, TotalOwnGoals=0, TotalPractices= 20},
					new Player{ Name = "Per Olav" , Rating=3, TotalAssists= 10, TotalGamePoints= 30, TotalGoals=20, TotalOwnGoals=0, TotalPractices= 17},
					new Player{ Name = "Tommy", TeamColor = "Red" ,IsSelected = true , Rating=4, TotalAssists= 3, TotalGamePoints= 30, TotalGoals=20, TotalOwnGoals=0, TotalPractices= 57},
					new Player{ Name = "Petter", TeamColor = "Green" ,IsSelected = true , Rating=2, TotalAssists= 10, TotalGamePoints= 30, TotalGoals=20, TotalOwnGoals=0, TotalPractices= 18},
					new Player{ Name = "Anders",TeamColor = "Green" , IsSelected = true , Rating=1, TotalAssists= 10, TotalGamePoints= 30, TotalGoals=20, TotalOwnGoals=0, TotalPractices= 17},
					new Player{ Name = "Andreas", TeamColor = "Green" ,IsSelected = true , Rating=4, TotalAssists= 10, TotalGamePoints= 30, TotalGoals=20, TotalOwnGoals=0, TotalPractices= 17},
					new Player{ Name = "Christoffer", TeamColor = "Green" ,IsSelected = true , Rating=2, TotalAssists= 10, TotalGamePoints= 30, TotalGoals=20, TotalOwnGoals=0, TotalPractices= 17},
					new Player{ Name = "Stein Olav" , Rating=3, TotalAssists= 10, TotalGamePoints= 30, TotalGoals=20, TotalOwnGoals=0, TotalPractices= 17}
				};
		}
	}
}
