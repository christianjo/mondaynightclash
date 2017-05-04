using System;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;

namespace mondaynightclash
{


	public class TeamsListPage : FreshMvvm.FreshBaseContentPage
	{
		public TeamsListPage()
		{
			Padding = new Thickness(0, 20, 0, 0);

			var listView = new ListView
			{
				GroupDisplayBinding = new Binding("Name"),
				IsGroupingEnabled = true
			};
			listView.ItemTemplate = new DataTemplate(typeof(TextCell));
			listView.ItemTemplate.SetBinding(TextCell.TextProperty, "Name");
			listView.ItemTemplate.SetBinding(TextCell.DetailProperty, "OverallScore");

			listView.SetBinding(ListView.ItemsSourceProperty, "Teams");

			//Bruker basepage i stedet.
			//var closeModal = new ToolbarItem();
			//closeModal.Text = "Lukk modal";
			//closeModal.SetBinding(ToolbarItem.CommandProperty, "CloseModal");
			//ToolbarItems.Add(closeModal);

			Content = listView;
		}
	}
}
