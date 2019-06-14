using BadMovieMaker.Controls;

namespace BadMovieMaker.Components
{
    public partial class FontComponent : UCComponent
    {
        private FontActor _act;

        public FontComponent(ComponentsTrunk trunk) : base(trunk)
        {
            InitializeComponent();
            _act = Trunk as FontActor;
            DataContext = _act;
            cmbFont.SelectedIndex = 0;
        }
    }
}