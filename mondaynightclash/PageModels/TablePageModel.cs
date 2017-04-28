using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using Xamarin.Forms;

namespace mondaynightclash
{
	public class TablePageModel : FreshMvvm.FreshBasePageModel
	{
		public ObservableCollection<Team> Teams { get; set; }
		readonly IDataService dataservice;
		public int NextMatchNumber { get; set; }
		List<Tuple<int, Team, Team, bool>> Tournamet;

		public Team Home { get; set; }
		public Team Away { get; set; }

		public TablePageModel(IDataService dataservice)
		{
			this.dataservice = dataservice;
		}

		public override void Init(object initData)
		{
			base.Init(initData); 

		}

		protected override void ViewIsAppearing(object sender, EventArgs e)
		{
			base.ViewIsAppearing(sender, e);
			//Todo: Legges et annet sted? 
			if (Teams == null)
			{
				Teams = dataservice.GetMockTeamsAndPlayers();
				CreateTournament(Teams.ToList(), 5);
			}

			if (Tournamet != null)
			{
				var nextMatch = Tournamet.FirstOrDefault(t => t.Item4 == false);
				Home = nextMatch.Item2;
				Away = nextMatch.Item3;
			}

            //UpdateTable();

		}

		//public void UpdateTable(){

		//	Teams = (ObservableCollection<Team>)Teams.OrderByDescending(team => team.Points);
		//}

		public Command StartMatchCommand
		{
			get
			{
				return new Command(async () =>
			   {
				   //Todo regge i DB
				   var remove = Tournamet.FirstOrDefault(c => c.Item4 == false);
				   var idx = Tournamet.FindIndex(ta => ta.Item4 == false);

				   var newtup = new Tuple<int, Team, Team, bool>(remove.Item1, remove.Item2, remove.Item3, true);
				   Tournamet.RemoveAt(idx);
				   Tournamet.Insert(idx, newtup);

				   await CoreMethods.PushPageModel<MatchPageModel>(true, true);
			   });
			}
		}

		public void CreateTournament(List<Team> ListTeam, int rounds)
		{
			//       kamp# , hjemme, borte, kamp spilt
			List<Tuple<int, Team, Team, bool>> games = new List<Tuple<int, Team, Team, bool>>();
			List<Team> teams = new List<Team>();

			var numberOfMatchesToPlay = ListTeam.Count * rounds;
			var counter = 1;
			Team lastPlayedTeam = null;

			while (numberOfMatchesToPlay >= counter)
			{
				foreach (var team in ListTeam)
				{
					//Lag en kopi og fjern "team" slik at ikke samme lag møtes(Rød vs Rød)
					teams.AddRange(ListTeam);
					teams.Remove(team);

					//Finner ut om man må rotere eller ikke
					if ((lastPlayedTeam != null) && (games.Count > 0))
					{
						if (teams.FirstOrDefault().Equals(lastPlayedTeam))
						{
							teams.Reverse();
							Debug.WriteLine("Rotate");
						}
					}

					foreach (var team2 in teams)
					{
						games.Add(new Tuple<int, Team, Team, bool>(counter, team, team2, false));
						Debug.WriteLine("" + counter + "   " + team.Name + " " + team2.Name);
						counter++;
					}


					lastPlayedTeam = team;
					teams.Clear();
				}

			}

			Tournamet = games;
		}
	}
}
