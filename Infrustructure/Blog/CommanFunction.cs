using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.IO;

namespace Infrustructure.Blog
{
   public class CommanFunction
    {


        public enum ImageType
        {
            img,
            png,
            jpg,
            gif
        }

        public static string GetImageLink(string content)
        {
            string imgPath=string.Empty;
            HtmlDocument doc = new HtmlDocument();
            

            //doc.Load(reader);
           //imgPath= notes.SelectSingleNode("html/img[1]").ToString();
            
            return imgPath;
        }

      
    }
}
