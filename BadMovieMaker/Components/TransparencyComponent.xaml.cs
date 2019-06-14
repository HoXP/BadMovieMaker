namespace BadMovieMaker.Components
{
    public partial class TransparencyComponent : UCComponent
    {
        public TransparencyComponent(ComponentsTrunk trunk) : base(trunk)
        {
            InitializeComponent();
            DataContext = Trunk;
        }
    }
}