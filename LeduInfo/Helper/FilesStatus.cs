using System;
using System.IO;
using LeduInfo.Models;

namespace LeduInfo.Helper
{
    public class FilesStatus
    {
        private PremiereDB DB = new PremiereDB();
        public const string HandlerPath = "/Upload/";

        public string group { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public int size { get; set; }
        public string progress { get; set; }
        public string url { get; set; }
        public string thumbnail_url { get; set; }
        public string delete_url { get; set; }
        public string delete_type { get; set; }
        public string error { get; set; }
        public string content { get; set; }

        public FilesStatus() { }

        public FilesStatus(FileInfo fileInfo) { SetValues(fileInfo.Name, (int)fileInfo.Length, fileInfo.FullName); }

        public FilesStatus(string fileName, int fileLength, string fullPath) { SetValues(fileName, fileLength, fullPath); }

        private void SetValues(string fileName, int fileLength, string fullPath)
        {
            name = fileName;
            type = "json/txt";
            size = fileLength;
            progress = "1.0";
            url = HandlerPath + "UploadHandler.ashx?f=" + fileName;
            delete_url = HandlerPath + "UploadHandler.ashx?f=" + fileName;
            delete_type = "DELETE";
            content = "";

            var ext = Path.GetExtension(fullPath);
            
            var fileSize = ConvertBytesToMegabytes(new FileInfo(fullPath).Length);
            if (IsJson(ext)) thumbnail_url = "/Content/Backload/content-types/128/Text.png";
            else thumbnail_url = @"data:image/png;base64," + EncodeFile(fullPath);
        }

        private bool IsImage(string ext)
        {
            return ext == ".gif" || ext == ".jpg" || ext == ".png";
        }

        private bool IsJson(string ext)
        {
            return ext == ".json" || ext == ".txt";
        }

        private string EncodeFile(string fileName)
        {
            return Convert.ToBase64String(System.IO.File.ReadAllBytes(fileName));
        }

        static double ConvertBytesToMegabytes(long bytes)
        {
            return (bytes / 1024f) / 1024f;
        }
    }
}