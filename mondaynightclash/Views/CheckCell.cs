using System;
using Xamarin.Forms;

namespace mondaynightclash
{
	public class CheckCell : ViewCell
	{
		public Label CellText { get; set; }
		public Image ImageDetail { get; set; }
        //test git
		public CheckCell()
		{
			Height = 60;

			CellText = new Label();
			CellText.FontAttributes = FontAttributes.Bold;
			CellText.SetBinding(Label.TextProperty, "Name");
			CellText.HorizontalOptions = LayoutOptions.StartAndExpand;

			ImageDetail = new Image();
			ImageDetail.HeightRequest = 25;
			ImageDetail.WidthRequest = 25;
			ImageDetail.HorizontalOptions = LayoutOptions.End;
			ImageDetail.SetBinding(Image.SourceProperty, "DetailImage");

			var CellStackLayout = new StackLayout();
			CellStackLayout.Children.Add(CellText);
			CellStackLayout.Children.Add(ImageDetail);
			
			CellStackLayout.Spacing = 10;
			CellStackLayout.Orientation = StackOrientation.Horizontal;
			CellStackLayout.HorizontalOptions = LayoutOptions.FillAndExpand;

			

			View = CellStackLayout;


		}
	}
}
