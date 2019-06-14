using BadMovieMaker.Common;
using BadMovieMaker.Controls;
using BadMovieMaker.Define;
using BadMovieMaker.Mgr;
using System;
using System.Windows;

namespace BadMovieMaker
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = MainWindowMgr.Instance;
        }

        private void Window_Drop(object sender, DragEventArgs e)
        {
            string path = string.Empty;
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                path = ((string[])e.Data.GetData(DataFormats.FileDrop))[0];
                Rootobject ajr = JsonUtils.Deserialize(path);
                if (ajr == null || ajr.actors == null || ajr.actors.Length <= 0)
                {
                    Console.WriteLine(string.Format("### no actors in file {0}", path));
                    return;
                }
                StageMgr.Instance.Reset();
                StageMgr.Instance.StageSize = new Vector(ajr.stage_size.Width, ajr.stage_size.Height);
                for (int i = 0; i < ajr.actors.Length; i++)
                {
                    Define.Actor ai = ajr.actors[i];
                    Controls.Actor act = null;
                    if(ai.actor_type == "ImageActor")
                    {
                        act = new ImageActor((ulong)ai.id, string.Format("{0}{1}", PathDefine.PackBase, ai.url));
                    }
                    act.Name = ai.actor_name;
                    //init
                    act.ACP.ttACP.X = ai.init_data.pos.X;
                    act.ACP.ttACP.Y = ai.init_data.pos.Y;
                    act.ACP.rtACP.Angle = ai.init_data.angle;
                    act.ACP.stACP.ScaleX = ai.init_data.scale.X;
                    act.ACP.stACP.ScaleY = ai.init_data.scale.Y;
                    act.Opacity = ai.init_data.opacity;
                    act.Width = ai.init_data.size.Width;
                    act.Height = ai.init_data.size.Height;
                    //Anim
                    act.AddTranslateAnimations(ai.animations.translateList);
                    act.AddRotationAnimations(ai.animations.rotationList);
                    act.AddScaleAnimations(ai.animations.scaleList);
                    act.AddOpacityAnimations(ai.animations.opacityList);
                    StageMgr.Instance.OCActor.Add(act);
                }
            }
        }

		private void Window_Loaded( object sender, RoutedEventArgs e )
		{
			string path = @"F:\Projects\WPF\BadMovieMaker_coding\BadMovieMaker\BadMovieMaker\Data\test2.json";
			Rootobject ajr = JsonUtils.Deserialize(path);
			if (ajr == null || ajr.actors == null || ajr.actors.Length <= 0)
			{
				Console.WriteLine(string.Format("### no actors in file {0}", path));
				return;
			}
			StageMgr.Instance.Reset();
			StageMgr.Instance.StageSize = new Vector(ajr.stage_size.Width, ajr.stage_size.Height);
			for (int i = 0; i < ajr.actors.Length; i++)
			{
				Define.Actor ai = ajr.actors[i];
				Controls.Actor act = null;
				if (ai.actor_type == "ImageActor")
				{
					act = new ImageActor((ulong)ai.id, string.Format("{0}{1}", PathDefine.PackBase, ai.url));
				}
				act.Name = ai.actor_name;
				//init
				act.ACP.ttACP.X = ai.init_data.pos.X;
				act.ACP.ttACP.Y = ai.init_data.pos.Y;
				act.ACP.rtACP.Angle = ai.init_data.angle;
				act.ACP.stACP.ScaleX = ai.init_data.scale.X;
				act.ACP.stACP.ScaleY = ai.init_data.scale.Y;
				act.Opacity = ai.init_data.opacity;
				act.Width = ai.init_data.size.Width;
				act.Height = ai.init_data.size.Height;
				//Anim
				act.AddTranslateAnimations(ai.animations.translateList);
				act.AddRotationAnimations(ai.animations.rotationList);
				act.AddScaleAnimations(ai.animations.scaleList);
				act.AddOpacityAnimations(ai.animations.opacityList);
				StageMgr.Instance.OCActor.Add(act);
			}
		}
	}
}