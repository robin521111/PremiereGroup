using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.IO;
using Newtonsoft;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;

namespace Infrustructure
{
   public class UtilFunction
    {
        public enum ImageType
        {
            gif,
            jpg,
            png,

        }


        public void AddMonthDate()
        {
            DirectoryInfo di = new DirectoryInfo("C:\\CodeForFun\\PremiereGroup\\Premiere\\123");
            List<string> paths = new List<string>();

            foreach (var item in di.GetFiles("*.txt",SearchOption.AllDirectories))
            {
                string textcontent = "";
                string parentName = item.Directory.Name;
                using (StreamReader sr = new StreamReader(item.FullName))
                {
                    textcontent = sr.ReadToEnd();

                }
                if (parentName.Length > 10)
	            {
		            if (parentName.Length ==13)
	                {
                        string month = parentName.Substring(12,1);
                        JObject rss = JObject.Parse(textcontent);
                        rss["series"]["Month"]="20140" + month ;
                        File.WriteAllText(@"C:\CodeForFun\PremiereGroup\Premiere\123\WEIBO\" +rss["series"]["Month"]+'\\' + rss["series"]["name"].ToString() + ".txt", rss.ToString());
	                }
                    else if (parentName.Length==14)
                    {
                        string month = parentName.Substring(12, 2);
                        JObject rss = JObject.Parse(textcontent);
                        rss["series"]["Month"]="2014" + month;
                        File.WriteAllText(@"C:\CodeForFun\PremiereGroup\Premiere\123\WEIBO\" + rss["series"]["Month"] + '\\' + rss["series"]["name"].ToString() + ".txt", rss.ToString());
                    }
	            }
                else if (parentName.Length==5)
                {
                    string month = parentName.Substring(4, 1);
                    JObject rss = JObject.Parse(textcontent);
                    rss["series"]["Month"]= "20140" + month ;
                    File.WriteAllText(@"C:\CodeForFun\PremiereGroup\Premiere\123\NEWS\" + rss["series"]["Month"] + '\\' + rss["series"]["name"].ToString() + ".txt", rss.ToString());
                }
                else if (parentName.Length == 6)
                {
                    string month = parentName.Substring(4, 2);
                    JObject rss = JObject.Parse(textcontent);
                    //var xAxis = rss["xAxis"] as JObject;
                    //xAxis.Properties("
                    rss["series"]["Month"] = "2014" + month;
                    File.WriteAllText(@"C:\CodeForFun\PremiereGroup\Premiere\123\NEWS\" + rss["series"]["Month"] + '\\' + rss["series"]["name"].ToString() + ".txt", rss.ToString());
                }
                
            }
            //string path = System.Web.HttpContext.Current.Server.MapPath("~/123");
            //Path.GetFileName(Path.GetDirectoryName(path));

            
        }
    }
}
