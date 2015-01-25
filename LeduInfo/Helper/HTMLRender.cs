using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using LeduInfo.Models;

namespace LeduInfo.Models
{
    public class HTMLRender
    {
        public static string PresentationRender(int spanNum, string imgPath, string altMessage, string Description, string smallDes, string warpWordsTitle, string warpContent)
        {
            StringBuilder HTML = new StringBuilder();
            HTML.AppendLine("<ul class=\"thumbnails\">");
            HTML.AppendLine("<li class=\"span" + spanNum + "\">");
            HTML.AppendLine("<div class=\"thumbnail\">");
            HTML.AppendLine("<img src=" + imgPath + "\"\" alt=\"" + altMessage + "\"/>");
            HTML.AppendLine("<blockquote>");
            HTML.AppendLine("<p>" + Description + "</p>");
            HTML.AppendLine("<small>" + smallDes + "<cite title=\"" + warpWordsTitle + "\">" + warpContent + "</cite></small>");
            HTML.AppendLine("</blockquote>");
            HTML.AppendLine("</div>");
            HTML.AppendLine("</li>");
            HTML.AppendLine("</ul>");
            return HTML.ToString();
        }

        public static string RendImageList()
        {
            StringBuilder ImageHTML = new StringBuilder();
            
            ImageHTML.AppendLine("<li><img class=\"item\" src=\"../../Images/fashion.png\" width=\"70\" alt=\"\"></li>");
            return ImageHTML.ToString();
            
        }
    }
}