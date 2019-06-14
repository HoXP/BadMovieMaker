using BadMovieMaker.Common;

namespace BadMovieMaker.Mgr
{
    internal class MainWindowMgr : SingletonBase<MainWindowMgr>
    {
        internal readonly string[] DefaultPercents = new string[] { "5*", "2*", "2*", "7*", "2*" };
        internal readonly string[] StageMaxPercents = new string[] { "7*", "0*", "0*", "11*", "0*" };

        public MainWindowMgr()
        {
            ResetStage();
        }

        private string[] _gridPercents = null;
        public string[] GridPercents
        {
            get { return _gridPercents; }
            set
            {
                //if(_gridPercents!=value)
                {
                    _gridPercents = value;
                    NotifyPropertyChanged("GridPercents");
                }
            }
        }

        internal void MaximizeStage()
        {
            GridPercents = StageMaxPercents;
            IsMax = true;
        }
        internal void ResetStage()
        {
            GridPercents = DefaultPercents;
            IsMax = false;
        }
        internal bool IsMax { get; private set; }
    }
}