using System;
using System.Collections.Generic;
using System.Windows.Input;
using Acr.UserDialogs;

namespace mondaynightclash
{
	public class AllPlayersListPageModel : FreshMvvm.FreshBasePageModel
	{
		public List<Player> PlayerList { get; set; }

		readonly IDataService dataservice;
		readonly IUserDialogs userDialogs;
		public bool initialized;

		public AllPlayersListPageModel(IDataService dataservice, IUserDialogs userDialogs)
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

		public override void ReverseInit(object returnedData)
		{
			base.ReverseInit(returnedData);
		}

		public Player SelectedPlayer
		{
			get { return null; }
			set
			{
				CoreMethods.PushPageModel<PlayerPageModel>(value);
				RaisePropertyChanged();
			}

		}

		protected override void ViewIsAppearing(object sender, EventArgs e)
		{
			base.ViewIsAppearing(sender, e);
			if(!initialized)
				userDialogs.ShowLoading();
			
		}
	}
}
