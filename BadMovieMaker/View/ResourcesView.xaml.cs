using BadMovieMaker.Common;
using BadMovieMaker.Mgr;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace BadMovieMaker.View
{
    public partial class ResourcesView : UserControl
    {
        public BoolVisibilityConverter cvtBoolVisibility;

        public ResourcesView()
        {
            InitializeComponent();
            tvDirectory.ItemsSource = ResourcesMgr.Instance.FileInfos;
        }

        private void stcItem_MouseEnter(object sender, MouseEventArgs e)
        {
            imgThumbnail.Visibility = Visibility.Visible;
            FileData fd = (e.Source as FrameworkElement).DataContext as FileData;
            if(fd.EFileType == FileType.Pic)
            {
                if (string.IsNullOrEmpty(fd.Thumbnail))
                {
                    imgThumbnail.Source = null;
                }
                else
                {
                    imgThumbnail.Source = new BitmapImage(new Uri(fd.Thumbnail));
                }
            }
            else
            {
                imgThumbnail.Source = null;
            }
        }
        private void stcItem_MouseLeave(object sender, MouseEventArgs e)
        {
            imgThumbnail.Visibility = Visibility.Collapsed;
        }

        private void tvDirectory_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {

        }
        private void tvDirectory_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Point pos = e.GetPosition(tvDirectory);
                HitTestResult result = VisualTreeHelper.HitTest(tvDirectory, pos);
                if (result == null)
                {
                    return;
                }
                TextBlock tb = result.VisualHit as TextBlock;
                if (tb == null)
                {
                    return;
                }
                FileData fd = tb.DataContext as FileData;
                if (fd == null || fd.EFileType == FileType.Folder)
                {
                    return;
                }
                DataObject dataObj = new DataObject(fd);
                DragDrop.DoDragDrop(tvDirectory, dataObj, DragDropEffects.Move);
            }
        }

        private void mnuDelete_Click(object sender, RoutedEventArgs e)
        {
            FileData fd = tvDirectory.SelectedItem as FileData;
            if (fd == null)
            {
                Console.WriteLine(string.Format("### mnuDelete_Click 未选中任何物体"));
                return;
            }
            ResourcesMgr.Instance.DeleteFile(fd);
        }
        private void mnuRename_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}