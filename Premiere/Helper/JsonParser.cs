using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using System.IO;
using Newtonsoft;
using Newtonsoft.Json.Linq;
using System.Text;

namespace LeduInfo.Models
{
    public class JsonParser 
    {
        /// <summary>
        /// 为第一张趋势图转换数据
        /// </summary>
        /// <param name="path">json文件的路径</param>
        public static StringBuilder JsonParser_Liner(string path)
        {
            StreamReader reader = new StreamReader(path);
                JsonTextReader Json_reader = new JsonTextReader(reader);
                JArray JsonRoot = JArray.Load(Json_reader);
                StringBuilder sb = new StringBuilder();
                foreach (JObject item in JsonRoot)
                {
                    //Response.Write("[");
                    //Response.Write(item["time"]);
                    //Response.Write(item["],"]);
                    //Response.Write(item["value"]);
                    //Response.Write("</br>");
                    sb.Append("[" + item["time"] + "], " + item["value"] + "," + "</br>");
                }
            return sb;
        }



        public static StringBuilder JsonParser_detail(string path)
        {
            StreamReader reader = new StreamReader(path);
            JsonTextReader Json_reader = new JsonTextReader(reader);
            JArray JsonRoot = JArray.Load(Json_reader);
            StringBuilder sb = new StringBuilder();
            foreach (JObject item in JsonRoot)
            {
                sb.Append(item["value"] + "," );
            }
            return sb;
        }
        
        

        public static StringBuilder JsonParser_linerMonthly(string path)
        {
            var db = new leduInfoDB();
            StreamReader reader = new StreamReader(path);
            JsonTextReader Json_reader = new JsonTextReader(reader);
            JArray JsonRoot = JArray.Load(Json_reader);
            StringBuilder sb = new StringBuilder();
            //List<JToken> JsonList=new List<JToken>();
            //var time =from timevalue in JsonRoot
            //               orderby timevalue["time"]
            //               select timevalue["time"] ;

            //foreach (var item in time.ToArray())
            //{
            //    sb.Append("[" + item["time"] + "] "+ "</br>");
            //}

            foreach (JObject item in JsonRoot)
	        {
                var Time = item["time"].ToString();
                var Value = Convert.ToInt32(item["value"]);

                var time = from model in db.JsonModeltbl
                                    where model.Time == Time
                                    select model.Time;


                var value = from model in db.JsonModeltbl
                                    where model.Value == Value
                                    select model.Value;

                //List<string> time= (from model in db.JsonModel
                //                    select model.Time).ToList();

                //List<string> value = (from model in db.JsonModel
                //                      select model.Value).ToList();

                db.JsonModeltbl.Add(new JsonModel { Time = Time, Value = Value });
                db.SaveChanges();             
                             
            for (int i = 1; i < 12; i++)
            {
               
            }
            }
            return sb;
            
            

        }

    }
}
