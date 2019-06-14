using System.Windows;
using System.Windows.Controls;
using BadMovieMaker.Mgr;
using BadMovieMaker.Model;
using System;
using BadMovieMaker.Utils;
using System.Windows.Shapes;
using System.Windows.Media;
using BadMovieMaker.Controls;

namespace BadMovieMaker.View
{
    /// <summary>
    /// TimelineView.xaml 的交互逻辑
    /// </summary>
    public partial class TimelineView : UserControl
    {
		private Line line;

        public TimelineView()
        {
			//DataContext = ModelsHolder;
            initData();

            InitializeComponent();
			//treeView.ItemsSource = ModelsHolder.timelineTreeModelList;

			line = new Line();
			line.Stroke = Brushes.Red;
			line.StrokeThickness = 2;
			line.Opacity = 0.5;
			line.X1 = 0;
			line.Y1 = 0;
			line.X2 = line.X1;
			line.Y2 = 1000;
			line.Visibility = Visibility.Hidden;
			canvasTimeline.Children.Add(line);

			itemsControl.ItemsSource = ModelsHolder.timelineModel.AnimationList;
		}

		private void Sv_timeRuler_ScrollChanged( object sender, ScrollChangedEventArgs e )
		{
			sv_canvas.ScrollToHorizontalOffset(sv_timeRuler.HorizontalOffset);
		}

		void initData()
        {
            for (ulong i = 0; i < 10; i++)
            {
                TimelineTreeItemModel model = new TimelineTreeItemModel(new RectActor(i));
                ModelsHolder.timelineTreeModelList.Add(model);
            }
        }

        private void Button_Click( object sender, RoutedEventArgs e )
        {
            Button btn = sender as Button;
            ContentPresenter cp = btn.TemplatedParent as ContentPresenter;
            TimelineTreeItemModel model = cp.Content as TimelineTreeItemModel;

            //TreeViewItem item = treeView.ItemContainerGenerator.ContainerFromItem(model) as TreeViewItem;
            //if (item != null)
            //{
            //    if (item.ItemsSource == null)
            //    {
            //        item.ItemsSource = model.AnimationList;
            //    }
            //    Window animWind = new BadMovieMaker.View.Windows.AnimationSelectWindow();
            //    animWind.Owner = Application.Current.MainWindow;
            //    animWind.Tag = item;
            //    animWind.ShowDialog();
           // }
        }

		private void Sv_timeRuler_MouseMove( object sender, System.Windows.Input.MouseEventArgs e )
		{
			int index = timelineRuler.GetScaleFromMousePos(e.GetPosition(timelineRuler));
			line.X1 = timelineRuler.GetScalePxPosFromeScaleIndex(index);
			line.X2 = line.X1;
			line.Visibility = Visibility.Visible;

			int millPerScaleStep = (int)timelineRuler.LargeScaleLabelGap / (int)timelineRuler.LargeScaleSteps;
			curTimeText.Text = AppHelper.MillionSecsToFormattedString(index * millPerScaleStep);
		}

		private void TimelineRulerControl_MouseDown( object sender, System.Windows.Input.MouseButtonEventArgs e )
		{
			int index = timelineRuler.GetScaleFromMousePos(e.GetPosition(timelineRuler));
			Canvas.SetLeft(selectedTimeLine, timelineRuler.GetScalePxPosFromeScaleIndex(index) - selectedTimeLine.Width * 0.5);
			
		}

		private void TimeRulerSB_Scroll( object sender, System.Windows.Controls.Primitives.ScrollEventArgs e )
		{
			sv_timeRuler.ScrollToHorizontalOffset(timeRulerSB.Value);
		}

		private void OnTimelineViewLoaded( object sender, EventArgs e )
		{
			//timeRulerSB.Maximum = sv_timeRuler.ExtentWidth - sv_timeRuler.ViewportWidth;
		}

		private void Sv_timeRuler_MouseLeave( object sender, System.Windows.Input.MouseEventArgs e )
		{
			line.Visibility = Visibility.Hidden;
		}

		private void UserControl_SizeChanged( object sender, SizeChangedEventArgs e )
		{
			timeRulerSB.Maximum = sv_timeRuler.ExtentWidth - sv_timeRuler.ViewportWidth + Math.Ceiling( e.NewSize.Width) - Math.Floor( e.PreviousSize.Width);
		}

		private void TimeRulerSB_ValueChanged( object sender, RoutedPropertyChangedEventArgs<double> e )
		{
			if (timeRulerSB.Maximum - e.NewValue <= 5)
			{
				timelineRulerBorder.Width += 3000;
				timeRulerSB.Maximum += 3000;
			}
		}
	}
}
