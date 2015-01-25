using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace LeduInfo.Models
{
    public class DAL
    {

        public class LeduInfoDBInitializer : CreateDatabaseIfNotExists<PremiereDB>
        {
            public LeduInfoDBInitializer()
            {
                using (var db = new PremiereDB())
                {
                    
                    //db.OwnerInfotbl.Add(ownerName);
                   // db.SaveChanges();
                }

            }



        }
    }
}