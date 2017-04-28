using System;
using System.Collections.Generic;
using Acr.UserDialogs;
using FreshMvvm;
using Xamarin.Forms;

namespace mondaynightclash
{

	public class SelectPlayersListPageModel : FreshMvvm.FreshBasePageModel
	{
		public List<Player> PlayerList { get; set; }

		public bool initialized { get; private set; }

		readonly IDataService dataservice;
		readonly IUserDialogs userDialogs;

		public SelectPlayersListPageModel(IDataService dataservice, IUserDialogs userDialogs)
		{
			this.userDialogs = userDialogs;
			this.dataservice = dataservice;
		}

		public override async void Init(object initData)
		{
			base.Init(initData);

			PlayerList = await dataservice.GetMockPlayers();
			userDialogs.HideLoading();
			initialized = true;
		}

		public Player SelectedPlayer
		{
			get { return null; }
			set
			{
				value.IsSelected = !value.IsSelected;
			}
		}

		public Command Start
		{
			get
			{
				return new Command(async () =>
			   {
				   dataservice.CreatePractice(PlayerList);
				   //Trenger ikke bytte hele RootNavigation
				   //CoreMethods.SwitchOutRootNavigation(NavigationStacks.GameNavigationStackTabbed);
				   var tabbedNavigation = new FreshTabbedNavigationContainer();
				   tabbedNavigation.AddTab<TablePageModel>("Tabell", null, null);
				   tabbedNavigation.AddTab<TeamsListPageModel>("Teams", null, null);
				   await CoreMethods.PushNewNavigationServiceModal(tabbedNavigation);
			   });
			}
		}

		protected override void ViewIsAppearing(object sender, System.EventArgs e)
		{
			base.ViewIsAppearing(sender, e);

			if (!initialized)
				userDialogs.ShowLoading();
		}
	}
}
