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
            //Database.Initialize(true);
        }

        public DbSet<UploadHandlerModel> UploadHandlertbl { get; set; }
        public DbSet<ChartsModel> Chartstbl { get; set; }
        public DbSet<BrandExposureLine> BrandExposureLinetbl { get; set; }
        public DbSet<BrandExposureBubble> BrandExposureBubbletbl { get; set; }
        public DbSet<BrandFocus> BrandFocustbl { get; set; }
        public DbSet<BrandImage> BrandImagetbl { get; set; }
        public DbSet<DesignSense> DesignSensetbl { get; set; }
        public DbSet<SexRatio> SexRatiotbl { get; set; }
        public DbSet<BrandSpreadMap> BrandSpreadMaptbl { get; set; }

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