using System;
namespace mondaynightclash
{
	public class PlayerPageModel : FreshMvvm.FreshBasePageModel
	{
		public Player Player
		{
			get;
			set;
		}

		public override void Init(object initData)
		{
			base.Init(initData);

			Player = initData as Player;

		}

		protected override void ViewIsAppearing(object sender, EventArgs e)
		{
			base.ViewIsAppearing(sender, e);
			//CoreMethods.DisplayAlert("Dude", "Where ma", "car=");
			//CoreMethods.DisplayActionSheet("Title","cancel", "destruction", "button1", "Button2");
			
		}
	}
}
