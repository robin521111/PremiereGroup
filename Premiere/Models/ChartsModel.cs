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


    [Table("BrandExposureLinetbl")]
    public class BrandExposureLine
    {
        [Key]
        public int ID { get; set; }

        public int ChartID { get; set; }
        public string BrandName { get; set; }
        public string Series { get; set; }
        public string xAxis { get; set; }
        public int Month { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime LastModified { get; set; }

    }

    [Table("BrandExposureBubbletbl")]
    public class BrandExposureBubble
    {
        [Key]
        public int ID { get; set; }
        public int ChartID { get; set; }
        public string BrandName { get; set; }
        public string Series { get; set; }
        public string Month { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime LastModified { get; set; }
    }
    [Table("BrandFocustbl")]
    public class BrandFocus
    {
        [Key]
        public int ID { get; set; }
        public int ChartID { get; set; }
        public string BrandName { get; set; }
        public string Series { get; set; }
        public string xAxis { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime LastModified { get; set; }
    }

    [Table("BrandImageBlogtbl")]
    public class BrandImageBlog
    {
        [Key]
        public int ID { get; set; }
        public int ChartID { get; set; }
        public string BrandName { get; set; }
        public string Series { get; set; }
        public string xAxis { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime LastModified { get; set; }

    }

    [Table("BrandImageNewstbl")]
    public class BrandImageNews
    {
        [Key]
        public int ID { get; set; }
        public int ChartID { get; set; }
        public string BrandName { get; set; }
        public string Series { get; set; }
        public string xAxis { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime LastModified { get; set; }

    }
    [Table("BrandSpreadMapBlogtbl")]
    public class BrandSpreadMapBlog
    {
        [Key]
        public int ID { get; set; }
        public int ChartID { get; set; }
        public string BrandName { get; set; }
        public int Period { get; set; }
        public string Content { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime LastModified { get; set; }
    }

    [Table("BrandSpreadMapNewstbl")]
    public class BrandSpreadMapNews
    {
        [Key]
        public int ID { get; set; }
        public int ChartID { get; set; }
        public string BrandName { get; set; }
        public int Period { get; set; }
        public string Content { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime LastModified { get; set; }
    }



    [Table("MediaFocusMaptbl")]
    public class MediaFocusMap
    {
        [Key]
        public int ID { get; set; }
        public string BrandName { get; set; }
        public string Content { get; set; }
        public int Period { get; set; }

        public string LastModifiedBy { get; set; }
        public DateTime LastModified { get; set; }
    }
    [Table("DesignSensetbl")]
    public class DesignSense
    {
        [Key]
        public int ID { get; set; }
        public int ChartID { get; set; }
        public string BrandName { get; set; }
        public string Series { get; set; }
        public string xAxis { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime LastModified { get; set; }
    }

    [Table("SexRatiotbl")]
    public class SexRatio
    {
        [Key]
        public int ID { get; set; }
        public int ChartID { get; set; }
        public string BrandName { get; set; }
        public string Series { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime LastModifed { get; set; }
    }

    //[Table("BrandExposureMaptbl")]
    //public class BrandExposureMap
    //{
    //    [Key]
    //    public int ID { get; set; }
    //    public int ChartID { get; set; }
    //    public string BrandName { get; set; }
    //    public string Property { get; set; }
    //    public string Feature { get; set; }
    //    public int Ratio { get; set; }
    //    public int Value { get; set; }
    //    public int Value_R { get; set; }
    //    public string Period { get; set; }
    //    public string LastModifiedBy { get; set; }
    //    public DateTime LastModified { get; set; }
    //}



   

}