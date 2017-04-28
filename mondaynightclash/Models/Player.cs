using System;
using System.Drawing;
using PropertyChanged;
//using SQLite;

namespace mondaynightclash
{
	[ImplementPropertyChanged]
	//[Table(nameof(Player))]
	public class Player
	{
		//[PrimaryKey, AutoIncrement]
		public int? Id { get; set; }

		public Player(string name)
		{
			Name = name;
		}
		public Player() { }

		public string Name { get; set; }
		public string TeamColor { get; set; }
		public bool IsSelected { get; set; }

		//Stats
		public int TotalPractices { get; set; }
		public int TotalGoals { get; set; } // 25%
		public int TotalAssists { get; set; } // 16%
		public int TotalOwnGoals { get; set; } // 1%
		public int TotalGamePoints { get; set; } // 10%
		public int Rating { get; set; } // 48%
		

		public double OverallScore{
			get{
				return (AvgGoals*25)+(AvgAssists*16)-(AvgAssists*1)+(AvgGamePoints*10)+(Rating*48);
			}
			
		}

		public double AvgGoals
		{
			get
			{
				return TotalGoals / TotalPractices;
			}
		}
		public double AvgAssists
		{
			get
			{
				return TotalAssists / TotalPractices;

			}
		}
		public double AvgOwnGoals
		{
			get
			{
				return TotalOwnGoals / TotalPractices;
			}
		}
		public double AvgGamePoints
		{
			get
			{
				return TotalGamePoints / TotalPractices;
			}
		}

		public string DetailImage
		{
			get
			{
				if (IsSelected)
				{
					return "ic_checkmark_active";
				}
				return "ic_checkmark_unactive";
			}
		}

	}
}
