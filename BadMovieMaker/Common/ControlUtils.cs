using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace BadMovieMaker.Common
{
    static class ControlUtils
    {
        public static List<T> GetChildObjects<T>(DependencyObject obj, string name) where T : FrameworkElement
        {
            DependencyObject child = null;
            List<T> childList = new List<T>();
            for (int i = 0; i <= VisualTreeHelper.GetChildrenCount(obj) - 1; i++)
            {
                child = VisualTreeHelper.GetChild(obj, i);
                if (child is T && (((T)child).GetType().ToString() == name | string.IsNullOrEmpty(name)))
                {
                    childList.Add((T)child);
                }
                childList.AddRange(GetChildObjects<T>(child, name));
            }
            return childList;
        }
        public static T GetChildObject<T>(DependencyObject obj, string name) where T : FrameworkElement
        {
            DependencyObject child = null;
            T grandChild = null;
            for (int i = 0; i <= VisualTreeHelper.GetChildrenCount(obj) - 1; i++)
            {
                child = VisualTreeHelper.GetChild(obj, i);
                if (child is T && (((T)child).Name == name | string.IsNullOrEmpty(name)))
                {
                    return (T)child;
                }
                else
                {
                    grandChild = GetChildObject<T>(child, name);
                    if (grandChild != null)
                        return grandChild;
                }
            }
            return null;
        }
        public static T FindVisualParent<T>(DependencyObject obj) where T : class
        {//根据子元素查找父元素
            while (obj != null)
            {
                if (obj is T)
                {
                    return obj as T;
                }
                obj = VisualTreeHelper.GetParent(obj);
            }
            return null;
        }

        public static Point GetPivotPos(FrameworkElement fe)
        {//获取该控件轴点位置——相对于该控件
            Point pos = new Point();
            pos.X = fe.RenderTransformOrigin.X * fe.ActualWidth;
            pos.Y = fe.RenderTransformOrigin.Y * fe.ActualHeight;
            return pos;
        }
    }
}