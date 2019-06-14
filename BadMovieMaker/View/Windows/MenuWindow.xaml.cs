using Microsoft.Win32;
using System.Windows.Forms;
using System;
using System.Collections.Generic;
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
using Microsoft.WindowsAPICodePack.Dialogs;
using System.ComponentModel;
using System.IO;
using System.Text.RegularExpressions;
using Path = System.IO.Path;
using BadMovieMaker.Mgr;

namespace BadMovieMaker.View
{
    /// <summary>
    /// NewProjectWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MenuWindow : Window,INotifyPropertyChanged
    {
        public MenuWindow(WindowType type)
        {
            InitializeComponent();
            ChangeWindowType(type);
            ProjectGrid.DataContext = this;
        }
        
        //窗口类型
        private WindowType windowType;

        void ChangeWindowType(WindowType type)
        {
            windowType = type;
            switch (windowType)
            {
                case WindowType.NewProject:
                    Name.Text = "工程名称";
                    Position.Text = "工程位置";
                    OkButton.Content = "创建";
                    break;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
        private string _Value2;
        public string TextProjectPath
        {
            get { return _Value2; }
            set
            {
                if(value != _Value2)
                {
                    _Value2 = value;
                    NotifyPropertyChanged("TextProjectPath");
                }
            }
        }

        //选择文件夹路径
        private void ChooseFile(object sender,RoutedEventArgs e)
        {
            CommonOpenFileDialog commmonDialog = new CommonOpenFileDialog();
            commmonDialog.IsFolderPicker = true;
            ///ShowDialog(window):指定父级窗口
            if (commmonDialog.ShowDialog(this) == CommonFileDialogResult.Ok)
            {
                TextProjectPath = commmonDialog.FileName;
            }
        }

        //创建新工程
        private void CreateNewProject()
        {
            if (TextProjectName != null && TextProjectPath != null)
            {
                string path = FilePath();
                Directory.CreateDirectory(path);
                File.Create(path + "/README" + ".txt");
                Directory.CreateDirectory(path + "/Asset");
                Directory.CreateDirectory(path + "/Asset" + "/res");
                Close();
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("工程名称或路径为空，请检查！！");
            }
        }

        //确定按钮
        private void ButtonClick_Ok(object sender,RoutedEventArgs e)
        {
            switch (windowType)
            {
                case WindowType.NewProject:
                    CreateNewProject();
                    break;
            }
        }
        private string FilePath()
        {
            Regex regex = new Regex(@"^([a-zA-Z]:\\)?[^\/\:\*\?\""\<\>\|\,]*$");
            Match match = regex.Match(TextProjectPath);
            if (!match.Success)
            {
                System.Windows.Forms.MessageBox.Show("非法的文件保存路径，请重新选择或输入！！");
            }
            string Name = TextProjectName.Text;
            string filepath = Path.Combine(TextProjectPath, Name);
            return filepath;
        }

    }
}
