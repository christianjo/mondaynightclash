
//using System;
//using System.ComponentModel;
//using System.Runtime.CompilerServices;
//using Xamarin.Forms;
//using ZeroFiveBit.Utils;

//namespace ZeroFiveBit.Utils
//{
//	/// <summary>
//	/// Countdown timer with periodical ticks.
//	/// </summary>
//	public class Countdown : INotifyPropertyChanged
//	{
//		/// <summary>
//		/// Gets the start date time.
//		/// </summary>
//		public DateTime StartDateTime { get; private set; }

//		/// <summary>
//		/// Gets the remain time in seconds.
//		/// </summary>
//		public double RemainTime
//		{
//			get { return remainTime; }

//			private set
//			{
//				remainTime = value;
//				OnPropertyChanged();
//			}
//		}

//		/// <summary>
//		/// Occurs when completed.
//		/// </summary>
//		public event Action Completed;

//		/// <summary>
//		/// Occurs when ticked.
//		/// </summary>
//		public event Action Ticked;

//		/// <summary>
//		/// The timer.
//		/// </summary>
//		Timer timer;

//		/// <summary>
//		/// The remain time.
//		/// </summary>
//		double remainTime;

//		/// <summary>
//		/// The remain time total.
//		/// </summary>
//		double remainTimeTotal;

//		/// <summary>
//		/// Starts the updating with specified period, total time and period are specified in seconds.
//		/// </summary>
//		public void StartUpdating(double total, double period = 1.0)
//		{
//			if (timer != null)
//			{
//				StopUpdating();
//			}

//			remainTimeTotal = total;
//			RemainTime = total;

//			StartDateTime = DateTime.Now;

//			timer = new Timer(period * 1000);
//			timer = new Timer();
//			timer.Elapsed += (sender, e) => Tick();
//			timer.Enabled = true;
//		}

//		/// <summary>
//		/// Stops the updating.
//		/// </summary>
//		public void StopUpdating()
//		{
//			RemainTime = 0;
//			remainTimeTotal = 0;

//			if (timer != null)
//			{
//				timer.Enabled = false;
//				timer = null;
//			}
//		}

//		/// <summary>
//		/// Updates the time remain.
//		/// </summary>
//		public void Tick()
//		{
//			var delta = (DateTime.Now - StartDateTime).TotalSeconds;

//			if (delta < remainTimeTotal)
//			{
//				RemainTime = remainTimeTotal - delta;

//				var ticked = Ticked;
//				if (ticked != null)
//				{
//					ticked();
//				}
//			}
//			else
//			{
//				RemainTime = 0;

//				var completed = Completed;
//				if (completed != null)
//				{
//					completed();
//				}
//			}
//		}

//		#region INotifyPropertyChanged implementation

//		/// <summary>
//		/// Occurs when property changed.
//		/// </summary>
//		public event PropertyChangedEventHandler PropertyChanged;

//		/// <summary>
//		/// Raises the property changed event.
//		/// </summary>
//		private void OnPropertyChanged([CallerMemberName] string propertyName = null)
//		{
//			var handler = PropertyChanged;
//			if (handler != null)
//			{
//				handler(this, new PropertyChangedEventArgs(propertyName));
//			}
//		}

//		#endregion
//	}
//}

//namespace CountdownExample
//{
//	public class App
//	{
//		static Countdown countdown;

//		public static Page GetMainPage()
//		{
//			var label = new Label
//			{
//				Text = "Hello, Forms!",
//				VerticalOptions = LayoutOptions.CenterAndExpand,
//				HorizontalOptions = LayoutOptions.CenterAndExpand,
//			};

//			countdown = new Countdown();
//			countdown.StartUpdating(3600 * 24);

//			label.SetBinding(Label.TextProperty,
//				new Binding("RemainTime", BindingMode.Default, new CountdownConverter()));
//			label.BindingContext = countdown;

//			return new ContentPage
//			{
//				Content = label,
//			};
//		}
//	}
//}



//namespace ZeroFiveBit.Utils
//{
//	/// <summary>
//	/// Converts countdown seconds double value to string "HH : MM : SS"
//	/// </summary>
//	public class CountdownConverter : IValueConverter
//	{
//		#region IValueConverter implementation

//		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
//		{
//			var timespan = TimeSpan.FromSeconds((double)value);

//			if (timespan.TotalSeconds < 1.0)
//			{
//				return "-- : --";
//			}
//			//            else if (timespan.TotalSeconds < 3600)
//			//            {
//			//                return string.Format("{0:D2} : {1:D2}",
//			//                    timespan.Minutes, timespan.Seconds);
//			//            }
//			else if (timespan.TotalSeconds > 3600 * 24)
//			{
//				return "24 : 00 : 00";
//			}

//			return string.Format("{0:D2} : {1:D2} : {2:D2}",
//				timespan.Hours, timespan.Minutes, timespan.Seconds);
//		}

//		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
//		{
//			throw new NotImplementedException();
//		}

//		#endregion
//	}
//}


