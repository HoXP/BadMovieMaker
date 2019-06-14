namespace BadMovieMaker.Controls
{
    public partial class RectActor : Actor
    {
        public RectActor(ulong id) : base(id)
        {
            Name = "Rect-" + id;
            InitializeComponent();
        }
    }
}