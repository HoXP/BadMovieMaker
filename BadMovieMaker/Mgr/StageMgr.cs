using BadMovieMaker.Common;
using BadMovieMaker.Controls;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace BadMovieMaker.Mgr
{
    class StageMgr : SingletonBase<StageMgr>
    {
        internal const string StrStageSize = "StageSize";
        internal const string StrOCActor = "OCActor";

        private static ulong id = 0; //每次添加Actor就自增，标记唯一的Actor.Id

        private Vector V1280_720 = new Vector(1280, 720);
        private Vector _stageSize;
        internal Vector StageSize
        {
            get
            {
                return _stageSize;
            }
            set
            {
                if(_stageSize != value)
                {
                    _stageSize = value;
                    NotifyPropertyChanged(StrStageSize);
                }
            }
        }

        internal ObservableCollection<Actor> OCActor { get; private set; }

        public StageMgr()
        {
            Reset();
        }

        internal void Reset()
        {
            StageSize = V1280_720;
            RecordDuration = 0;
            if (OCActor == null)
            {
                OCActor = new ObservableCollection<Actor>();
            }
            else
            {
                OCActor.Clear();
            }
        }

        internal ulong CreateActor(EActor eActor)
        {
            Actor act = null;
            switch (eActor)
            {
                case EActor.Rect:
                    act = new RectActor(++id);
                    break;
                case EActor.Ellipse:
                    act = new EllipseActor(++id);
                    break;
                case EActor.Image:
                    act = new ImageActor(++id);
                    break;
                case EActor.Media:
                    act = new MediaActor(++id);
                    break;
                case EActor.Font:
                    act = new FontActor(++id);
                    break;
                default:
                    break;
            }
            OCActor.Add(act);
            NotifyPropertyChanged(StrOCActor);
            return id;
        }

        internal void Rename(ulong id, string name)
        {
            for (int i = 0; i < OCActor.Count; i++)
            {
                if (OCActor[i].Id == id)
                {
                    OCActor[i].Name = name;
                }
            }
        }

        internal void Delete(ulong id)
        {
            for (int i = OCActor.Count - 1; i >= 0; i--)
            {
                if(OCActor[i].Id == id)
                {
                    OCActor.RemoveAt(i);
                }
            }
        }

        internal long ActorCount()
        {
            return OCActor.Count;
        }

        internal void Select(ulong id, bool isSelected)
        {
            for (int i = 0; i < OCActor.Count; i++)
            {
                OCActor[i].IsSelected = false;
                if (OCActor[i].Id == id)
                {
                    OCActor[i].IsSelected = isSelected;
                }
            }
        }
        internal void DeselectAll()
        {
            for (int i = 0; i < OCActor.Count; i++)
            {
                Select(OCActor[i].Id, false);
            }
        }

        internal void Swap(int idx1, int idx2)
        {
            if(idx1 == idx2)
            {
                return;
            }
            Actor tmp = null;
            tmp = OCActor[idx1];
            OCActor[idx1] = OCActor[idx2];
            OCActor[idx2] = tmp;
            Console.WriteLine(string.Format("Swap {0} {1}", idx1, idx2));
        }

        #region Mouse
        public delegate void MouseDownDelegate(object sender, MouseButtonEventArgs e);
        public event MouseDownDelegate MouseDownEvent;
        public void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (MouseDownEvent != null)
            {
                MouseDownEvent.Invoke(sender, e);
            }
        }
        public delegate void MouseMoveDelegate(object sender, MouseEventArgs e);
        public event MouseMoveDelegate MouseMoveEvent;
        public void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (MouseMoveEvent != null)
            {
                MouseMoveEvent.Invoke(sender, e);
            }
        }
        public delegate void MouseUpDelegate(object sender, MouseButtonEventArgs e);
        public event MouseUpDelegate MouseUpEvent;
        public void OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (MouseUpEvent != null)
            {
                MouseUpEvent.Invoke(sender, e);
            }
        }
        #endregion

        #region MaximizeStage
        internal void MaximizeStage()
        {//最大化接口
            MaxStageAction?.Invoke();
        }
        /// <summary>
        /// 通知最大化
        /// </summary>
        internal Action MaxStageAction;
        /// <summary>
        /// 最大化舞台后的回调
        /// </summary>
        internal Action<Point, Vector, double> MaxStageCallback;
        #endregion

        internal void PlayAnim()
        {
            if(OCActor == null || OCActor.Count <= 0)
            {
                return;
            }
            for (int i = 0; i < OCActor.Count; i++)
            {
                OCActor[i].PlayAnim();
            }
        }
        internal void StopAnim()
        {
            if (OCActor == null || OCActor.Count <= 0)
            {
                return;
            }
            for (int i = 0; i < OCActor.Count; i++)
            {
                OCActor[i].StopAnim();
            }
        }

        internal void UpdateMaxRecordDuration(double val)
        {//获取val和录屏时长的最大值
            RecordDuration = Math.Max(RecordDuration, val);
        }
        internal double RecordDuration { get; set; }    //录屏时长/毫秒
    }

    public enum EActor
    {
        Rect,
        Ellipse,
        Image,
        Media,
        Font
    }
}