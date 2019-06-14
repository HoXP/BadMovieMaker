using BadMovieMaker.Components;
using System;
using System.Windows;
using System.Windows.Controls;

namespace BadMovieMaker.Controls
{
    public partial class MediaActor : Actor
    {
        private MediaComponent _cmp;

        public MediaActor(ulong id, string path = "") : base(id)
        {
            InitializeComponent();
            Name = "Media-" + id;
            _cmp = new MediaComponent(this);
            AddComponent(_cmp);
            if (string.IsNullOrEmpty(path))
            {
                path = "http://flv3.bn.netease.com/videolib3/1801/04/MsbTq9983/SD/MsbTq9983-mobile.mp4";
            }
            Media = new MediaElement()
            {
                Source = new Uri(string.Format("{0}", path))
            };
        }
        ~MediaActor()
        {
            RemoveComponent(_cmp);
            _cmp = null;
        }

        public MediaElement Media
        {
            get { return (MediaElement)GetValue(MediaProperty); }
            set { SetValue(MediaProperty, value); }
        }
        public static readonly DependencyProperty MediaProperty = DependencyProperty.Register("Media", typeof(MediaElement), typeof(MediaActor));
    }
}