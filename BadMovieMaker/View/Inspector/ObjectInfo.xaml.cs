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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BadMovieMaker.View.Inspector
{
    /// <summary>
    /// ObjectInfo.xaml 的交互逻辑
    /// </summary>
    public partial class ObjectInfo : UserControl
    {
        public ObjectInfo()
        {
            InitializeComponent();
        }
        #region IsActive
        public static readonly DependencyProperty IsActiveProperty = DependencyProperty.Register("IsActive", typeof(bool), typeof(ObjectInfo), new PropertyMetadata(true), IsActiveCallback);
        private static bool IsActiveCallback(object value)
        {
            Console.WriteLine("IsActiveCallback IsActive:" + value);
            return true;
        }
        public bool IsActive
        {
            get { return (bool)GetValue(IsActiveProperty); }
            set { SetValue(IsActiveProperty, value); }
        }
        void ChangeActive(bool Active)
        {
            IsActive = Active;
        }
        #endregion
        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            if(checkBox.IsChecked == true )
            {
                ChangeActive(true);
                Console.WriteLine("选中_____" + checkBox.IsChecked);
            }
            else
            {
                ChangeActive(false);
                Console.WriteLine("为选中_____" + checkBox.IsChecked);
            }
        }


        #region GOName
        public static readonly DependencyProperty GONameProperty = DependencyProperty.Register("GOName", typeof(string), typeof(ObjectInfo), new PropertyMetadata("GOName"), GONameCallback);
        private static bool GONameCallback(object value)
        {
            Console.WriteLine("GONameCallback GOName:" + value);
            return true;
        }
        public string GOName
        {
            get { return (string)GetValue(GONameProperty); }
            set { SetValue(GONameProperty, value); }
        }
        #endregion

        void ReName(string value)
        {
            GOName = value;
        }

        private void Enter_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                ReName(Name.Text);
            }
        }


    }
}
