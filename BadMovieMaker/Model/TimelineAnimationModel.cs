using System.Collections.ObjectModel;
using System;
using System.Linq;

namespace BadMovieMaker.Model
{
	public class TimelineAnimationModel :ModelBase
	{
		public bool  collapsed{ get; set; }
		public ObservableCollection<TimelineItemModel> ItemsList { get => itemsList;}

		private ObservableCollection<TimelineItemModel> itemsList = new ObservableCollection<TimelineItemModel>();

		public TimelineAnimationModel()
		{
			Random rad = new Random(DateTime.Now.Millisecond);
			for(int i = 0; i < 20; i++)
			{
				TimelineItemModel item = new TimelineItemModel();
				item.time = rad.Next(100000);
				itemsList.Add(item);
			}
			itemsList.OrderBy(item => item.time);

			//for(int i = 0; i <20; i++)
			//{
			//	Console.WriteLine("####: " + itemsList[i].time);
			//}
		}
	}
}
