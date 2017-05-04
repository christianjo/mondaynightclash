using System;
using System.Diagnostics;
using Xamarin.Forms;
using static Xamarin.Forms.Device;

namespace mondaynightclash
{
	public class MatchPageModel : FreshMvvm.FreshBasePageModel
	{

		public string Timer { get; set; }
		public TimeSpan TimeKeeper { get; set; }
		public int Ticks { get; set; }
		public string ButtonText { get; set; }
		public string GoalButtonText { get; set; }

		private bool timeIsRunning { get; set; }

		public override void Init(object initData)
		{
			base.Init(initData);
			Timer = "-- : -- : --";
			TimeKeeper = new TimeSpan(0, 6, 0);
			Ticks = 0;
			ButtonText = "Start/Pause";
			timeIsRunning = false;
			GoalButtonText = "Nytt mål!";
			Test = "te";
		}

		//Todo: Burde ikke få lov til å fjerne denne modalen uten at tiden er gått ut eller man avslutter kampen på en måte med advarsel.

		public Command StartTimer
		{
			get
			{
				return new Command(() =>
			  {
				  if (!timeIsRunning)
				  {
					  timeIsRunning = true;
					  StartTimer(new TimeSpan(0, 0, 1), () =>
								{
									Ticks++;
									Timer = TimeKeeper.Subtract(new TimeSpan(0, 0, Ticks)).ToString();
									return timeIsRunning;
								});
				  }
				  else
				  {
					  timeIsRunning = false;
				  }
			  });
			}
		}
		public string Test { get; set; }

		public Command GoalCommand
		{
			get
			{
				return new Command<string>((para) =>
			  {
				  CoreMethods.PushPageModel<SelectAssistAndGoalPageModel>(true,true);

			  });
			}
		}

	}
}
