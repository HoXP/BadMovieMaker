namespace BadMovieMaker.Controls
{
    public partial class EllipseActor : Actor
    {
        public EllipseActor(ulong id) : base(id)
        {
            Name = "Ellipse-" + id;
            InitializeComponent();
        }
    }
}