using System.Windows.Controls;

namespace BadMovieMaker.Components
{
    public class UCComponent : UserControl
    {//组件基类
        internal long Id;   //组件ID

        internal ComponentsTrunk Trunk { get; set; }    //组件所依附的主干对象

        private UCComponent() { }
        public UCComponent(ComponentsTrunk truck)
        {
            Trunk = truck;
        }

        internal void Remove()
        {//删除该组件
            Trunk.RemoveComponent(this);
        }
    }
}