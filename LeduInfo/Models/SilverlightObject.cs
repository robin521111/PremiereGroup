using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;

namespace LeduInfo.Models
{
    public class SilverlightObject
    {
        public string XapName { get; set; }
        public Size Size { get; set; }
        public string OnSilverlightError { get; set; }
        public Color BackgroundColor { get; set; }
        public string MinimumRuntimeVersion { get; set; }
        public bool AutoUpgrade { get; set; }

        public SilverlightObject()
        {
            OnSilverlightError = "onSilverlightError";
            BackgroundColor = Color.Blue;
            MinimumRuntimeVersion = "3.0.40624.0";
            AutoUpgrade = true;
        }

    }
    public class UploadViewModel
    {
        public SilverlightObject UploadControl { get; set; }
    }

    public class JsonUploadModel
    {
        public SilverlightObject JsonUploadControl { get; set; }
    }
}