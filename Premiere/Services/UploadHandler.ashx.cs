﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using Premiere.Helper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using System.Web.Mvc;
using Backload.Plugin.Handler;
using System.Threading.Tasks;
using Backload;
using System;
using System.Web.Security;
using Premiere.Models;

namespace Premiere.Services
{
    /// <summary>
    /// Summary description for UploadHandler
    /// </summary>
    public class UploadHandler : IHttpHandler
    {
        private readonly JavaScriptSerializer js;
        private Premiere.Models.PremiereDB DB = new PremiereDB();


        private string StorageRoot
        {
            get { return Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Upload/ChartData/")); } //Path should! always end with '/'
        }    

        public UploadHandler()
        {
            js = new JavaScriptSerializer();
            js.MaxJsonLength = 41943040;
        }

        public bool IsReusable { get { return false; } }

        public void ProcessRequest(HttpContext context)
        {
            context.Response.AddHeader("Pragma", "no-cache");
            context.Response.AddHeader("Cache-Control", "private, no-cache");
            
            string folderName = context.Request.QueryString["title"];
            HandleMethod(context,folderName);
        }

        // Handle request based on methods
        private void HandleMethod(HttpContext context, string folderName)
        {
            switch (context.Request.HttpMethod)
            {
                case "HEAD":
                case "GET":
                    if (GivenFilename(context)) DeliverFile(context);
                    else ListCurrentFiles(context);
                    break;

                case "POST":
                case "PUT":
                    UploadFile(context,folderName);
                    break;

                case "DELETE":
                    DeleteFile(context, folderName);
                    break;

                case "OPTIONS":
                    ReturnOptions(context);
                    break;

                default:
                    context.Response.ClearHeaders();
                    context.Response.StatusCode = 405;
                    break;
            }
        }

        private static void ReturnOptions(HttpContext context)
        {
            context.Response.AddHeader("Allow", "DELETE,GET,HEAD,POST,PUT,OPTIONS");
            context.Response.StatusCode = 200;
        }

        // Delete file from the server
        private void DeleteFile(HttpContext context, string folderName)
        {
            var filePath = StorageRoot +folderName+"\\" +folderName+'_'+ context.Request["f"];
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        // Upload file to the server
        private void UploadFile(HttpContext context, string folderName)
        {
            var statuses = new List<FilesStatus>();
            var headers = context.Request.Headers;

            if (string.IsNullOrEmpty(headers["X-File-Name"]))
            {
                UploadWholeFile(context, statuses,folderName);
            }
            else
            {
                UploadPartialFile(headers["X-File-Name"], context, statuses);
            }

            WriteJsonIframeSafe(context, statuses);
     
        }

        // Upload partial file
        private void UploadPartialFile(string fileName, HttpContext context, List<FilesStatus> statuses)
        {
            if (context.Request.Files.Count != 1) throw new HttpRequestValidationException("Attempt to upload chunked file containing more than one fragment per request");
            var inputStream = context.Request.Files[0].InputStream;
            var fullName = StorageRoot + Path.GetFileName(fileName);

            using (var fs = new FileStream(fullName, FileMode.Append, FileAccess.Write))
            {
                var buffer = new byte[1024];

                var l = inputStream.Read(buffer, 0, 1024);
                while (l > 0)
                {
                    fs.Write(buffer, 0, l);
                    l = inputStream.Read(buffer, 0, 1024);
                }
                fs.Flush();
                fs.Close();
            }
            statuses.Add(new FilesStatus(new FileInfo(fullName)));
        }

        // Upload entire file
        private void UploadWholeFile(HttpContext context, List<FilesStatus> statuses, string folderName)
        {

            for (int i = 0; i < context.Request.Files.Count; i++)
            {
                var file = context.Request.Files[i];
                var fullPath = StorageRoot + folderName+ "\\"+ folderName+'_'+ Path.GetFileName(file.FileName);
                var folderPath = StorageRoot + folderName ;
                
                if (!File.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                var ext = Path.GetExtension(fullPath);
                file.SaveAs(fullPath);
                string fullName = Path.GetFileName(file.FileName);
                FilesStatus status = new FilesStatus(fullName,file.ContentLength,fullPath);
                string path = Path.GetFullPath(fullPath);
                
                
                DB.UploadHandlertbl.Add(new UploadHandlerModel{
                    FileName=status.name,
                    FileType=status.type,
                    Size=status.size,
                    URL=status.url,
                    Delete_Type=status.delete_type,
                    Delete_Url= status.delete_url,
                    Error = status.error,
                    Progress= status.progress,
                    Thumbnail_Url = status.thumbnail_url,
                    LastModified=DateTime.Now,
                    LastModifiedBy = Membership.GetUser().UserName.ToString()
                  
                });

                DataSync(folderName, status, path);
                //statuses.Add(new FilesStatus(fullName, file.ContentLength, fullPath));
             
            }

            DB.SaveChanges();
        }


        private void DataSync(string folderName, FilesStatus status, string path)
        {
            switch (folderName)
            {
                case  "品牌传播曝光度分析-每日数据导入":
                    var text = File.ReadAllText(path,System.Text.Encoding.UTF8);
                    JObject obj = JObject.Parse(text);
                    string data = obj["series"].ToString();
                    string xAxis = obj["xAxis"].ToString();
                    string t_month = obj["date"].ToString();
                    int i  = status.name.IndexOf('-', 0);
                    string brand_name = status.name.Substring(0, i);
                    string year = obj["date"].ToString().Substring(0, 4);
                    string mon = "";
                    if (obj["date"].ToString().Length == 8)
                    {
                        mon = obj["date"].ToString().Substring(5, 2);
                    }
                    else
                    {
                        mon = obj["date"].ToString().Substring(5, 1);
                        mon = '0'+mon;
                    }
                    t_month = year + mon;
                    int month = Convert.ToInt32(t_month);
                    string file_name = status.name.Replace("(一个月).txt", "");
                    DB.BrandExposureLinetbl.Add(new BrandExposureLine
                    {
                        ChartID = 1,
                        BrandName=brand_name,
                        Series= data,
                        xAxis=xAxis,
                        Month=month,
                        LastModified = DateTime.Now,
                        LastModifiedBy = Membership.GetUser().UserName.ToString(),
                    });

                    break;
                case "品牌传播曝光度分析-每月数据导入":
                    text = File.ReadAllText(path, System.Text.Encoding.UTF8);
                    obj = JObject.Parse(text);
                    file_name = status.name.Replace(".txt", "");
                    data = obj["series"].ToString();
                    DB.BrandExposureBubbletbl.Add(new BrandExposureBubble
                    {
                        ChartID=2,
                        BrandName=file_name,
                        Series=data,
                        LastModified=DateTime.Now,
                        LastModifiedBy=Membership.GetUser().UserName.ToString(),
                    });

                    break;
                case "品牌传播曝光度分析微博论坛数据":
                    text = File.ReadAllText(path, System.Text.Encoding.UTF8);
                    obj = JObject.Parse(text);
                    file_name = status.name.Replace(".txt", "");
                    data = obj["data"].ToString();
                    if (obj["data"]["wordgraph"][0]["features"].ToString() == "[]")
                    {
                        break;
                    }
                    int period = Convert.ToInt32(obj["date"].ToString());
                    DB.BrandSpreadMapBlogtbl.Add(new BrandSpreadMapBlog
                    {
                        Content=text,
                        Period=period,
                        BrandName = file_name,
                        LastModified = DateTime.Now,
                        LastModifiedBy = Membership.GetUser().UserName.ToString()
                    });
                    

                    break;
                case "品牌关键字分析新闻数据":
                    text = File.ReadAllText(path, System.Text.Encoding.UTF8);
                    obj = JObject.Parse(text);
                    file_name = status.name.Replace(".txt", "");
                    data = obj["data"].ToString();
                    if (obj["data"]["wordgraph"][0]["features"].ToString() == "[]")
                    {
                        break;
                    }
                    period = Convert.ToInt32(obj["date"].ToString());
                    DB.BrandSpreadMapNewstbl.Add(new BrandSpreadMapNews
                    {
                        Content = text,
                        Period = period,
                        BrandName = file_name,
                        LastModified = DateTime.Now,
                        LastModifiedBy = Membership.GetUser().UserName.ToString()
                    });


                    break;
                case "品牌关键字分析微博论坛数据":
                    text = File.ReadAllText(path, System.Text.Encoding.UTF8);
                    obj = JObject.Parse(text);
                    file_name = status.name.Replace(".txt", "");
                    data = obj["data"].ToString();
                    if (obj["data"]["wordgraph"][0]["features"].ToString() == "[]")
                    {
                        break;
                    }
                    period = Convert.ToInt32(obj["date"].ToString());
                    DB.BrandSpreadMapBlogtbl.Add(new BrandSpreadMapBlog
                    {
                        Content = text,
                        Period = period,
                        BrandName = file_name,
                        LastModified = DateTime.Now,
                        LastModifiedBy = Membership.GetUser().UserName.ToString()
                    });


                    break;

                    
                case "品牌形象分析论坛微博数据":
                    text = File.ReadAllText(path, System.Text.Encoding.UTF8);
                    obj = JObject.Parse(text);
                    file_name = status.name.Replace(".txt", "");
                    data = obj["series"].ToString();
                    xAxis = obj["xAxis"].ToString();

                    DB.BrandImageBlogtbl.Add(new BrandImageBlog
                    {
                        ChartID = 3,
                        BrandName = file_name,
                        Series = data,
                        xAxis = xAxis,
                        LastModified = DateTime.Now,
                        LastModifiedBy = Membership.GetUser().UserName.ToString(),
                    });

                    break;

                case "品牌形象分析新闻数据":
                    text = File.ReadAllText(path, System.Text.Encoding.UTF8);
                    obj = JObject.Parse(text);
                    file_name = status.name.Replace(".txt", "");
                    data = obj["series"].ToString();
                    xAxis = obj["xAxis"].ToString();

                    DB.BrandImageNewstbl.Add(new BrandImageNews
                    {
                        ChartID = 3,
                        BrandName = file_name,
                        Series = data,
                        xAxis = xAxis,
                        LastModified = DateTime.Now,
                        LastModifiedBy = Membership.GetUser().UserName.ToString(),
                    });

                    break;
                case "设计感与功能性分析图":
                    text = File.ReadAllText(path, System.Text.Encoding.UTF8);
                    obj = JObject.Parse(text);
                    file_name = status.name.Replace(".txt", "");
                    data = obj["series"].ToString();
                    xAxis = obj["xAxis"].ToString();

                    DB.DesignSensetbl.Add(new DesignSense
                    {
                        ChartID=3,
                        BrandName=file_name,
                        Series=data,
                        xAxis=xAxis,
                        LastModified=DateTime.Now,
                        LastModifiedBy=Membership.GetUser().UserName.ToString(),
                    });
                    break;
                case "品牌用户属性分析(性别)":
                    text = File.ReadAllText(path, System.Text.Encoding.UTF8);
                    obj = JObject.Parse(text);
                    file_name = status.name.Replace(".txt", "");
                    data = obj["series"].ToString();
                    DB.SexRatiotbl.Add(new SexRatio { 
                         ChartID=4,
                         BrandName=file_name,
                         Series=data,
                         LastModifed=DateTime.Now,
                         LastModifiedBy=Membership.GetUser().UserName.ToString(),
                    });
                    break;
                case "品牌关注点分析论坛微博数据":
                    text = File.ReadAllText(path, System.Text.Encoding.UTF8);
                    obj = JObject.Parse(text);
                    file_name = status.name.Replace(".txt", "");
                    data = obj["series"].ToString();
                    xAxis = obj["xAxis"].ToString();
                    month = int.Parse( obj["series"]["Month"].ToString());
                    DB.BrandFocusBlogtbl.Add(new BrandFocusBlog {
                        ChartID = 3,
                        BrandName = file_name,
                        Month = month,
                        Series = data,
                        xAxis = xAxis,
                        LastModified = DateTime.Now,
                        LastModifiedBy = Membership.GetUser().UserName.ToString(),
                    
                    });
                    break;

                case "品牌关注点分析新闻数据":
                    text = File.ReadAllText(path, System.Text.Encoding.UTF8);
                    obj = JObject.Parse(text);
                    file_name = status.name.Replace(".txt", "");
                    data = obj["series"].ToString();
                    xAxis = obj["xAxis"].ToString();
                    month = int.Parse(obj["series"]["Month"].ToString());
                    DB.BrandFocusNewstbl.Add(new BrandFocusNews
                    {
                        ChartID = 3,
                        BrandName = file_name,
                        Month = month,
                        Series = data,
                        xAxis = xAxis,
                        LastModified = DateTime.Now,
                        LastModifiedBy = Membership.GetUser().UserName.ToString(),

                    });
                    break;
                case "媒体关注度分析图谱":
                    text = File.ReadAllText(path, System.Text.Encoding.UTF8);
                    obj = JObject.Parse(text);
                    file_name = status.name.Replace(".txt", "");
                    period = Convert.ToInt32(obj["date"].ToString());
                    if (obj["data"]["wordgraph"][0]["features"].ToString() == "[]")
	                    {
                            break;
	                    }
                    DB.MediaFocusMaptbl.Add(new MediaFocusMap
                    {
                        Content = text,
                        Period = period,
                        BrandName = file_name,
                        LastModified = DateTime.Now,
                        LastModifiedBy = Membership.GetUser().UserName.ToString(),
                    });
                    break;
                default:
                    break;
            }
        }
        private string ReadFile(string path)
        {
              string text = File.ReadAllText(path);
             return text;
        }

        private void ReadJson(string path)
        {
         //JObject obj = JObject.Parse(File.ReadAllText(path));
                //read Json from file
            var text  = File.ReadAllText(path);
            //JsonTextReader reader = new JsonTextReader(new StringReader(text));
            JObject obj = JObject.Parse(text);
            //IList<JToken> result = obj["data"].Children().ToList();
            var result = obj["data"];
                
        }

        private void WriteJsonIframeSafe(HttpContext context, List<FilesStatus> statuses)
        {
            context.Response.AddHeader("Vary", "Accept");
            try
            {
                if (context.Request["HTTP_ACCEPT"].Contains("application/json"))
                    context.Response.ContentType = "application/json";
                else
                    context.Response.ContentType = "text/plain";
            }
            catch
            {
                context.Response.ContentType = "text/plain";
            }

            var jsonObj = js.Serialize(statuses.ToArray());
            context.Response.Write(jsonObj);
        }

        private static bool GivenFilename(HttpContext context)
        {
            return !string.IsNullOrEmpty(context.Request["f"]);
        }

        private void DeliverFile(HttpContext context)
        {
            var filename = context.Request["f"];
            var filePath = StorageRoot + filename;

            if (File.Exists(filePath))
            {
                context.Response.AddHeader("Content-Disposition", "attachment; filename=\"" + filename + "\"");
                context.Response.ContentType = "application/octet-stream";
                context.Response.ClearContent();
                context.Response.WriteFile(filePath);
            }
            else
                context.Response.StatusCode = 404;
        }

        private void ListCurrentFiles(HttpContext context)
        {
            var files =
                new DirectoryInfo(StorageRoot)
                    .GetFiles("*", SearchOption.TopDirectoryOnly)
                    .Where(f => !f.Attributes.HasFlag(FileAttributes.Hidden))
                    .Select(f => new FilesStatus(f))
                    .ToArray();

            string jsonObj = js.Serialize(files);
            context.Response.AddHeader("Content-Disposition", "inline; filename=\"files.json\"");
            context.Response.Write(jsonObj);
            context.Response.ContentType = "application/json";
        }

    }

    //public class FileHandler : IHttpHandler
    //{

    //    //public void ProcessRequest(HttpContext context)
    //    //{
    //    //    HttpRequestBase request = new HttpRequestWrapper(context.Request);      // Wrap the request into a HttpRequestBase type
    //    //    context.Response.ContentType = "application/json; charset=utf-8";

    //    //    // Important note: we reference the .NET 4.0 assembly here, 
    //    //    // not the .NET 4.5 version! If you use the NuGet package, make sure
    //    //    // to set the correct reference
    //    //    FileUploadHandler handler = new FileUploadHandler(request, null);       // Get an instance of the handler class
    //    //    handler.IncomingRequestStarted += handler_IncomingRequestStarted;       // Register event handler for demo purposes

    //    //    var jsonResult = handler.HandleRequestAsync();               // Call the handler method
    //    //    var result = jsonResult;            // JsonResult.Data is of type object and must be casted 

    //    //    context.Response.Write(JsonConvert.SerializeObject(result));            // Serialize the JQueryFileUpload object to a Json string
    //    //}

    //    // Event handler for demo purposes
    //    void handler_IncomingRequestStarted(object sender, Backload.Eventing.Args.IncomingRequestEventArgs e)
    //    {
    //        var values = e.Param.BackloadValues;
    //    }



    //    public bool IsReusable
    //    {
    //        get
    //        {
    //            return false;
    //        }
    //    }
    //}
}