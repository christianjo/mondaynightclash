using System;

using Xamarin.Forms;

namespace mondaynightclash
{
	public class SelectAssistAndGoalPage : FreshMvvm.FreshBaseContentPage
	{
		public SelectAssistAndGoalPage()
		{
			var ListView = new ListView();
			ListView.SetBinding(ListView.ItemsSourceProperty, "PlayerList");
			ListView.ItemTemplate = new DataTemplate(typeof(CheckCell));
			ListView.SetBinding(ListView.SelectedItemProperty, "SelectedPlayer", BindingMode.TwoWay);

			var ListViewLayout = new StackLayout();
			ListViewLayout.Padding = new Thickness(10, 20);
			ListViewLayout.Children.Add(ListView);

			var toolbarItem = new ToolbarItem();
			toolbarItem.Text = "Done";
			toolbarItem.SetBinding(ToolbarItem.CommandProperty, "Done");
			ToolbarItems.Add(toolbarItem);

			Content = ListViewLayout;
		}
	}
}

