using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadMovieMaker.Utils
{
    public static class AppHelper
    {
        /// <summary>
        /// 毫秒转化为秒
        /// </summary>
        /// <param name="millionSecs"></param>
        /// <returns></returns>
        public static float MillionSecsToSecs(int millionSecs)
        {
            return millionSecs / 1000.0f;
        }

        /// <summary>
        /// 秒转化为毫秒
        /// </summary>
        /// <param name="Secs"></param>
        /// <returns></returns>
        public static int SecsToMillionSecs( float Secs )
        {
            return (int)(Secs * 1000);
        }

        public static string MillionSecsToFormattedString(int millionSecs)
        {
            int millions = millionSecs % 1000;
            int seconds = millionSecs / 1000;
            int minutes = seconds / 60;
            seconds = seconds % 60;

            return string.Format("{0:D2}:{1:D2}.{2:D3}", minutes, seconds, millions);
        }
    }
}
