using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;

namespace BadMovieMaker.BMMControls
{
    public class TimelineRulerControl : Control
    {
        public enum LargeScaleLabelStepsEnum { OneStep = 1,TwoStep = 2,FiveStep = 5}
        public enum ScaleStepLengthEnum { Px5 = 5,Px10 = 10,Px15=15,Px20=20}
        public enum LargeScaleLabelGapEnum { Gap100 = 100,Gap200 = 200,Gap500 = 500,Gap1000 = 1000,Gap2000 = 2000,Gap5000 = 5000}

        public TimelineRulerControl() { }

        #region DP

        [Category("外观")]
        public Brush SmallScaleBrush
        {
            get { return (Brush)GetValue(SmallScaleBrushProperty); }
            set { SetValue(SmallScaleBrushProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SmallScaleBrushProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SmallScaleBrushProperty =
    DependencyProperty.Register("SmallScaleBrushProperty", typeof(Brush), typeof(TimelineRulerControl), new FrameworkPropertyMetadata(Brushes.Black,FrameworkPropertyMetadataOptions.AffectsRender));


        [Category("外观")]
        public Brush LargeScaleBrush
        {
            get { return (Brush)GetValue(LargeScaleBrushProperty); }
            set { SetValue(LargeScaleBrushProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BigScaleBrushProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LargeScaleBrushProperty =
    DependencyProperty.Register("LargeScaleBrush", typeof(Brush), typeof(TimelineRulerControl), new FrameworkPropertyMetadata(Brushes.Black,FrameworkPropertyMetadataOptions.AffectsRender));



        [Category("外观")]
        public Size SmallScaleSize
        {
            get { return (Size)GetValue(SmallScaleSizeProperty); }
            set { SetValue(SmallScaleSizeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SmallScaleSize.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SmallScaleSizeProperty =
    DependencyProperty.Register("SmallScaleSize", typeof(Size), typeof(TimelineRulerControl),new FrameworkPropertyMetadata(new Size(1,5),FrameworkPropertyMetadataOptions.AffectsMeasure|FrameworkPropertyMetadataOptions.AffectsRender));


        [Category("外观")]
        public Size LargeScaleSize
        {
            get { return (Size)GetValue(LargeScaleSizeProperty); }
            set { SetValue(LargeScaleSizeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SmallScaleSize.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LargeScaleSizeProperty =
    DependencyProperty.Register("LargeScaleSize", typeof(Size), typeof(TimelineRulerControl),new FrameworkPropertyMetadata(new Size(2,10),FrameworkPropertyMetadataOptions.AffectsMeasure|FrameworkPropertyMetadataOptions.AffectsRender));



        [Category("外观")]
        public Brush ScaleLabelBrush
        {
            get { return (Brush)GetValue(ScaleLabelBrushProperty); }
            set { SetValue(ScaleLabelBrushProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ScaleLabelBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ScaleLabelBrushProperty =
    DependencyProperty.Register("ScaleLabelBrush", typeof(Brush), typeof(TimelineRulerControl), new PropertyMetadata(Brushes.Black));


    //    [Category("外观")]
    //    public int ScaleLabelSize
    //    {
    //        get { return (int)GetValue(ScaleLabelSizeProperty); }
    //        set { SetValue(ScaleLabelSizeProperty, value); }
    //    }

    //    // Using a DependencyProperty as the backing store for ScaleLabelSize.  This enables animation, styling, binding, etc...
    //    public static readonly DependencyProperty ScaleLabelSizeProperty =
    //DependencyProperty.Register("ScaleLabelSize", typeof(int), typeof(TimelineRulerControl), new PropertyMetadata(10));



        public LargeScaleLabelStepsEnum LargeScaleSteps
        {
            get { return (LargeScaleLabelStepsEnum)GetValue(LargeScaleStepProperty); }
            set { SetValue(LargeScaleStepProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LargeScaleStep.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LargeScaleStepProperty =
    DependencyProperty.Register("LargeScaleStep", typeof(LargeScaleLabelStepsEnum), typeof(TimelineRulerControl),  new FrameworkPropertyMetadata(LargeScaleLabelStepsEnum.FiveStep,FrameworkPropertyMetadataOptions.AffectsRender));



        public ScaleStepLengthEnum ScaleStepLength
        {
            get { return (ScaleStepLengthEnum)GetValue(ScaleStepLengthProperty); }
            set { SetValue(ScaleStepLengthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ScaleStepLength.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ScaleStepLengthProperty =
    DependencyProperty.Register("ScaleStepLength", typeof(ScaleStepLengthEnum), typeof(TimelineRulerControl),  new FrameworkPropertyMetadata(ScaleStepLengthEnum.Px5,FrameworkPropertyMetadataOptions.AffectsRender));


        public int ZeroScaleOffset
        {
            get { return (int)GetValue(ZeroScaleOffsetProperty); }
            set { SetValue(ZeroScaleOffsetProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ZeroScaleOffset.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ZeroScaleOffsetProperty =
    DependencyProperty.Register("ZeroScaleOffset", typeof(int), typeof(TimelineRulerControl), new FrameworkPropertyMetadata(0,FrameworkPropertyMetadataOptions.AffectsRender));



        public LargeScaleLabelGapEnum LargeScaleLabelGap
        {
            get { return (LargeScaleLabelGapEnum)GetValue(LargeScaleLabelGapProperty); }
            set { SetValue(LargeScaleLabelGapProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LargeScaleLabelGap.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LargeScaleLabelGapProperty =
    DependencyProperty.Register("LargeScaleLabelGap", typeof(LargeScaleLabelGapEnum), typeof(TimelineRulerControl), new FrameworkPropertyMetadata(LargeScaleLabelGapEnum.Gap100,FrameworkPropertyMetadataOptions.AffectsRender));



		#endregion

		#region 交互

		#endregion

		#region 公共接口
		#endregion

		#region overrides
		protected override Size ArrangeOverride( Size arrangeBounds )
        {
            renderSize = new Rect(arrangeBounds);
            return base.ArrangeOverride(arrangeBounds);
        }

        protected override Size MeasureOverride( Size constraint )
        {
            Size size = new Size();

            _textSize = GetTextSize();

            if (Double.IsPositiveInfinity(constraint.Width))
                size.Width = 0;
            else
                size.Width = constraint.Width;

            size.Height = LargeScaleSize.Height + _textSize.Height;

            return size;
        }

        protected override void OnRender( DrawingContext dc )
        {
            Pen smallScalePen = new Pen(SmallScaleBrush,SmallScaleSize.Width);
            Pen largeScalePen = new Pen(LargeScaleBrush,LargeScaleSize.Width);

            FormattedText fText = null;

            //画大刻度
            int largeScaleStepLen = (int)LargeScaleSteps * (int)ScaleStepLength;
            int largeScaleDrawnLen = ZeroScaleOffset;

            int count = 0;
            while(largeScaleDrawnLen < renderSize.Width)
            {
                Point startP = new Point(largeScaleDrawnLen, renderSize.Height);
                Point endP = new Point(largeScaleDrawnLen, renderSize.Height - LargeScaleSize.Height);
                dc.DrawLine(largeScalePen, startP, endP);

                //画小刻度
                for (int i = 1; i < (int)LargeScaleSteps; i++)
                {
                    int smallX = largeScaleDrawnLen + i * (int)ScaleStepLength;
                    startP = new Point(smallX, renderSize.Height);
                    endP = new Point(smallX, renderSize.Height - SmallScaleSize.Height);
                    dc.DrawLine(smallScalePen, startP, endP);
                }
                //=================================

                //画刻度标签文本
                fText = new FormattedText(Utils.AppHelper.MillionSecsToFormattedString(count * (int)LargeScaleLabelGap),CultureInfo.CurrentCulture,FlowDirection.LeftToRight, _typeface, FontSize,ScaleLabelBrush);
                dc.DrawText(fText, new Point(largeScaleDrawnLen,renderSize.Height - LargeScaleSize.Height - _textSize.Height));
                //=================================
                count++;
                largeScaleDrawnLen += largeScaleStepLen;
            }
            //==========================
            
           

        }
        #endregion

        private Size GetTextSize()
        {
            FontFamily fontFamily = (FontFamily)GetValue(TextElement.FontFamilyProperty);
            FontStyle fontStyle = (FontStyle)GetValue(TextElement.FontStyleProperty);
            FontWeight fontWeight = (FontWeight)GetValue(TextElement.FontWeightProperty);
            FontStretch fontStretch = (FontStretch)GetValue(TextElement.FontStretchProperty);
            double _fontSize = (double)GetValue(TextElement.FontSizeProperty);
            //int _fontSize = ScaleLabelSize;

            _typeface = new Typeface(fontFamily, fontStyle, fontWeight, fontStretch);

            FormattedText textLabel = new FormattedText("00:00:000", CultureInfo.CurrentUICulture, FlowDirection.LeftToRight, _typeface, _fontSize, ScaleLabelBrush);
            double width = textLabel.BuildGeometry(new Point()).Bounds.Width;

            //textLabel = new FormattedText(String.Format(TextFormat, Maximum), CultureInfo.CurrentUICulture, FlowDirection.LeftToRight, _typeface, _fontSize, ScaleLabelBrush);
            //double width2 = textLabel.BuildGeometry(new Point()).Bounds.Width;

            //width = ( width2 > width ) ? width2 : width;

            double height = textLabel.Height;

            return new Size(width, height);
        }

		public int GetScaleFromMousePos(Point mousePos)
		{
			double validX = mousePos.X - ZeroScaleOffset;
			int ScaleIndex = (int)Math.Round(validX / (int)ScaleStepLength,0);

			return ScaleIndex;
		}

		public double GetScalePxPosFromeScaleIndex(int scaleIndex)
		{
			return scaleIndex * (int)ScaleStepLength + ZeroScaleOffset;
		}

        private Rect renderSize;
        private Typeface _typeface;
        private Size _textSize;
    }
}
