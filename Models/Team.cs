using System.Collections.ObjectModel;
using System.Drawing;

namespace mondaynightclash
{
	public class Team : ObservableCollection<Player>
	{
		public Team(string name, string color)
		{
			Color = color;
			Name = name;
			Points = 0;
			Goals = 0;
		}

		public Team() { }
		public string Name { get; set; }

		//Farge
		public string Color { get; set; }

		public int Points { get; set; }
		public int Goals { get; set; }
	}
}
