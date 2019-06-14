using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadMovieMaker.Model
{
	public enum KeyFrameType
	{
		IndependentKF, //独立帧
		LoopKF, //循环帧
	}
	public class TimelineItemModel : ModelBase
	{
		public bool isSelected { get; set; }
		public int time { get; set; }
	}
}
