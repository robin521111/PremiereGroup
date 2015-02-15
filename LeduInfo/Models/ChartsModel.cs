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
    public class ChartsTypeModel
    {
        [Key]
        public int ChartTypeID { get; set; }

        public string ChartTypeName { get; set; }

        public string ChartTypePath { get; set; }

    }
}