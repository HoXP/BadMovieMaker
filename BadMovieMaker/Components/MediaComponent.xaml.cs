using BadMovieMaker.Controls;
using BadMovieMaker.Mgr;
using System;
using System.Windows;

namespace BadMovieMaker.Components
{
    public partial class MediaComponent : UCComponent
    {
        private MediaActor _act;

        public MediaComponent(ComponentsTrunk trunk) : base(trunk)
        {
            InitializeComponent();
            _act = Trunk as MediaActor;
            DataContext = _act;
        }

        private void mda_Drop(object sender, DragEventArgs e)
        {
            FileData fd = e.Data.GetData(typeof(FileData)) as FileData;
            if (fd != null)
            {
                _act.Media.Source = new Uri(fd.FullName, UriKind.Absolute);
            }
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string path = ((Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
                if(!string.IsNullOrEmpty(path))
                {
                    _act.Media.Source = new Uri(path, UriKind.Absolute);
                }
            }
        }
    }
}
