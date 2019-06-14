using BadMovieMaker.Common;
using BadMovieMaker.Controls;
using BadMovieMaker.Mgr;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace BadMovieMaker.View
{
    public partial class HierarchyView : UserControl
    {
        public HierarchyView()
        {
            InitializeComponent();
            lstHierarchy.ItemsSource = StageMgr.Instance.OCActor;
        }
        
        private void Rect_Click(object sender, RoutedEventArgs e)
        {//创建Rectangle
            CreateActor(EActor.Rect);
        }
        private void Ellipse_Click(object sender, RoutedEventArgs e)
        {
            CreateActor(EActor.Ellipse);
        }
        private void Image_Click(object sender, RoutedEventArgs e)
        {//创建Image
            CreateActor(EActor.Image);
        }
        private void Media_Click(object sender, RoutedEventArgs e)
        {
            CreateActor(EActor.Media);
        }
        private void Font_Click(object sender, RoutedEventArgs e)
        {//创建Font
            CreateActor(EActor.Font);
        }
        private ulong CreateActor(EActor eActor)
        {
            ulong id = StageMgr.Instance.CreateActor(eActor);
            lstHierarchy.SelectedIndex = StageMgr.Instance.OCActor.Count - 1;
            return id;
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            MenuItem mi = sender as MenuItem;
            ulong id = 0;
            ulong.TryParse(mi.Uid, out id);
            StageMgr.Instance.Delete(id);
        }

        private void lstItem_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            Console.WriteLine(sender.ToString());
        }

        private void lstHierarchy_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            for (int i = 0; i < e.AddedItems.Count; i++)
            {
                Actor act = e.AddedItems[i] as Actor;
                StageMgr.Instance.Select(act.Id, true);
            }
        }

        #region 重命名
        private void Rename_Click(object sender, RoutedEventArgs e)
        {
            MenuItem mi = sender as MenuItem;
            long id = 0;
            long.TryParse(mi.Uid, out id);
            //lstHierarchy.SelectedItem获取的是绑定的Actor数据
            ListBoxItem lbi = lstHierarchy.ItemContainerGenerator.ContainerFromIndex(lstHierarchy.SelectedIndex) as ListBoxItem;
            EditableTextBlock edtActor = ControlUtils.GetChildObject<EditableTextBlock>(lbi, "edtActor");
            edtActor.PrepareRename();
        }
        #endregion

        #region Drag Drop
        private void lstHierarchy_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Point pos = e.GetPosition(lstHierarchy);
                HitTestResult result = VisualTreeHelper.HitTest(lstHierarchy, pos);
                if (result == null)
                {
                    return;
                }
                ListBoxItem listBoxItem = ControlUtils.FindVisualParent<ListBoxItem>(result.VisualHit);
                if (listBoxItem == null || listBoxItem.Content != lstHierarchy.SelectedItem)
                {
                    return;
                }
                Actor act = listBoxItem.Content as Actor;
                if(act == null)
                {
                    return;
                }
                DataObject dataObj = new DataObject(act);
                DragDrop.DoDragDrop(lstHierarchy, dataObj, DragDropEffects.Move);
            }
        }

        private void lstHierarchy_Drop(object sender, DragEventArgs e)
        {
            Point pos = e.GetPosition(lstHierarchy);
            HitTestResult result = VisualTreeHelper.HitTest(lstHierarchy, pos);
            if (result == null)
            {
                return;
            }
            Actor source = lstHierarchy.SelectedItem as Actor;
            if (source == null)
            {
                return;
            }
            int sIdx = lstHierarchy.Items.IndexOf(source);
            ListBoxItem listBoxItem = ControlUtils.FindVisualParent<ListBoxItem>(result.VisualHit);
            Actor target = null;
            int tIdx = -1;
            if (listBoxItem == null)
            {
                StageMgr.Instance.OCActor.RemoveAt(sIdx);
                StageMgr.Instance.OCActor.Add(source);
            }
            else
            {
                target = listBoxItem.Content as Actor;
                if (target == null)
                {
                    StageMgr.Instance.OCActor.RemoveAt(sIdx);
                    StageMgr.Instance.OCActor.Add(source);
                    return;
                }
                if (ReferenceEquals(target, source))
                {
                    return;
                }
                tIdx = lstHierarchy.Items.IndexOf(target);
                StageMgr.Instance.Swap(sIdx, tIdx);
            }
        }
        #endregion
    }
}