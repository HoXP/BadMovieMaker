using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BadMovieMaker.Model;
using System.Collections.ObjectModel;

namespace BadMovieMaker.Mgr
{
   public static class ModelsHolder
    {
        public static ObservableCollection<TimelineTreeItemModel> timelineTreeModelList = new ObservableCollection<TimelineTreeItemModel>();
		public static TimelineModel timelineModel = new TimelineModel();
    }
}
