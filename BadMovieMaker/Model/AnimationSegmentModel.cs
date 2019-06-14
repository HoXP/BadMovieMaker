using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadMovieMaker.Model
{
    public abstract class AnimationSegmentModel : ModelBase
    {
        private int startTime;
        private int duration;

        /// <summary>
        /// 动画的开始时间（ms）
        /// </summary>
        protected int StartTime
        {
            get
            {
                return startTime;
            }

            set
            {
                startTime = value;
                NotifyPropertyChanged("StartTime");
            }
        }

        /// <summary>
        /// 动画的持续时间（ms )
        /// </summary>
        protected int Duration
        {
            get
            {
                return duration;
            }

            set
            {
                duration = value;
                NotifyPropertyChanged("Duration");
            }
        }
    }
}
