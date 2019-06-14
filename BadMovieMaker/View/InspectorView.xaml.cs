using BadMovieMaker.Mgr;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace BadMovieMaker.View
{
    public partial class InspectorView : UserControl
    {
        public InspectorView()
        {
            InitializeComponent();
            InspectorMgr.Instance.PropertyChanged += InspectorMgr_PropertyChanged;
        }
        ~InspectorView()
        {
            InspectorMgr.Instance.PropertyChanged -= InspectorMgr_PropertyChanged;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("增加组件");
        }
        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("移除组件");
        }

        private void InspectorMgr_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {//CurObj属性更改
            if (e.PropertyName == "CurObj")
            {
                InspectorWrapPanel.Children.Clear();
                for (int i = 0; i < InspectorMgr.Instance.CurObj.Components.Count; i++)
                {
                    InspectorWrapPanel.Children.Add(InspectorMgr.Instance.CurObj.Components[i]);
                }
            }
        }
    }
}