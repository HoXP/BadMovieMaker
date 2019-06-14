using BadMovieMaker.Common;
using BadMovieMaker.Components;
using System.ComponentModel;

namespace BadMovieMaker.Mgr
{
    class InspectorMgr : SingletonBase<InspectorMgr>, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public InspectorMgr()
        {
        }

        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(null, new PropertyChangedEventArgs(propertyName));
        }

        #region CurObj
        internal void UpdateCurObj(ComponentsTrunk obj)
        {
            CurObj = obj;
        }
        private ComponentsTrunk _curObj;
        public ComponentsTrunk CurObj
        {
            get
            {
                return _curObj;
            }
            set
            {
                _curObj = value;
                NotifyPropertyChanged("CurObj");
            }
        }
        #endregion
    }
}