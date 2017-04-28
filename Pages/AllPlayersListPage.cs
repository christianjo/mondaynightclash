using Xamarin.Forms;

namespace mondaynightclash
{
	public class AllPlayersListPage : ContentPage
	{
		public AllPlayersListPage()
		{
			ListView listView = new ListView();

			listView.SetBinding(ItemsView<Cell>.ItemsSourceProperty, "PlayerList");
			listView.SetBinding(ListView.SelectedItemProperty, "SelectedPlayer");
			listView.ItemTemplate = new DataTemplate(typeof(SwitchCell));
			listView.ItemTemplate.SetBinding(SwitchCell.TextProperty, "Name");

			Padding = new Thickness(0, 20, 0, 0);

			Content = listView;
		}

	}
}

