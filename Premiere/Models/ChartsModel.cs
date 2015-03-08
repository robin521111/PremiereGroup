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

    [Table("Chartstbl")]
    public class ChartsModel
    {
        [Required]
        [Key]
        public int ChartID { get; set; }
        public string ChartName { get; set; }
        public string ChartType { get; set; }
    }


    [Table("BrandExposuretbl")]
    public class BrandExposure
    {
        [Key]
        public int ID { get; set; }
        public int ChartID { get; set; }
        public string Content { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime LastModified { get; set; }

    }
}