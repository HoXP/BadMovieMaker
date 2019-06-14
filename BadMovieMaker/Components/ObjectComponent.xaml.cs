namespace BadMovieMaker.Components
{
    public partial class ObjectComponent : UCComponent
    {
        public ObjectComponent(ComponentsTrunk trunk) : base(trunk)
        {
            InitializeComponent();
            DataContext = Trunk;
        }
    }
}