using BadMovieMaker.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BadMovieMaker.View.Windows
{
    /// <summary>
    /// AnimationSelectWindow.xaml 的交互逻辑
    /// </summary>
    public partial class AnimationSelectWindow : Window
    {
        private string curSelectAnimType;
        public AnimationSelectWindow()
        {
            InitializeComponent();
        }

        private void Button_Click( object sender, RoutedEventArgs e )
        {
            Button btn = sender as Button;
            if(btn.Content.ToString() == "确定")
            {
                AnimationModel childModel = new AnimationModel();
                childModel.AnimationType = curSelectAnimType;
                TreeViewItem it = this.Tag as TreeViewItem;
                ObservableCollection<AnimationModel> animList =it.ItemsSource as ObservableCollection<AnimationModel>;
                foreach(AnimationModel model in animList)
                {
                    if(model.AnimationType == curSelectAnimType)
                    {
                        MessageBox.Show(string.Format("已经包含{0}的动画", curSelectAnimType), "错误");
                        return;
                    }
                }
                animList.Add(childModel);
                it.IsExpanded = animList.Count > 0;
            }
            else
            {
                this.Close();
            }
        }

        private void TreeView_SelectedItemChanged( object sender, EventArgs e )
        {
            if (btnComfirm == null) return;
            TreeViewItem item = treeView.SelectedItem as TreeViewItem;
            btnComfirm.IsEnabled = item.Tag == null;
            if (item.Tag == null)
            {
                curSelectAnimType = item.Header.ToString();
            }
        }
    }
}
