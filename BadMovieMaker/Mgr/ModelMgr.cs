using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BadMovieMaker.Common;
using BadMovieMaker.Model;

namespace BadMovieMaker.Mgr
{
    /// <summary>
    /// 所有的model管理类
    /// </summary>
    internal class ModelMgr : SingletonBase<ModelMgr>
    {
        public Dictionary<string, ModelBase> modelDic = new Dictionary<string, ModelBase>();

        public void RegisterModel (string key, ModelBase model)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new Exception("注册的model名是空值!");
            }
            if(model == null)
            {
                throw new Exception("注册的model实例是空值!");
            }

            if(!modelDic.ContainsKey(key))
            {
                modelDic.Add(key, model);
            }
            else
            {
                throw new Exception(string.Format("注册名为{0}的model已经存在!",key));
            }
        }

        public void UnregisterModel(string key)
        {
            if(modelDic.ContainsKey(key))
            {
                modelDic.Remove(key);
            }
        }

        public T GetModel<T>(string key) where T : ModelBase
        {
            if (modelDic.ContainsKey(key))
            {
                return modelDic[key] as T;
            }
            return null;
        }

        public void PringAllModelName()
        {
            Console.WriteLine("============PringAllModelName Start==============");
            foreach(KeyValuePair<string,ModelBase> kv in modelDic)
            {
                Console.WriteLine(string.Format("model-name:{0}; model-type:{1}", kv.Key,kv.GetType().FullName));
            }
            Console.WriteLine("============PringAllModelName End==============");
        }
    }
}
