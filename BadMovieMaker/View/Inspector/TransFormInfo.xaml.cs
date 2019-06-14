using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// TransFormInfo.xaml 的交互逻辑
    /// </summary>
    public partial class TransFormInfo : UserControl,INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private string positionX;
        private string positionY;
        private string rotationX;
        private string rotationY;
        private string scaleX;
        private string scaleY;
        protected void NotifyPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
        public string PositionX
        {
            get { return positionX;}
            set {if (positionX != value)
                {
                    positionX = value;
                    NotifyPropertyChanged("PositionX");
                }; }
        }

        public string PositionY
        {
            get { return positionY; }
            set {if (positionY != value)
                {
                    positionY = value;
                    NotifyPropertyChanged("PositionY");
                }; }
        }

        public string RotationX
        {
            get { return rotationX;}
            set {if (rotationX != value)
                {
                    rotationX = value;
                    NotifyPropertyChanged("RotationX");
                }; }
        }

        public string RotationY
        {
            get {return rotationY ;}
            set {if (rotationY != value)
                {
                    rotationY = value;
                    NotifyPropertyChanged("RotationY");
                }; }
        }

        public string ScaleX
        {
            get { return scaleX; }
            set { if (scaleX != value)
                {
                    scaleX = value;
                    NotifyPropertyChanged("ScaleX");
                }; }
        }

        public string ScaleY
        {
            get {return scaleY;}
            set {if (scaleY != value)
                {
                    scaleY = value;
                    NotifyPropertyChanged("ScaleY");
                }; }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("2333");
            RotationX = "10";
        }
    }


}
