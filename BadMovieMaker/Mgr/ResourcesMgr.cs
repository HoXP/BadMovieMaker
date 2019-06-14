using BadMovieMaker.Common;
using BadMovieMaker.Define;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;

namespace BadMovieMaker.Mgr
{
    class ResourcesMgr : SingletonBase<ResourcesMgr>
    {
        private ulong TotalNum = 0; //文件及文件夹数目
        private string strpp = "../../";
        private string rootPath;
        internal ObservableCollection<FileData> FileInfos { get; private set; }

        public ResourcesMgr()
        {
            rootPath = string.Format("{0}/{1}Resources", Environment.CurrentDirectory, strpp);
            FileInfos = GetAllDirectory(FileInfos, rootPath);
        }

        private ObservableCollection<FileData> GetFileDatas(ObservableCollection<FileData> list, string filepath)
        {//获得指定路径下所有文件名
            DirectoryInfo root = new DirectoryInfo(filepath);
            foreach (FileInfo f in root.GetFiles())
            {
                FileData fd = new FileData
                {
                    Id = ++TotalNum,
                    Text = f.Name,
                    FullName = f.FullName,
                    FileState = new State { Opened = false },
                    EFileType = GetFileTypeByExt(f.Extension),
                    Icon = GetIconByExt(f.Extension),
                    Thumbnail = GetThumbnail(f.FullName)
                };
                list.Add(fd);
            }
            return list;
        }
        public ObservableCollection<FileData> GetAllDirectory(ObservableCollection<FileData> list, string path)
        {//获得指定路径下的所有子目录名
            if(list == null)
            {
                list = new ObservableCollection<FileData>();
            }
            DirectoryInfo root = new DirectoryInfo(path);
            DirectoryInfo[] dirs = root.GetDirectories();
            if (dirs.Length != 0)
            {
                foreach (DirectoryInfo d in dirs)
                {
                    FileData fd = new FileData
                    {
                        Id = ++TotalNum,
                        Text = d.Name,
                        FullName = d.FullName,
                        FileState = new State { Opened = false },
                        EFileType = FileType.Folder,
                        Icon = GetIconByExt(string.Empty),
                        Children = GetAllDirectory(new ObservableCollection<FileData>(), d.FullName)
                    };
                    list.Add(fd);
                }
            }
            list = GetFileDatas(list, path);
            return list;
        }
        public void DeleteFile(FileData fileData)
        {//删除指定文件
            //for (int i = 0; i < FileInfos.Count; i++)
            //{
            //    if(FileInfos[i].Children!=null)
            //    {

            //    }
            //    if(fileData.Id == FileInfos[i].Id)
            //    {
            //        DirectoryInfo di = new DirectoryInfo(FileInfos[i].FullName);
            //        di.Delete(true);
            //        FileInfos.RemoveAt(i);
            //        break;
            //    }
            //}
            Console.WriteLine(string.Format("### DeleteFile"));
        }
        private string GetIconByExt(string ext)
        {
            FileType ft = GetFileTypeByExt(ext);
            string icon = string.Empty;
            if (ft == FileType.Folder)
            {
                icon = "folder";
            }
            else if(ft == FileType.Pic)
            {
                icon = "image";
            }
            else if(ft == FileType.Media)
            {
                icon = "video";
            }
            return string.Format("{0}Res/{1}.png", PathDefine.PackBase, icon);
        }
        private string GetThumbnail(string fullName)
        {
            string partName = fullName.Substring(fullName.IndexOf("Resources"));
            partName = partName.Replace("\\","/");
            return string.Format("{0}{1}", PathDefine.PackBase, partName);
        }
        private FileType GetFileTypeByExt(string ext)
        {
            FileType ft =  FileType.Folder;
            if (ext == ".jpg" || ext == ".png")
            {
                ft = FileType.Pic;
            }
            else if (_mediaExts.Contains(ext))
            {
                ft = FileType.Media;
            }
            return ft;
        }
        private List<string> _mediaExts = new List<string>{ ".wmv" , ".avi", ".mp4", ".mp3" };
    }

    public class FileData
    {
        public ulong Id { get; set; }
        public string Text { get; set; }
        public FileType EFileType { get; set; }
        public State FileState { get; set; }
        public ObservableCollection<FileData> Children { get; set; }
        public string Icon { get; set; }
        public string Thumbnail { get; set; }
        public string FullName { get; set; }
    }
    public class State
    {
        public bool Opened { get; set; }
    }
    public enum FileType
    {
        Folder,
        Pic,
        Media
    }
}