using BadMovieMaker.Mgr;
using System.Windows;

namespace BadMovieMaker.Components
{
    public partial class StageSizeComponent : UCComponent
    {
        public StageSizeComponent(ComponentsTrunk trunk) : base(trunk)
        {
            InitializeComponent();
            DataContext = this;
        }

        public Vector Size
        {
            get { return (Vector)GetValue(SizeProperty); }
            set { SetValue(SizeProperty, value); }
        }
        public static readonly DependencyProperty SizeProperty =
            DependencyProperty.Register("Size", typeof(Vector), typeof(StageSizeComponent), new PropertyMetadata(StageMgr.Instance.StageSize), SizeCallback);
        private static bool SizeCallback(object value)
        {
            StageMgr.Instance.StageSize = (Vector)value;
            return true;
        }
    }
}