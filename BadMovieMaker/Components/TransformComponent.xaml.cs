using BadMovieMaker.Controls;
using System;
using System.Windows;
using System.Windows.Media;

namespace BadMovieMaker.Components
{
    public partial class TransformComponent : UCComponent
    {
        private Actor _act;

        public TransformComponent(ComponentsTrunk trunk) : base(trunk)
        {
            InitializeComponent();
            DataContext = this;
            _act = Trunk as Actor;
            Translate = _act.ACP.ttACP;
            Rotate = _act.ACP.rtACP;
            Scale = _act.ACP.stACP;
        }
        
        public TranslateTransform Translate
        {
            get { return (TranslateTransform)GetValue(TranslateProperty); }
            set { SetValue(TranslateProperty, value); }
        }
        public static readonly DependencyProperty TranslateProperty =
            DependencyProperty.Register("Translate", typeof(TranslateTransform), typeof(TransformComponent), new PropertyMetadata(new TranslateTransform()));

        public RotateTransform Rotate
        {
            get { return (RotateTransform)GetValue(RotateProperty); }
            set { SetValue(RotateProperty, value); }
        }
        public static readonly DependencyProperty RotateProperty =
            DependencyProperty.Register("Rotate", typeof(RotateTransform), typeof(TransformComponent), new PropertyMetadata(new RotateTransform()));

        public ScaleTransform Scale
        {
            get { return (ScaleTransform)GetValue(ScaleTransformProperty); }
            set { SetValue(ScaleTransformProperty, value); }
        }
        public static readonly DependencyProperty ScaleTransformProperty =
            DependencyProperty.Register("Scale", typeof(ScaleTransform), typeof(TransformComponent), new PropertyMetadata(new ScaleTransform()));
        private void txbScale_LostFocus(object sender, RoutedEventArgs e)
        {
            _act.ACP.ScaleCP();
        }
    }
}