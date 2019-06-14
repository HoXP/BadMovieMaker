using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace BadMovieMaker.Components
{
    public class ComponentsTrunk : UserControl
    {//组件主干类，继承该类的对象可以挂组件
        internal ObservableCollection<UCComponent> Components { get; set; } //所挂靠的组件

        public ComponentsTrunk()
        {
            Components = new ObservableCollection<UCComponent>();
        }

        public void AddComponent(UCComponent ucc)
        {//增
            Components.Add(ucc);
        }
        public void AddComponents(params UCComponent[] ucc)
        {//增
            if(ucc==null || ucc.Length <= 0)
            {
                return;
            }
            for (int i = 0; i < ucc.Length; i++)
            {
                Components.Add(ucc[i]);
            }
        }

        public void RemoveComponent(UCComponent ucc)
        {//删
            Components.Remove(ucc);
        }

        public void ClearComponent()
        {//清除
            if (Components != null && Components.Count > 0)
            {
                Components.Clear();
            }
        }
    }
}