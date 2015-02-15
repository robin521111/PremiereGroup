using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;


namespace Premiere.Models
{
    public class PremiereDB:DbContext
    {
        public PremiereDB()
            : base("PremiereDB")
        {
            Database.CreateIfNotExists();
        }

        public DbSet<UploadHandlerModel> UploadHandlertbl { get; set; }
        public DbSet<ChartsTypeModel> ChartsTypetbl { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        
        
    }

    public class AccountDB : DbContext
    {
        public AccountDB()
            : base("DefaultConnection")
        {
            
        }

      

    }
}