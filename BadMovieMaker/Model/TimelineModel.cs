using System.Collections.ObjectModel;
using System;

namespace BadMovieMaker.Model
{
	public class TimelineModel : ModelBase
	{
		private ObservableCollection<TimelineAnimationModel> animationList = new ObservableCollection<TimelineAnimationModel>();

		public TimelineModel()
		{
			Random rad = new Random();
			for(int i = 0; i < rad.Next(20); i++)
			{
				TimelineAnimationModel anm = new TimelineAnimationModel();
				animationList.Add(anm);
			}
		}

		public ObservableCollection<TimelineAnimationModel> AnimationList { get => animationList; }
	}
}

