using BadMovieMaker.Define;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;

namespace BadMovieMaker.Common
{
    class JsonUtils
    {
        public static Rootobject Deserialize(string jsonFile)
        {
            StringBuilder sb = new StringBuilder();
            try
            {
                string strLine;
                using (StreamReader sr = new StreamReader(jsonFile))
                {
                    while ((strLine = sr.ReadLine()) != null)
                    {
                        sb.Append(strLine);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(string.Format("The file could not be read:{0}", e.Message));
                return null;
            }
            Rootobject root = null;
            string json = string.Empty;
            try
            {
                json = sb.ToString();
                root = JsonConvert.DeserializeObject<Rootobject>(json);
            }
            catch(Exception e)
            {
                Console.WriteLine(string.Format("### AnimJsonRoot {0}", e.ToString()));
            }
            return root;
        }
    }
}