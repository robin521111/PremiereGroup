using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Infrustructure;
using System.Data.Entity;
using System.Configuration;
using System.Drawing.Imaging;
using System.IO;
using System.IO.IsolatedStorage;
using System.Security.Permissions;
using System.Web.Security;
using System.Security.AccessControl;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace Premiere.Models
{
    [FileIOPermission(SecurityAction.Demand, AllLocalFiles=FileIOPermissionAccess.AllAccess)]
    public class FileModel
    {

        #region initial instant of Data
        public int FileID { get; set; }
        public string FileName { get; set; }
        public string LastModified { get; set; }
        public List<string> FilePaths { get; set; }
        PremiereDB DB = new PremiereDB();
        #endregion




        #region doesn't use it any more
        //public FileModel()
        //{
        //    FileIOPermission f = new FileIOPermission(FileIOPermissionAccess.Write, ConfigurationManager.AppSettings["ImgBasePath"].ToString());
        //    f.AddPathList(FileIOPermissionAccess.AllAccess, "C:\\CodeForFun\\InforwebMvc4\\Premiere\\Images\\ImgFolder");
        //    f.Demand();
        //    if (Directory.GetDirectories(ConfigurationManager.AppSettings["ImgBasePath"]).Length > 0 || Directory.GetFiles(ConfigurationManager.AppSettings["ImgBasePath"]).Length > 0)
        //    {
               
        //        FileInfo fi = new FileInfo(ConfigurationManager.AppSettings["ImgBasePath"].ToString());
        //        FileSecurity fs=fi.GetAccessControl();
        //        fs.AddAccessRule(new FileSystemAccessRule(Environment.UserName,FileSystemRights.FullControl,AccessControlType.Allow));
                
        //        fi.CopyTo("~/Images/ImgFolder", true);
        //        FileSecurity FS = new FileSecurity();

        //        fi.SetAccessControl(FS);
                
        //    }
            
        //}
        //public void SaveFilePath()
        //{
        //   var paths = img.GetFilePaths(ConfigurationManager.AppSettings["ImgBasePath"].ToString());
            
        //   foreach (var item in paths)
        //   {
        //       var image = from images in DB.FileResourcetbl
        //                   where images.Path==item 
        //                   select images;

        //       if (!image.Any())
        //       {
        //           DB.FileResourcetbl.Add(new FileResource { Path= item, FileTypeID= 1 });
        //           DB.FileTypetbl.Add(new FileType { fileType = "image" });
        //       }
              
        //   } 
        //    DB.SaveChanges();
        //}
        #endregion
        public void SaveFileAsRelativePath()
        {
            
        }
                
    }




    [FileIOPermission(SecurityAction.Demand, AllLocalFiles=FileIOPermissionAccess.AllAccess)]
    [Table("UploadedDataTbl")]
    public class UploadHandlerModel
    {
        [Required]
        [Key]
        public int FileID { get; set; } 

        public string FileName { get; set; }

        public string FileType { get; set; }

        public int Size { get; set; }

        public string Content { get; set; }

        public string Progress { get; set; }

        public string URL { get; set; }

        public string Thumbnail_Url { get; set; }

        public string Delete_Url { get; set; }

        public string Delete_Type { get; set; }

        public string Error { get; set; }

        public DateTime LastModified { get; set; }

        public string LastModifiedBy { get; set; }

    }

}