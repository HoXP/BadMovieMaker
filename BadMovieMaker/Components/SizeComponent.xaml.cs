namespace BadMovieMaker.Components
{
    public partial class SizeComponent : UCComponent
    {
        public SizeComponent(ComponentsTrunk trunk) : base(trunk)
        {
            InitializeComponent();
            DataContext = Trunk;
        }
    }
}