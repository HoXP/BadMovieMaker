using System.Windows.Controls;
using System;
using System.Windows;
using Microsoft.WindowsAPICodePack.Dialogs;
using BadMovieMaker.Mgr;
using BadMovieMaker.View.Windows;
using Microsoft.Win32;

public enum WindowType
{
    NewProject = 1,
    Export = 2,
}

namespace BadMovieMaker.View
{
    /// <summary>
    /// MenuView.xaml 的交互逻辑
    /// </summary>
    public partial class MenuView : UserControl
    {
        public MenuView()
        {
            InitializeComponent();
        }

        private void MenuItem_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MenuItem item = e.OriginalSource as MenuItem;
            switch ((string)item.Tag)
            {
                case "Open":

                    break;
                case "New":
                    MenuWindow newProjectWindow = new MenuWindow(WindowType.NewProject);
                    newProjectWindow.Title = "新建工程";
                    newProjectWindow.ShowDialog();
                    newProjectWindow.Owner = Application.Current.MainWindow;
                    break;
                case "Save":

                    break;
                case "Quit":
                    Application.Current.MainWindow.Close();
                    break;
                case "Export":
                    string fullFilePath = "";
                    string fileName = "";
                    string filePath = "";
                    SaveFileDialog dialog = new SaveFileDialog();
                    dialog.RestoreDirectory = true;
                    dialog.Filter = "mp4(*.mp4)|*.mp4";
                    dialog.FileName = "fuck";
                    if(dialog.ShowDialog() == true)
                    {
                        fullFilePath = dialog.FileName.ToString();
                        fileName = fullFilePath.Substring(fullFilePath.LastIndexOf("\\") + 1);
                        filePath = fullFilePath.Substring(0,fullFilePath.LastIndexOf("\\"));
                        MaxSatge(filePath, fileName);
                    }
                    break;
            }
        }
        //最大化窗口
        private void MaxSatge(string filePath, string fileName)
        {
            //Console.WriteLine("FuckPath：" + filePath + "__FuckName：" + fileName);
            StageMgr.Instance.MaxStageCallback += (p, s, d) =>
            {
                //Timer timer = new Timer(2000);
                ScreenCaptureMgr.Instance.StartRecord(p, s, d / 1000, filePath, fileName);
            };
            StageMgr.Instance.MaximizeStage();
        }
    }
}
