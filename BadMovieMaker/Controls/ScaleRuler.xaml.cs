using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System;
using System.Windows.Input;

namespace BadMovieMaker.Controls
{
    /// <summary>
    /// ScaleRuler.xaml 的交互逻辑
    /// </summary>
    public partial class ScaleRuler : UserControl
    {
        #region 刻度数据配置
        private struct ScaleInfo
        {
            //public int zeroOffset;//零刻度偏移
            public int labelValue;
            public int labelStep; //数字标签刻度间隔
            public int scaleGap; //每两个刻度间的像素间隔
        }

        private ScaleInfo[] scaleInfos = new ScaleInfo[]
        {
             new ScaleInfo() { labelValue = 5000,labelStep = 5,scaleGap = 10 },
            new ScaleInfo() { labelValue = 5000,labelStep = 5,scaleGap = 12 },
            new ScaleInfo() { labelValue = 5000,labelStep = 5,scaleGap = 15 },
            new ScaleInfo() { labelValue = 5000,labelStep = 5,scaleGap = 19 },

             new ScaleInfo() { labelValue = 3000,labelStep = 5,scaleGap = 10 },
            new ScaleInfo() { labelValue = 3000,labelStep = 5,scaleGap = 12 },
            new ScaleInfo() { labelValue = 3000,labelStep = 5,scaleGap = 15 },
            new ScaleInfo() { labelValue = 3000,labelStep = 5,scaleGap = 19 },

            new ScaleInfo() { labelValue = 1000,labelStep = 5,scaleGap = 10 },
            new ScaleInfo() { labelValue = 1000,labelStep = 5,scaleGap = 12 },
            new ScaleInfo() { labelValue = 1000,labelStep = 5,scaleGap = 15 },
            new ScaleInfo() { labelValue = 1000,labelStep = 5,scaleGap = 19 },

             new ScaleInfo() { labelValue = 500,labelStep = 5,scaleGap = 10 },
            new ScaleInfo() { labelValue = 500,labelStep = 5,scaleGap = 12 },
            new ScaleInfo() { labelValue = 500,labelStep = 5,scaleGap = 15 },
            new ScaleInfo() { labelValue = 500,labelStep = 5,scaleGap = 19 },

            new ScaleInfo() { labelValue = 300,labelStep = 5,scaleGap = 10 },
            new ScaleInfo() { labelValue = 300,labelStep = 5,scaleGap = 12 },
            new ScaleInfo() { labelValue = 300,labelStep = 5,scaleGap = 15 },
            new ScaleInfo() { labelValue = 300,labelStep = 5,scaleGap = 19 },

            new ScaleInfo() { labelValue = 100,labelStep = 5,scaleGap = 10 },
            new ScaleInfo() { labelValue = 100,labelStep = 5,scaleGap = 12 },
            new ScaleInfo() { labelValue = 100,labelStep = 5,scaleGap = 15 },
            new ScaleInfo() { labelValue = 100,labelStep = 5,scaleGap = 19 },

            new ScaleInfo() { labelValue = 50,labelStep = 5,scaleGap = 10 },
            new ScaleInfo() { labelValue = 50,labelStep = 5,scaleGap = 12 },
            new ScaleInfo() { labelValue = 50,labelStep = 5,scaleGap = 15 },
            new ScaleInfo() { labelValue = 50,labelStep = 5,scaleGap = 19 },

            new ScaleInfo() { labelValue = 20,labelStep = 5,scaleGap = 10 },
            new ScaleInfo() { labelValue = 20,labelStep = 5,scaleGap = 12 },
            new ScaleInfo() { labelValue = 20,labelStep = 5,scaleGap = 15 },
            new ScaleInfo() { labelValue = 20,labelStep = 5,scaleGap = 19 },

            new ScaleInfo() { labelValue = 10,labelStep = 5,scaleGap = 10 },
            new ScaleInfo() { labelValue = 10,labelStep = 5,scaleGap = 12 },
            new ScaleInfo() { labelValue = 10,labelStep = 5,scaleGap = 15 },
            new ScaleInfo() { labelValue = 10,labelStep = 5,scaleGap = 19 },

            new ScaleInfo() { labelValue = 5,labelStep = 5,scaleGap = 10 },
            new ScaleInfo() { labelValue = 5,labelStep = 5,scaleGap = 12 },
            new ScaleInfo() { labelValue = 5,labelStep = 5,scaleGap = 15 },
            new ScaleInfo() { labelValue = 5,labelStep = 5,scaleGap = 19 },

            new ScaleInfo() { labelValue = 2,labelStep = 2,scaleGap = 15 },
            new ScaleInfo() { labelValue = 2,labelStep = 2,scaleGap = 20 },
            new ScaleInfo() { labelValue = 2,labelStep = 2,scaleGap = 30 },

            new ScaleInfo() { labelValue = 1,labelStep = 1,scaleGap = 30 },
            new ScaleInfo() { labelValue = 1,labelStep = 1,scaleGap = 40 },
            new ScaleInfo() { labelValue = 1,labelStep = 1,scaleGap = 60 },
        };
        #endregion

        #region 对外公开属性

        public static readonly DependencyProperty scaleSizeProperty = null;

        private static void scaleSizePropertyChangedCallback( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            ScaleRuler dp = d as ScaleRuler;
            dp.ScaleSize = (int)e.NewValue;
        }

        //private int scaleSize;

        public int ScaleSize
        {
            get
            {
                return (int)GetValue(scaleSizeProperty);
            }

            set
            {
                //int oldVal = (int)GetValue(scaleSizeProperty);
                //if (oldVal == value) return;
                if (value > scaleInfos.Length - 1)
                {
                    SetValue(scaleSizeProperty, scaleInfos.Length - 1);
                }
                else if (value < 0)
                {
                    SetValue(scaleSizeProperty, 0);
                }
                else
                {
                    SetValue(scaleSizeProperty, value);
                }
                DrawScale();
            }
        }
        #endregion

        //鼠标右键按下去的位置
        Point mouseRightClickPos;
        Point mouseLeftClickPos;

        TranslateTransform totalTranslate = new TranslateTransform();
        TranslateTransform tempTranslate = new TranslateTransform();

        Line clickLine;
        ScaleInfo curScaleInfo;

        double lastDragPos = double.MinValue;

        static ScaleRuler()
        {
            scaleSizeProperty = DependencyProperty.Register(
            "ScaleSize",
            typeof(int),
            typeof(ScaleRuler),
            new PropertyMetadata(new PropertyChangedCallback(scaleSizePropertyChangedCallback))
            );
        }

        public ScaleRuler()
        {
            InitializeComponent();
            DrawScale();
        }

        private void DrawScale()
        {
            if(canvas.Children.IndexOf(clickLine) != -1)
            {
                canvas.Children.RemoveRange(0, canvas.Children.Count - 1);
            }
            else
            {
                canvas.Children.Clear();
            }
            totalTranslate = new TranslateTransform();
            tempTranslate = new TranslateTransform();

            curScaleInfo = scaleInfos[ScaleSize];
            int zeroOffset = 5;//零刻度偏移
            int labelValue = curScaleInfo.labelValue;
            int labelStep = curScaleInfo.labelStep; //数字标签刻度间隔
            int scaleGap = curScaleInfo.scaleGap; //每两个刻度间的像素间隔

            for (int i = 0; i <= 1000; i++)
            {
                int factor = i + (int)(-tempTranslate.X / scaleGap);
                Line x_scale = new Line();
                x_scale.StrokeEndLineCap = PenLineCap.Square;
                x_scale.StrokeThickness = 0.5;
                x_scale.Stroke = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0));
                x_scale.X1 = zeroOffset + i * scaleGap;
                x_scale.X2 = x_scale.X1;
                x_scale.Y1 = factor % labelStep == 0 ? 15 : 25;
                x_scale.Y2 = 30;

                if (i % labelStep == 0)
                {
                    TextBlock scaleLabel = new TextBlock();
                    //scaleLabel.Width = 100;
                    scaleLabel.Text = ( labelValue * ( factor / labelStep ) ).ToString();
                    //scaleLabel.HorizontalAlignment = HorizontalAlignment.Center;
                    Canvas.SetLeft(scaleLabel, x_scale.X1);
                    this.canvas.Children.Insert(0, scaleLabel);
                }

                this.canvas.Children.Insert(0,x_scale);
            }
        }

        private void DrawClickedLine(double X)
        {
            if(clickLine == null)
            {
                clickLine = new Line();
                clickLine.StrokeThickness = 2;
                clickLine.Stroke = new SolidColorBrush(Color.FromArgb(255, 0, 255, 0));
                canvas.Children.Add(clickLine);
                clickLine.Y1 = 15;
                clickLine.Y2 = 30;
            }

            TranslateTransform tt = clickLine.RenderTransform as TranslateTransform;
            double scaleValue = X - (tt ==null ? 0 : tt.X);
            clickLine.X1 = GetScalePosX(scaleValue);
            clickLine.X2 = clickLine.X1;
            Console.WriteLine("###: " + GetScaleValue(clickLine.X1));
        }

        #region 鼠标/键盘事件
        protected override void OnMouseLeftButtonDown( MouseButtonEventArgs e )
        {
            mouseLeftClickPos = e.GetPosition(canvas);
        }

        protected override void OnMouseLeftButtonUp( MouseButtonEventArgs e )
        {
            Point pos = e.GetPosition(canvas);
            if(Math.Abs(mouseLeftClickPos.X - pos.X) < 5 && Math.Abs(mouseLeftClickPos.Y - pos.Y) < 5)
            {
                //左键单击合法
                DrawClickedLine(pos.X);
            }
        }

        protected override void OnMouseWheel( MouseWheelEventArgs e )
        {
            ScaleSize = ScaleSize + ( e.Delta > 0 ? 1 : -1 );
        }

        protected override void OnMouseRightButtonDown( MouseButtonEventArgs e )
        {
            mouseRightClickPos = e.GetPosition(canvas);
        }

        protected override void OnMouseRightButtonUp( MouseButtonEventArgs e )
        {
            Point endMovePosition = e.GetPosition(canvas);

            totalTranslate.X += ( endMovePosition.X - mouseRightClickPos.X );
            if(totalTranslate.X > 0)
            {
                totalTranslate.X = 0;
            }
            this.Cursor = Cursors.Arrow;
        }

        protected override void OnMouseMove( MouseEventArgs e )
        {
            if (e.RightButton == MouseButtonState.Pressed)
            {
                //滑动时间轴
                this.Cursor = Cursors.ScrollWE;
                Point currentMousePosition = e.GetPosition(canvas);//当前鼠标位置

                Point deltaPt = new Point(0, 0);
                deltaPt.X = ( currentMousePosition.X - mouseRightClickPos.X );

                tempTranslate.X = totalTranslate.X + deltaPt.X;
                if(tempTranslate.X > 0)
                {
                    tempTranslate.X = 0;
                }
                
                adjustGraph();
            }
            else if(e.LeftButton == MouseButtonState.Pressed)
            {
                //滑动当前轴

                double x = e.GetPosition(canvas).X;
                if (Math.Abs(lastDragPos - x) >= curScaleInfo.scaleGap)
                {
                    lastDragPos = x;
                    DrawClickedLine(x);
                }
            }
        }
        #endregion

        private void adjustGraph()
        {
            foreach (UIElement ue in canvas.Children)
            {
                ue.RenderTransform = tempTranslate;
            }
        }

        private double GetScaleValue(double scalePosX)
        {
            int a = (int)(scalePosX / curScaleInfo.scaleGap);
            double b = (double)a / curScaleInfo.labelStep;
            double c = b * curScaleInfo.labelValue;
            return c;
        }

        private double GetScalePosX(double posX)
        {
            for(int i = 0; i < canvas.Children.Count - 2; i++)
            {
                UIElement ue = canvas.Children[i];
                if(ue is Line)
                {
                    Line line = ue as Line;
                    if(Math.Abs(line.X1 - posX) <= curScaleInfo.scaleGap * 0.5)
                    {
                        return line.X1;
                    }
                }
            }
            return 0;
        }
    }
}
