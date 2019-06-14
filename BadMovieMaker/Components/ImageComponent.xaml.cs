using BadMovieMaker.Controls;
using BadMovieMaker.Mgr;
using System;
using System.Windows;
using System.Windows.Media.Imaging;

namespace BadMovieMaker.Components
{
    public partial class ImageComponent : UCComponent
    {
        private ImageActor _act;

        public ImageComponent(ComponentsTrunk trunk) : base(trunk)
        {
            InitializeComponent();
            _act = Trunk as ImageActor;
            DataContext = _act;
        }

        private void img_Drop(object sender, DragEventArgs e)
        {
            FileData dobj = e.Data.GetData(typeof(FileData)) as FileData;
            if(dobj != null)
            {
                _act.Pic.Source = new BitmapImage(new Uri(dobj.Thumbnail));
            }
        }
    }
}