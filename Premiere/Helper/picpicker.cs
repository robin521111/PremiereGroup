using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.IO;
using System.Data;



namespace LeduInfo.Models
{
    public static class picpicker
    {
        //public static void  imgPicker()
        //{
        //    var db = new leduInfoDB();
            
            
        //    var rootpath = ConfigurationManager.AppSettings["ImgBasePath"];
        //    if (Directory.Exists(rootpath))
        //    {
        //        foreach (var item in Directory.GetFiles(rootpath))
        //        {
        //           var foo=@item;
        //           var filepathInDB = from p in db.ImgPathstbl
        //                          where p.Path==foo
        //                           select p.Path;
                                 
                   
        //           if (filepathInDB.Any())
        //           {
        //               continue;
        //           }

        //            db.ImgPathstbl.Add(new ImgPath { Path = item});
                    
        //        }
        //        db.SaveChanges();
        //    }

        //}
        public static List<string> imgPickers()
        {
            List<string> paths = new List<string>();
            var rootpath = ConfigurationManager.AppSettings["ImgBasePath"];
            if (Directory.Exists(rootpath))
            {
                foreach (var item in Directory.GetFiles(rootpath))
                {
                    paths.Add(item);
                }
            }
            return paths;
        }
    }
}