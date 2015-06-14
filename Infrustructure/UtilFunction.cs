using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.IO;

namespace Infrustructure
{
    class UtilFunction
    {
        public enum ImageType
        {
            gif,
            jpg,
            png,

        }


        public void AddMonthDate()
        {
            string path = System.Web.HttpContext.Current.Server.MapPath("~/123");
            Path.GetFileName(Path.GetDirectoryName(path));
            
        }
    }
}
