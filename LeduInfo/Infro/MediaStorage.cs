using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Configuration;
using System.Data;
using System.Globalization;

namespace LeduInfo.Infro
{
    public class MediaStorage:IMediaStorage
    {
        public string Storage(MemoryStream stream, string filename)
        {
            var virtualPath = ConfigurationManager.AppSettings["VitualPath"].ToString(CultureInfo.InstalledUICulture);
            var physicalPath = HttpContext.Current.Request.MapPath(virtualPath);

            if (string.IsNullOrEmpty(physicalPath))
            {
                throw new NoNullAllowedException("Physical path should not be null");
              
            }
            var fullPath = Path.Combine(physicalPath, filename);
            //stream.WriteTo(fullPath);
            return filename;
        }
    }
}