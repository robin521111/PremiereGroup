using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Premiere.Models
{
    public class DAL
    {

        public class PremiereDBInitializer : CreateDatabaseIfNotExists<PremiereDB>
        {
            public PremiereDBInitializer()
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