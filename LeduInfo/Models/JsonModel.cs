using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Security;

namespace LeduInfo.Models
{
    public class JsonModel
    {
        [Key]
        [Required]
        public int ID { get; set; }
        
        [Required]
        [DataType(DataType.Date)]
        public string Time { get; set; }        

        [Required]
        [DataType(DataType.Text)]
        public int Value { get; set; }

        [Required]
        public int BrandsID { get; set; }
    }

    public class Brands
    {
        [Key]
        public int BrandsID { get; set; }

        [Required]
        public string BrandsName { get; set; }
    }
}