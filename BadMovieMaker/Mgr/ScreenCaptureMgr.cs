using BadMovieMaker.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Threading;
using System.Threading.Tasks;

namespace BadMovieMaker.Mgr
{
	class ScreenCaptureMgr : SingletonBase<ScreenCaptureMgr>
	{
		public async void StartRecord(Point offset,Vector size,double duration,string outputFile,string fileName)
		{
			Console.WriteLine("==========:" + duration);
			Console.WriteLine("==========:" + offset);
			await Task.Run(()=>
			{
				Process p = new Process();
				p.StartInfo.FileName = "ffmpeg.exe";
				//p.StartInfo.Arguments = string.Format(" -loglevel repeat+level+verbose -f gdigrab -video_size {0} -frames 60 -offset_x {1} -offset_y {2} -i desktop -qscale 6 -rtbufsize 100M -t {3} -c:v libx265 {4}",((int)size.X + "x" + (int)size.Y),(int)offset.X,(int)offset.Y,10,"d://out" + DateTime.Now.Millisecond + ".mp4");
				//p.StartInfo.Arguments = string.Format(" -f gdigrab -video_size {0} -offset_x {1} -offset_y {2} -i desktop -qscale 6 -rtbufsize 100M -t {3} -c:v libx265 {4}",((int)size.X + "x" + (int)size.Y),(int)offset.X,(int)offset.Y,(int)duration,"d://out" + DateTime.Now.Millisecond + ".mp4");
				p.StartInfo.Arguments = string.Format(" -f gdigrab -video_size {0} -offset_x {1} -offset_y {2} -i desktop -q:v 6 -rtbufsize 100M -y -t {3}  {4}", ( (int)size.X + "x" + (int)size.Y ), (int)offset.X, (int)offset.Y, duration, outputFile + "\\temp.avi");
				Console.WriteLine(p.StartInfo.Arguments);
				p.StartInfo.UseShellExecute = false; // 必需设置此属性为false，下面两个属性才有效
				p.StartInfo.RedirectStandardOutput = true; // 关键行2
				p.StartInfo.CreateNoWindow = true;
				p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
				p.Start();
				Console.WriteLine(p.StandardOutput.ReadToEnd());
				p.WaitForExit();
				p.Close();
				//return await p.StandardOutput.ReadToEndAsync();//.ReadToEnd();
			});
			await Task.Run(()=>
			{
				Process p = new Process();
				p.StartInfo.FileName = "ffmpeg.exe";
				p.StartInfo.Arguments = string.Format(" -i {0} -y {1}", outputFile + "\\temp.avi", outputFile + "\\" + fileName);
				Console.WriteLine(p.StartInfo.Arguments);
				p.StartInfo.UseShellExecute = false; // 必需设置此属性为false，下面两个属性才有效
				p.StartInfo.RedirectStandardOutput = true; // 关键行2
				p.StartInfo.CreateNoWindow = true;
				p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
				p.Start();
				//Console.WriteLine(p.StandardOutput.ReadToEnd());
				p.WaitForExit();
				p.Close();
			});
			StageMgr.instance.MaximizeStage();
		}
	}
}
