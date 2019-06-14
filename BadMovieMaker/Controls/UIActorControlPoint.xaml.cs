using BadMovieMaker.Common;
using BadMovieMaker.Mgr;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace BadMovieMaker.Controls
{
    public partial class UIActorControlPoint : UserControl
    {
        private Canvas _cvs = null;

        public BoolVisibilityConverter cvtBoolVisibility;

        public Actor ActorHolder { get; set; }

        private UIActorControlPoint() { }
        public UIActorControlPoint(Actor ma)
        {
            InitializeComponent();
            DataContext = this;
            Content = ma;
            ActorHolder = ma;
            //StageMgr.Instance.MouseMoveEvent += elpPivot_MouseMove;
            //StageMgr.Instance.MouseUpEvent += elpPivot_MouseUp;
            #region T
            StageMgr.Instance.MouseMoveEvent += cpMove_MouseMove;
            StageMgr.Instance.MouseUpEvent += cpMove_MouseUp;
            #endregion
            #region R
            StageMgr.Instance.MouseMoveEvent += cpRotate_MouseMove;
            StageMgr.Instance.MouseUpEvent += cpRotate_MouseUp;
            #endregion
            #region S
            StageMgr.Instance.MouseMoveEvent += cpScale_MouseMove;
            StageMgr.Instance.MouseUpEvent += cpScale_MouseUp;
            #endregion
            //#region Skew
            //StageMgr.Instance.MouseMoveEvent += cpSkew_MouseMove;
            //StageMgr.Instance.MouseUpEvent += cpSkew_MouseUp;
            //#endregion
        }

        ~UIActorControlPoint()
        {
            //StageMgr.Instance.MouseMoveEvent -= elpPivot_MouseMove;
            //StageMgr.Instance.MouseUpEvent -= elpPivot_MouseUp;
            #region T
            StageMgr.Instance.MouseMoveEvent -= cpMove_MouseMove;
            StageMgr.Instance.MouseUpEvent -= cpMove_MouseUp;
            #endregion
            #region R
            StageMgr.Instance.MouseMoveEvent -= cpRotate_MouseMove;
            StageMgr.Instance.MouseUpEvent -= cpRotate_MouseUp;
            #endregion
            #region S
            StageMgr.Instance.MouseMoveEvent -= cpScale_MouseMove;
            StageMgr.Instance.MouseUpEvent -= cpScale_MouseUp;
            #endregion
            //#region Skew
            //StageMgr.Instance.MouseMoveEvent -= cpSkew_MouseMove;
            //StageMgr.Instance.MouseUpEvent -= cpSkew_MouseUp;
            //#endregion
        }

        private void uc_Loaded(object sender, RoutedEventArgs e)
        {
            _cvs = ControlUtils.FindVisualParent<Canvas>(this);
            UpdateTTPivot(new Point(0, 0));
        }

        public new object Content
        {//用于放置所创建的控件
            get
            {
                return ccContent.Content;
            }
            set
            {
                ccContent.Content = value;
            }
        }

        #region Pivot
        private void UpdateTTPivot(Point pntO)
        {
            if(ActualWidth == 0 || ActualHeight == 0)
            {
                return;
            }
            ttPivot.X = pntO.X;
            ttPivot.Y = pntO.Y;
            RenderTransformOrigin = new Point(ttPivot.X / ActualWidth + 0.5, ttPivot.Y / ActualHeight + 0.5);   //轴点相对坐标转换为比例
        }
        private void elpPivot_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Point pntStartPos = e.GetPosition(this);
                _vStartOffsetPivot = new Point(ttPivot.X, ttPivot.Y) - pntStartPos;
                _isInitPivotT = true;
            }
        }
        private void elpPivot_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isInitPivotT && e.LeftButton == MouseButtonState.Pressed)
            {
                Point pntEndPos = e.GetPosition(this);
                Point pntO = pntEndPos + _vStartOffsetPivot;
                UpdateTTPivot(pntO);
            }
        }
        private void elpPivot_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Released)
            {
                _isInitPivotT = false;
            }
        }
        bool _isInitPivotT = false;
        private Vector _vStartOffsetPivot;
        #endregion

        #region R
        private void cpRotate_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed)
            {
                _isInitR = true;
                Point pntStartPos = e.GetPosition(this);
                Point pntACP = ControlUtils.GetPivotPos(this);
                _vStart = pntStartPos - pntACP;
            }
        }
        private void cpRotate_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isInitR && e.LeftButton == MouseButtonState.Pressed)
            {
                Point pntEndPos = e.GetPosition(this);
                Point pntACP = ControlUtils.GetPivotPos(this);
                Vector vEnd = pntEndPos - pntACP;
                double aglDelta = Vector.AngleBetween(vEnd, _vStart);
                aglDelta = -aglDelta;
                rtACP.Angle += aglDelta;
            }
        }
        private void cpRotate_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Released)
            {
                _isInitR = false;
            }
        }
        private bool _isInitR = false;
        private Vector _vStart;
        #endregion

        #region T
        private void cpMove_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed)
            {
                Point pntStartPos = e.GetPosition(_cvs);
                _vStartOffset = new Point(ttACP.X, ttACP.Y) - pntStartPos;
                _isInitT = true;
            }
        }
        private void cpMove_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isInitT && e.LeftButton == MouseButtonState.Pressed)
            {
                Point pntEndPos = e.GetPosition(_cvs);
                Point pntO = pntEndPos + _vStartOffset;
                ttACP.X = pntO.X;
                ttACP.Y = pntO.Y;
            }
        }
        private void cpMove_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Released)
            {
                _isInitT = false;
            }
        }
        bool _isInitT = false;
        private Vector _vStartOffset;
        #endregion

        #region S
        private void cpScaleLT_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SMouseDown(e, HorizontalAlignment.Left, VerticalAlignment.Top);
        }
        private void cpScaleT_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SMouseDown(e, HorizontalAlignment.Center, VerticalAlignment.Top);
        }
        private void cpScaleRT_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SMouseDown(e, HorizontalAlignment.Right, VerticalAlignment.Top);
        }
        private void cpScaleL_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SMouseDown(e, HorizontalAlignment.Left, VerticalAlignment.Center);
        }
        private void cpScaleR_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SMouseDown(e, HorizontalAlignment.Right, VerticalAlignment.Center);
        }
        private void cpScaleLB_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SMouseDown(e, HorizontalAlignment.Left, VerticalAlignment.Bottom);
        }
        private void cpScaleB_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SMouseDown(e, HorizontalAlignment.Center, VerticalAlignment.Bottom);
        }
        private void cpScaleRB_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SMouseDown(e, HorizontalAlignment.Right, VerticalAlignment.Bottom);
        }
        private void SMouseDown(MouseButtonEventArgs e, HorizontalAlignment ha, VerticalAlignment va)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                _ha = ha;
                _va = va;
                _pntStart = e.GetPosition(_cvs);
                _sScaleX = stACP.ScaleX;
                _sScaleY = stACP.ScaleY;
                _isInitS = true;
                _rtBak = rtACP.Clone();
                _rtBak.Angle = -rtACP.Angle;
            }
        }
        private void cpScale_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isInitS && e.LeftButton == MouseButtonState.Pressed)
            {
                Point pntEnd = e.GetPosition(_cvs);
                Vector v = pntEnd - _pntStart;
                v = _rtBak.Value.Transform(v);
                double timesX = 0;
                if (_ha == HorizontalAlignment.Left)
                {
                    timesX = -v.X / ActualWidth * 2;
                }
                else if(_ha == HorizontalAlignment.Right)
                {
                    timesX = v.X / ActualWidth * 2;
                }
                stACP.ScaleX = _sScaleX + timesX;
                double timesY = 0;
                if (_va == VerticalAlignment.Top)
                {
                    timesY = -v.Y / ActualHeight * 2;
                }
                else if(_va == VerticalAlignment.Bottom)
                {
                     timesY = v.Y / ActualHeight * 2;
                }
                stACP.ScaleY = _sScaleY + timesY;
                ScaleCP();
            }
        }
        private void cpScale_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Released)
            {
                _ha = HorizontalAlignment.Center;
                _va = VerticalAlignment.Center;
                _rtBak = null;
                _isInitS = false;
            }
        }
        internal void ScaleCP()
        {
            double min = 0.0001;
            double x = stACP.ScaleX;
            double y = stACP.ScaleY;
            if (Math.Abs(x) <= min || Math.Abs(y) <= min)
            {
                return;
            }
            x = 1 / x;
            y = 1 / y;
            //Rotate
            stRotate.ScaleX = x;
            stRotate.ScaleY = y;
            stLine.ScaleX = x;
            //Scale
            stScaleLT.ScaleX = x;
            stScaleLT.ScaleY = y;
            stScaleT.ScaleX = x;
            stScaleT.ScaleY = y;
            stScaleRT.ScaleX = x;
            stScaleRT.ScaleY = y;
            stScaleL.ScaleX = x;
            stScaleL.ScaleY = y;
            stScaleR.ScaleX = x;
            stScaleR.ScaleY = y;
            stScaleLB.ScaleX = x;
            stScaleLB.ScaleY = y;
            stScaleB.ScaleX = x;
            stScaleB.ScaleY = y;
            stScaleRB.ScaleX = x;
            stScaleRB.ScaleY = y;
            //Skew
            //stSkewT.ScaleY = y;
            //stSkewL.ScaleX = x;
            //stSkewR.ScaleX = x;
            //stSkewB.ScaleY = y;
            //Pivot
            stPivot.ScaleX = x;
            stPivot.ScaleY = y;
        }
        private HorizontalAlignment _ha = HorizontalAlignment.Center;
        private VerticalAlignment _va = VerticalAlignment.Center;
        private Point _pntStart;
        private double _sScaleX;
        private double _sScaleY;
        private bool _isInitS = false;
        private RotateTransform _rtBak;
        #endregion

        //#region Skew
        //private void cpSkewT_MouseDown(object sender, MouseButtonEventArgs e)
        //{
        //    SkMouseDown(e, HorizontalAlignment.Center, VerticalAlignment.Top);
        //}
        //private void cpSkewL_MouseDown(object sender, MouseButtonEventArgs e)
        //{
        //    SkMouseDown(e, HorizontalAlignment.Left, VerticalAlignment.Center);
        //}
        //private void cpSkewR_MouseDown(object sender, MouseButtonEventArgs e)
        //{
        //    SkMouseDown(e, HorizontalAlignment.Right, VerticalAlignment.Center);
        //}
        //private void cpSkewB_MouseDown(object sender, MouseButtonEventArgs e)
        //{
        //    SkMouseDown(e, HorizontalAlignment.Center, VerticalAlignment.Bottom);
        //}
        //private void cpSkew_MouseMove(object sender, MouseEventArgs e)
        //{
        //    if (_isInitSk && e.LeftButton == MouseButtonState.Pressed)
        //    {
        //        Point pntEnd = e.GetPosition(_cvs);
        //        Vector vOE = pntEnd - _pntOriginSk;
        //        vOE = _rtBakSkew.Value.Transform(vOE);
        //        double angle = Vector.AngleBetween(vOE, _vOS);
        //        if (_haSk == HorizontalAlignment.Left)
        //        {
        //            skACP.AngleY = _skAngleY + angle;
        //        }
        //        else if (_haSk == HorizontalAlignment.Right)
        //        {
        //            skACP.AngleY = _skAngleY - angle;
        //        }
        //        if (_vaSk == VerticalAlignment.Top)
        //        {
        //            skACP.AngleX = _skAngleX + angle;
        //        }
        //        else if (_vaSk == VerticalAlignment.Bottom)
        //        {
        //            skACP.AngleX = _skAngleX - angle;
        //        }
        //    }
        //}
        //private void cpSkew_MouseUp(object sender, MouseButtonEventArgs e)
        //{
        //    if (e.LeftButton == MouseButtonState.Released)
        //    {
        //        _haSk = HorizontalAlignment.Center;
        //        _vaSk = VerticalAlignment.Center;
        //        _isInitSk = false;
        //        _rtBakSkew = null;
        //    }
        //}
        //private void SkMouseDown(MouseButtonEventArgs e, HorizontalAlignment ha, VerticalAlignment va)
        //{
        //    if (e.LeftButton == MouseButtonState.Pressed)
        //    {
        //        _haSk = ha;
        //        _vaSk = va;
        //        Point pntStartSk = e.GetPosition(_cvs);
        //        _pntOriginSk = TranslatePoint(new Point(0,0), _cvs);
        //        _vOS = pntStartSk - _pntOriginSk;
        //        _skAngleX = skACP.AngleX;
        //        _skAngleY = skACP.AngleY;
        //        _isInitSk = true;
        //        _rtBakSkew = rtACP.Clone();
        //        _rtBakSkew.Angle = -rtACP.Angle;
        //        _vOS = _rtBakSkew.Value.Transform(_vOS);
        //        Console.WriteLine(string.Format("### SkMouseDown CenterX={0};CenterY={1}", skACP.CenterX, skACP.CenterY));
        //    }
        //}
        //bool _isInitSk = false;
        //private HorizontalAlignment _haSk = HorizontalAlignment.Center;
        //private VerticalAlignment _vaSk = VerticalAlignment.Center;
        //private Point _pntOriginSk; //轴点
        //private Vector _vOS;
        //private double _skAngleX;
        //private double _skAngleY;
        //private RotateTransform _rtBakSkew;
        //#endregion

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            SelectThis();
            Console.WriteLine(string.Format("### ccContent_PreviewMouseLeftButtonDown {0}", sender));
            e.Handled = true;
        }
        internal void SelectThis()
        {
            if (ActorHolder == null)
            {
                return;
            }
            StageMgr.Instance.Select(ActorHolder.Id, true);
        }

        internal void GetTransform(out TranslateTransform tt, out RotateTransform rt, out ScaleTransform st)
        {//获取Transform
            tt = ttACP;
            rt = rtACP;
            st = stACP;
        }
    }
}