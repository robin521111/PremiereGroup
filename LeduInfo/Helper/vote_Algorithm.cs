using System;
using System.Dynamic;
using System.Collections;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Web.Mvc;

namespace LeduInfo.Models
{
    public class Vote_Algorithm
    {
        Dictionary<Guid, string> imgSort = new Dictionary<Guid, string>();
        List<string> paths = new List<string>();
        
        public Vote_Algorithm()
        {
            paths.AddRange(picpicker.imgPickers());
            foreach (var item in paths)
            {
                imgSort.Add(new Guid(), item);
            }
        }
        


    }


}
