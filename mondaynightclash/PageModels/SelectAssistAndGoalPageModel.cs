using System;
using System.Collections.Generic;
using Acr.UserDialogs;
using Xamarin.Forms;

namespace mondaynightclash
{
	public class SelectAssistAndGoalPageModel : FreshMvvm.FreshBasePageModel
	{
		public List<Player> PlayerList { get; set; }

		public SelectAssistAndGoalPageModel()
		{
		}

		readonly IDataService dataservice;
		readonly IUserDialogs userDialogs;

		public SelectAssistAndGoalPageModel(IDataService dataservice, IUserDialogs userDialogs)
		{
			this.userDialogs = userDialogs;
			this.dataservice = dataservice;
		}

		public override async void Init(object initData)
		{
			base.Init(initData);

			PlayerList = await dataservice.GetMockPlayers();
			userDialogs.HideLoading();
			//initialized = true;
		}

		public Player SelectedPlayer
		{
			get { return null; }
			set
			{
				value.IsSelected = !value.IsSelected;
			}
		}

		public Command Done
		{
			get
			{
				return new Command(async () =>
			   {
				   await CoreMethods.PopPageModel(true, true);
			   });
			}
		}

	}
}
