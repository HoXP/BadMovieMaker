using BadMovieMaker.Components;
using System.Windows;

namespace BadMovieMaker.Controls
{
    public partial class FontActor : Actor
    {
        private FontComponent _font;

        public FontActor(ulong id, string text = "") : base(id)
        {
            InitializeComponent();
            Name = "Font-" + id;
            Width = 100;
            Height = 30;
            if (string.IsNullOrEmpty(text))
            {
                Text = Name;
            }
            else
            {
                Text = text;
            }
            _font = new FontComponent(this);
            AddComponent(_font);
        }
        ~FontActor()
        {
            RemoveComponent(_font);
            _font = null;
        }

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(FontActor));
    }
}
