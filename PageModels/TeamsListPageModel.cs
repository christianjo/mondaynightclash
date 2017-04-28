using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Acr.UserDialogs;
using Xamarin.Forms;

namespace mondaynightclash
{
	public class TeamsListPageModel : FreshMvvm.FreshBasePageModel
	{
		public ObservableCollection<Team> Teams { get; set; }

		readonly IDataService dataService;
		readonly IUserDialogs userDialogs;

		public TeamsListPageModel(IDataService dataService, IUserDialogs userDialogs)
		{
			this.userDialogs = userDialogs;
			this.dataService = dataService;
		}

		public override void Init(object initData)
		{
			base.Init(initData);
			//Teams = dataService.GetMockTeamsAndPlayers();
			//userDialogs.HideLoading();
			//SetupTeams();
		}

		protected override void ViewIsAppearing(object sender, EventArgs e)
		{
			base.ViewIsAppearing(sender, e);
			Teams = dataService.GetMockTeamsAndPlayers();

			//SetupTeamsAlgo();
		}

		public Command CloseModal
		{
			get
			{
				return new Command(() =>
			  {
					CoreMethods.PopModalNavigationService();
			  });
			}
		}

	}
}
