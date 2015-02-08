using System.Collections.Generic;
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

            HandleMethod(context);
        }

        // Handle request based on method
        private void HandleMethod(HttpContext context)
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
                    UploadFile(context);
                    break;

                case "DELETE":
                    DeleteFile(context);
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
        private void DeleteFile(HttpContext context)
        {
            var filePath = StorageRoot + context.Request["f"];
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        // Upload file to the server
        private void UploadFile(HttpContext context)
        {
            var statuses = new List<FilesStatus>();
            var headers = context.Request.Headers;

            if (string.IsNullOrEmpty(headers["X-File-Name"]))
            {
                UploadWholeFile(context, statuses);
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
        private void UploadWholeFile(HttpContext context, List<FilesStatus> statuses)
        {

            for (int i = 0; i < context.Request.Files.Count; i++)
            {
                var file = context.Request.Files[i];

                var fullPath = StorageRoot + Path.GetFileName(file.FileName);
                var ext = Path.GetExtension(fullPath);
                file.SaveAs(fullPath);
                string fullName = Path.GetFileName(file.FileName);
                FilesStatus status = new FilesStatus(fullName,file.ContentLength,fullPath);
                string path = Path.GetFullPath(fullPath);

                //path = "@"+path;
                if (ext.ToString() == ".json")
                {
                    ReadJson(path);
                }
                else if (ext.ToString() == ".txt")
                {
                    status.content = ReadFile(path);
                }

                DB.UploadHandlertbl.Add(new UploadHandlerModel{
                    FileName=status.name,
                    FileType=status.type,
                    Size=status.size,
                    Content=status.content,
                    URL=status.url,
                    Delete_Type=status.delete_type,
                    Delete_Url= status.delete_url,
                    Error = status.error,
                    Progress= status.progress,
                    Thumbnail_Url = status.thumbnail_url
                });


                DB.SaveChanges();
                //statuses.Add(new FilesStatus(fullName, file.ContentLength, fullPath));
             
            }

            DB.SaveChanges();
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

          using (StreamReader file = File.OpenText(path))
          using (JsonTextReader reader = new JsonTextReader(file))
          {

          }
                
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

    public class FileHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            HttpRequestBase request = new HttpRequestWrapper(context.Request);      // Wrap the request into a HttpRequestBase type
            context.Response.ContentType = "application/json; charset=utf-8";

            // Important note: we reference the .NET 4.0 assembly here, 
            // not the .NET 4.5 version! If you use the NuGet package, make sure
            // to set the correct reference
            FileUploadHandler handler = new FileUploadHandler(request, null);       // Get an instance of the handler class
            handler.IncomingRequestStarted += handler_IncomingRequestStarted;       // Register event handler for demo purposes

            var jsonResult = handler.HandleRequest();                   // Call the handler method
            var result = jsonResult;            // JsonResult.Data is of type object and must be casted 

            context.Response.Write(JsonConvert.SerializeObject(result));            // Serialize the JQueryFileUpload object to a Json string
        }

        // Event handler for demo purposes
        void handler_IncomingRequestStarted(object sender, Backload.Eventing.Args.IncomingRequestEventArgs e)
        {
            var values = e.Param.BackloadValues;
        }



        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}