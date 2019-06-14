using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using System;

namespace BadMovieMaker.Controls
{
    public partial class EditableTextBlock : UserControl
    {
        public EditableTextBlock()
        {
            InitializeComponent();
            txb.Visibility = Visibility.Collapsed;
            Console.WriteLine(string.Format("EditableTextBlock - [{0}],[{1}],[{2}]", Text, txt.Text, txb.Text));
        }
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(EditableTextBlock), new PropertyMetadata("",new PropertyChangedCallback(OnTextChanged)));

        private static void OnTextChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            Console.WriteLine("OnTextChanged: " + e.NewValue.ToString());
        }

        public string Text
        {
            get
            {
                return (string)GetValue(TextProperty);
            }
            set
            {
                SetValue(TextProperty, value);
                txt.Text = value;
            }
        }

        internal void PrepareRename()
        {
            Console.WriteLine(string.Format("PrepareRename - [{0}],[{1}],[{2}]", Text, txt.Text, txb.Text));
            txb.Visibility = Visibility.Visible;
            txb.Select(0, Text.Length);
        }
        internal void Rename(string name)
        {
            Text = name;
            txb.Visibility = Visibility.Collapsed;
        }

        private void txb_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Rename(txb.Text.Trim());
            }
        }

        private void txb_GotFocus(object sender, RoutedEventArgs e)
        {

        }
        private void txb_LostFocus(object sender, RoutedEventArgs e)
        {
            Rename(txb.Text.Trim());
        }

        private void txt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F2)
            {
                PrepareRename();
            }
        }
    }
}