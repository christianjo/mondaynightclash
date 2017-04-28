using System;

using Xamarin.Forms;

namespace mondaynightclash
{
	public class MatchPage : BasePage
	{
		public MatchPage()
		{

			var stacklayout = new StackLayout();

			var timer = new Label();
			timer.HorizontalOptions = LayoutOptions.CenterAndExpand;
			timer.SetBinding(Label.TextProperty, "Timer");

			var startStopButton = new Button();
			startStopButton.SetBinding(Button.TextProperty, "ButtonText");
			startStopButton.SetBinding(Button.CommandProperty, "StartTimer");

			var teamScoreStacklayout = new StackLayout();
			teamScoreStacklayout.Orientation = StackOrientation.Horizontal;

			var homeSl = new StackLayout();
			homeSl.Orientation = StackOrientation.Vertical;
			var homeTeam = new Label
			{
				Text = "Blått",
			};
			var homeScore = new Label
			{
				Text = "0",
			};
			homeSl.Children.Add(homeTeam);
			homeSl.Children.Add(homeScore);


			var awaySl = new StackLayout();
			awaySl.Orientation = StackOrientation.Vertical;
			var awayTeam = new Label
			{
				Text = "Rødt"
			};
			var awayScore = new Label
			{
				Text = "0",
			};

			var goalButton = new Button();
			goalButton.SetBinding(Button.TextProperty, "GoalButtonText");
			goalButton.SetBinding(Button.CommandParameterProperty, "ButtonText");
			goalButton.SetBinding(Button.CommandProperty, "GoalCommand");

			awaySl.Children.Add(awayTeam);
			awaySl.Children.Add(awayScore);

			teamScoreStacklayout.Children.Add(homeSl);
			teamScoreStacklayout.Children.Add(awaySl);

			stacklayout.Children.Add(timer);
			stacklayout.Children.Add(startStopButton);
			stacklayout.Children.Add(teamScoreStacklayout);
			stacklayout.Children.Add(goalButton);

			Content = stacklayout;
		}


	}
}

