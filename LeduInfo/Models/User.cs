using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.Design;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
namespace LeduInfo.Models
{
    public class User
    {
        [Key]
        [ForeignKey("BlogAuthor")]
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string NickName { get; set; }
        public DateTime  Cretaed { get; set; }
        public bool Active { get; set; }
        public virtual BlogAuthor BlogAuthor { get; set; }
    }
 
}