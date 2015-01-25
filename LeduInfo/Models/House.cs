using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;
namespace LeduInfo.Models
{
    
        public class HouseInfo
        {
            [Key]
            public int houseID { get; set; }
            public string Address { get; set; }
            public int ownerID { get; set; }
            public int renterID { get; set; }
            public bool rented { get; set; }
            public virtual HouseStyle HouseStyle { get; set; }
            public int houseStyleID { get; set; }
        }
        public class OwnerInfo
        {   
            [Key]
            public int ownerID { get; set; }
            public string ownerName { get; set; }
            
            public long phoneNumber { get; set; }
            public int DistrictID { get; set; }
        }

        public class RenterInfo
        {
            [Key]
            public int renterID { get; set; }
            public string renterName { get; set; }
            public long renterPhoneNumber { get; set; }
        }

        public class HouseStyle
        {
            [Key]
            public int houseStyleID { get; set; }
            public string houseStyle { get; set; }
        }

       public class ImgResource
        {
           [Key]
           public int imgID { get; set; }
           public string imgPath { get; set; }
           public int spanNum { get; set; }
           public string AltMsg { get; set; }
           public string Description { get; set; }
           public string smallDes { get; set; }
           public string warpwordsTitle { get; set; }
           public string warpContent { get; set; }
           public int houseID { get; set; }
           public int houseStyleID { get; set; }
           public virtual HouseInfo HouseInfo { get; set; }
           public virtual HouseStyle HouseStyle { get; set;}
        }

       public class FileResource
       {
           [Key]
           public int ID { get; set; }
           public string Name { get; set; }
           public string Path { get; set; }
           public int FileTypeID { get; set; }
           public virtual FileType FileType { get; set; }

       }

       public class FileType
       {
           [Key]
           public int FileTypeID { get; set; }
           public string fileType { get; set; }
       }

       public class District
       {
           [Key]
           public int DistrictID { get; set; }
           public string DistructName { get; set; }
       }

       public class ImgPath
       {
           [Key]
           public int pathID { get; set; }
           public string Path { get; set; }
       }

      


}