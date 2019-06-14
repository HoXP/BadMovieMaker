using BadMovieMaker.Components;
using BadMovieMaker.Define;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace BadMovieMaker.Controls
{
    public partial class ImageActor : Actor
    {
        private ImageComponent _cmp;

        public ImageActor(ulong id, string path = "") : base(id)
        {//path需要是绝对路径
            InitializeComponent();
            Name = "Image-" + id;
            _cmp = new ImageComponent(this);
            AddComponent(_cmp);
            if (string.IsNullOrEmpty(path))
            {
                path = string.Format("{0}{1}", PathDefine.PackBase, "Resources/Textures/17zuoye.png");
            }
            Pic = new Image()
            {
                Source = new BitmapImage(new Uri(string.Format("{0}", path)))
            };
        }
        ~ImageActor()
        {
            RemoveComponent(_cmp);
            _cmp = null;
        }

        public Image Pic
        {
            get { return (Image)GetValue(PicProperty); }
            set { SetValue(PicProperty, value); }
        }
        public static readonly DependencyProperty PicProperty = DependencyProperty.Register("Pic", typeof(Image), typeof(ImageActor));
    }
}