using BadMovieMaker.Mgr;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Collections.Specialized;
using System.Collections;
using BadMovieMaker.Controls;
using BadMovieMaker.Components;
using System.ComponentModel;

namespace BadMovieMaker.View
{
    public partial class StageView : ComponentsTrunk
    {
        public StageView()
        {
            InitializeComponent();
            DataContext = this;
            StageMgr.Instance.MouseMoveEvent += scrStage_MouseMove;
            StageMgr.Instance.MouseUpEvent += scrStage_MouseUp;
            StageMgr.Instance.OCActor.CollectionChanged += OnOCActorChanged;
            StageMgr.Instance.PropertyChanged += Instance_PropertyChanged;
            StageMgr.Instance.MaxStageAction += FullScreen;
            grdStage.AddHandler(MouseLeftButtonDownEvent, new RoutedEventHandler(btn_Click));
            _stageSizeComponent = new StageSizeComponent(this);
            AddComponent(_stageSizeComponent);
        }
        ~StageView()
        {
            StageMgr.Instance.MouseMoveEvent -= scrStage_MouseMove;
            StageMgr.Instance.MouseUpEvent -= scrStage_MouseUp;
            StageMgr.Instance.OCActor.CollectionChanged -= OnOCActorChanged;
            StageMgr.Instance.PropertyChanged -= Instance_PropertyChanged;
            StageMgr.Instance.MaxStageAction -= FullScreen;
            RemoveComponent(_stageSizeComponent);
        }
        private StageSizeComponent _stageSizeComponent;

        private void Instance_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == StageMgr.StrStageSize)
            {
                ScreenSize = StageMgr.Instance.StageSize * 2;
            }
        }

        #region Size
        public Vector ScreenSize
        {
            get { return (Vector)GetValue(ScreenSizeProperty); }
            set { SetValue(ScreenSizeProperty, value); }
        }
        public static readonly DependencyProperty ScreenSizeProperty = DependencyProperty.Register("ScreenSize", typeof(Vector), typeof(StageView), new PropertyMetadata(StageMgr.Instance.StageSize * 2));

        private const double MinTimes = 0.2;
        private const double MaxTimes = 2;
        public double StageSizeTime
        {//舞台尺寸倍数
            get { return (double)GetValue(StageSizeTimeProperty); }
            set
            {
                if (value < MinTimes)
                {
                    value = MinTimes;
                }
                else if (value > MaxTimes)
                {
                    value = MaxTimes;
                }
                SetValue(StageSizeTimeProperty, value);
            }
        }
        public static readonly DependencyProperty StageSizeTimeProperty = DependencyProperty.Register("StageSizeTime", typeof(double), typeof(StageView), new PropertyMetadata(1.0));

        private void txbSizeTime_TextChanged(object sender, TextChangedEventArgs e)
        {
            string p = (sender as TextBox).Text.Trim('%');
            double n = 0;
            double.TryParse(p, out n);
            StageSizeTime = n / 100;
        }
        #endregion

        #region 中键拖拽
        private Point _startMousePos;   //开始拖动时鼠标位置
        private Point _scrDeltaOffset;
        private Point _scrLastOffset;
        public Point MousePos
        {
            get { return (Point)GetValue(MousePosProperty); }
            set { SetValue(MousePosProperty, value); }
        }
        public static readonly DependencyProperty MousePosProperty =
            DependencyProperty.Register("MousePos", typeof(Point), typeof(StageView), new PropertyMetadata(new Point()));
        #endregion

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            _scrDeltaOffset = new Point(scrStage.ScrollableWidth / 2, scrStage.ScrollableHeight / 2);
            scrStage.ScrollToHorizontalOffset(_scrDeltaOffset.X);
            scrStage.ScrollToVerticalOffset(_scrDeltaOffset.Y);
        }

        #region 创建Actor
        private void OnOCActorChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    IList news = e.NewItems;
                    if (news.Count > 0)
                    {//增
                        for (int i = 0; i < news.Count; i++)
                        {
                            Actor tmp = news[i] as Actor;
                            cvsWorld.Children.Add(tmp.ACP);
                        }
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    IList olds = e.OldItems;
                    if (olds.Count > 0)
                    {//删
                        for (int i = 0; i < olds.Count; i++)
                        {
                            Actor tmp = olds[i] as Actor;
                            cvsWorld.Children.Remove(tmp.ACP);
                        }
                    }
                    break;
                case NotifyCollectionChangedAction.Replace:
                    //替
                    Console.WriteLine("OnOCActorChanged Replace ");
                    break;
                case NotifyCollectionChangedAction.Move:
                    //移
                    Console.WriteLine("OnOCActorChanged Move ");
                    break;
                case NotifyCollectionChangedAction.Reset:
                    Console.WriteLine("OnOCActorChanged Reset ");
                    break;
            }
        }
        #endregion

        #region 舞台操作
        private void scrStage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.MiddleButton == MouseButtonState.Pressed)
            {
                Cursor = Cursors.SizeAll;
                _startMousePos = e.GetPosition(this);
            }
        }
        private void scrStage_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.MiddleButton == MouseButtonState.Pressed)
            {
                if (Mouse.Captured == null)
                {
                    Mouse.Capture(scrStage);
                }
                _scrDeltaOffset.X = _startMousePos.X - MousePos.X;
                _scrDeltaOffset.Y = _startMousePos.Y - MousePos.Y;
                scrStage.ScrollToHorizontalOffset(_scrLastOffset.X + _scrDeltaOffset.X);
                scrStage.ScrollToVerticalOffset(_scrLastOffset.Y + _scrDeltaOffset.Y);
            }
            else if(e.MiddleButton == MouseButtonState.Released)
            {
                Mouse.Capture(null);
                _scrLastOffset.X = scrStage.ContentHorizontalOffset;
                _scrLastOffset.Y = scrStage.ContentVerticalOffset;
            }
        }
        private void scrStage_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (e.MiddleButton == MouseButtonState.Released)
            {
                Cursor = Cursors.Arrow;
            }
        }
        private void grdStage_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            double factor = scaleFactor * (e.Delta > 0 ? 1 : -1);
            StageSizeTime += factor;
        }
        private double scaleFactor = 0.1;
        #endregion

        #region 点击背景
        private void btn_Click(object sender, RoutedEventArgs e)
        {
            FrameworkElement os = e.OriginalSource as FrameworkElement;
            FrameworkElement s = e.Source as FrameworkElement;
            if(s.GetType() == typeof(UIActorControlPoint))
            {
                UIActorControlPoint acp = e.Source as UIActorControlPoint;
                acp.SelectThis();
            }
            else
            {
                StageMgr.Instance.DeselectAll();
                InspectorMgr.Instance.UpdateCurObj(this);
            }
        }
        #endregion

        private void UserControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.H)
            {//按H可Reset舞台尺寸
                Console.WriteLine("UserControl_KeyDown Key.H");
                StageSizeTime = 1;
            }
        }

        #region Mouse
        private void UserControl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            StageMgr.Instance.OnMouseDown(sender, e);
        }
        private void UserControl_MouseMove(object sender, MouseEventArgs e)
        {
            MousePos = e.GetPosition(this);
            StageMgr.Instance.OnMouseMove(sender, e);
        }
        private void UserControl_MouseUp(object sender, MouseButtonEventArgs e)
        {
            StageMgr.Instance.OnMouseUp(sender, e);
        }
        #endregion

        private void FullScreen()
        {
            if (MainWindowMgr.Instance.IsMax)
            {
                MainWindowMgr.Instance.ResetStage();
                StageMgr.Instance.StopAnim();
            }
            else
            {
                Application.Current.MainWindow.WindowState = WindowState.Maximized;
                MainWindowMgr.Instance.MaximizeStage();
                StageSizeTime = 1;
                _isMax = true;
                scrStage.ScrollToHorizontalOffset(scrStage.ScrollableWidth / 3);
                scrStage.ScrollToVerticalOffset(scrStage.ScrollableHeight / 3);
            }
        }
        private void scrStage_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (_isMax)
            {
                _isMax = false;
                Point stageLT = cvsWorld.PointToScreen(PointZero);
                StageMgr.Instance.MaxStageCallback?.Invoke(stageLT, StageMgr.Instance.StageSize, StageMgr.Instance.RecordDuration);
                StageMgr.Instance.PlayAnim();
            }
        }
        private void btnFullScreen_Click(object sender, RoutedEventArgs e)
        {
            StageMgr.Instance.MaximizeStage();
        }
        private bool _isMax = false;
        private readonly Point PointZero = new Point(0d, 0d);
    }
}