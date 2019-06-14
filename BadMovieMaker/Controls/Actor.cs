using BadMovieMaker.Common;
using BadMovieMaker.Components;
using BadMovieMaker.Define;
using BadMovieMaker.Mgr;
using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace BadMovieMaker.Controls
{
    public class Actor : ComponentsTrunk
    {
        public ulong Id { get; private set; }  //角色ID

        private ObjectComponent _object;
        private SizeComponent _size;
        private TransformComponent _tran;
        private TransparencyComponent _transparency;

        internal Actor(ulong id) : base()
        {
            DataContext = this;
            IsHitTestVisible = false;   //使内容不接收输入事件
            Id = id;
            ACP = new UIActorControlPoint(this);
            Width = 100;
            Height = 100;

            _object = new ObjectComponent(this);
            _size = new SizeComponent(this);
            _tran = new TransformComponent(this);
            _transparency = new TransparencyComponent(this);
            AddComponents(_object, _size, _tran, _transparency);
        }
        ~Actor()
        {
            RemoveComponent(_object);
            RemoveComponent(_size);
            RemoveComponent(_tran);
            RemoveComponent(_transparency);
            _object = null;
            _size = null;
            _tran = null;
            _transparency = null;
        }

        #region UI
        public UIActorControlPoint ACP { get; set; }
        #endregion

        /// <summary>
        /// 角色名
        /// </summary>
        public new string Name
        {
            get { return (string)GetValue(NameProperty); }
            set { SetValue(NameProperty, value); }
        }
        public new static readonly DependencyProperty NameProperty = DependencyProperty.Register("Name", typeof(string), typeof(Actor));

        public bool IsSelected
        {//角色是否选中
            get { return (bool)GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value);
                if(value)
                {
                    InspectorMgr.Instance.UpdateCurObj(this);
                }
            }
        }

        public static readonly DependencyProperty IsSelectedProperty = DependencyProperty.Register("IsSelected", typeof(bool), typeof(Actor), new PropertyMetadata(false));

        public bool IsActive
        {//是否激活
            get { return (bool)GetValue(IsActiveProperty); }
            set { SetValue(IsActiveProperty, value); }
        }
        public static readonly DependencyProperty IsActiveProperty = DependencyProperty.Register("IsActive", typeof(bool), typeof(Actor), new PropertyMetadata(true));

        #region 动画
        internal void AddTranslateAnimations(Translatelist[] translateList)
        {
            if(translateList == null || translateList.Length <= 0)
            {
                return;
            }
            DoubleAnimationUsingKeyFrames dkX = new DoubleAnimationUsingKeyFrames();
            Storyboard.SetTargetName(dkX, "ttACP");
            Storyboard.SetTargetProperty(dkX, new PropertyPath(TranslateTransform.XProperty));
            _sbSRT.Children.Add(dkX);

            DoubleAnimationUsingKeyFrames dkY = new DoubleAnimationUsingKeyFrames();
            Storyboard.SetTargetName(dkY, "ttACP");
            Storyboard.SetTargetProperty(dkY, new PropertyPath(TranslateTransform.YProperty));
            _sbSRT.Children.Add(dkY);
            for (int i = 0; i < translateList.Length; i++)
            {
                LinearDoubleKeyFrame lpkX = new LinearDoubleKeyFrame(translateList[i].end_pos.X);
                lpkX.KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(translateList[i].time));
                dkX.KeyFrames.Add(lpkX);

                LinearDoubleKeyFrame lpkY = new LinearDoubleKeyFrame(translateList[i].end_pos.Y);
                lpkY.KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(translateList[i].time));
                dkY.KeyFrames.Add(lpkY);
            }
            StageMgr.Instance.UpdateMaxRecordDuration(translateList[translateList.Length - 1].time);
        }
        internal void AddRotationAnimations(Rotationlist[] rotationList)
        {
            if (rotationList == null || rotationList.Length <= 0)
            {
                return;
            }
            DoubleAnimationUsingKeyFrames dk = new DoubleAnimationUsingKeyFrames();
            Storyboard.SetTargetName(dk, "rtACP");
            Storyboard.SetTargetProperty(dk, new PropertyPath(RotateTransform.AngleProperty));
            _sbSRT.Children.Add(dk);
            for (int i = 0; i < rotationList.Length; i++)
            {
                LinearDoubleKeyFrame lpk = new LinearDoubleKeyFrame(rotationList[i].end_angle);
                lpk.KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(rotationList[i].time));
                dk.KeyFrames.Add(lpk);
            }
            StageMgr.Instance.UpdateMaxRecordDuration(rotationList[rotationList.Length - 1].time);
        }
        internal void AddScaleAnimations(Scalelist[] scaleList)
        {
            if (scaleList == null || scaleList.Length <= 0)
            {
                return;
            }
            DoubleAnimationUsingKeyFrames dkX = new DoubleAnimationUsingKeyFrames();
            Storyboard.SetTargetName(dkX, "stACP");
            Storyboard.SetTargetProperty(dkX, new PropertyPath(ScaleTransform.ScaleXProperty));
            _sbSRT.Children.Add(dkX);

            DoubleAnimationUsingKeyFrames dkY = new DoubleAnimationUsingKeyFrames();
            Storyboard.SetTargetName(dkY, "stACP");
            Storyboard.SetTargetProperty(dkY, new PropertyPath(ScaleTransform.ScaleYProperty));
            _sbSRT.Children.Add(dkY);
            for (int i = 0; i < scaleList.Length; i++)
            {
                LinearDoubleKeyFrame lpkX = new LinearDoubleKeyFrame(scaleList[i].end_scale.X);
                lpkX.KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(scaleList[i].time));
                dkX.KeyFrames.Add(lpkX);

                LinearDoubleKeyFrame lpkY = new LinearDoubleKeyFrame(scaleList[i].end_scale.Y);
                lpkY.KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(scaleList[i].time));
                dkY.KeyFrames.Add(lpkY);
            }
            StageMgr.Instance.UpdateMaxRecordDuration(scaleList[scaleList.Length - 1].time);
        }
        internal void AddOpacityAnimations(Opacitylist[] opacityList)
        {
            if (opacityList == null || opacityList.Length <= 0)
            {
                return;
            }
            DoubleAnimationUsingKeyFrames dk = new DoubleAnimationUsingKeyFrames();
            Storyboard.SetTargetProperty(dk, new PropertyPath("Opacity"));
            _sbOpacity.Children.Add(dk);
            for (int i = 0; i < opacityList.Length; i++)
            {
                LinearDoubleKeyFrame lpk = new LinearDoubleKeyFrame(opacityList[i].end_opacity);
                lpk.KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(opacityList[i].time));
                dk.KeyFrames.Add(lpk);
            }
            StageMgr.Instance.UpdateMaxRecordDuration(opacityList[opacityList.Length - 1].time);
        }
        internal void PlayAnim()
        {
            _sbSRT.Begin(ACP, true);
            _sbOpacity.Begin(this, true);
        }
        internal void StopAnim()
        {
            _sbSRT.Stop(ACP);
            _sbOpacity.Stop(this);
        }
        private Storyboard _sbSRT = new Storyboard();
        private Storyboard _sbOpacity = new Storyboard();
        #endregion
    }
}