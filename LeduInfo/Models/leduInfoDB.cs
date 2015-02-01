using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;


namespace LeduInfo.Models
{
    public class PremiereDB:DbContext
    {
        public PremiereDB()
            : base("PremiereDB")
        {
            Database.CreateIfNotExists();
        }
        //public DbSet<OwnerInfo> OwnerInfotbl { get; set; }
        //public DbSet<RenterInfo> RenterInfotbl { get; set; }
        //public DbSet<District> Districtstbl { get; set; }
        //public DbSet<ImgPath> ImgPathstbl { get; set; }
        //public DbSet<VoteComments> VoteCommenttbl { get; set; }
        //public DbSet<ExternalUserInformation> ExternalUserstbl { get; set; }
        //public DbSet<JsonModel> JsonModeltbl { get; set; }
        //public DbSet<Brands> Brandstbl { get; set; }
        //public DbSet <LoginModel>LoginModeltbl { get; set; }
        //public DbSet<FileResource> FileResourcetbl { get; set; }
        //public DbSet<FileType> FileTypetbl { get; set; }
        //public DbSet<Blog> Blogtbl { get; set; }
        //public DbSet<BlogContent> BlogContenttbl { get; set; }
        //public DbSet<BlogTitle> BlogTitletbl { get; set; }
        //public DbSet<BlogAid> BlogAidtbl { get; set; }
        //public DbSet<BlogAuthor> BlogAuthortbl { get; set; }
        //public DbSet<BlogComment> BlogCommenttbl { get; set; }
        //public DbSet<BlogNews> BlogNewstbl { get; set; }
        //public DbSet<Tags> Tagstbl { get; set; }
        public DbSet<UploadHandlerModel> UploadHandlertbl { get; set; }

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