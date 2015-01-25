using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace LeduInfo.Models
{
    public class bloggingModel
    {

        [Key]
        public int BlogID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }

    public class VoteComments
    {
        [Key]
        public int CommentID { get; set; }
        public string Comments { get; set; }
        public bool ViewPoint { get; set; }
        public int UserID { get; set; }
    }

    public class Blog
    {
        [Key]
        public int BlogID { get; set; }
        public System.Nullable <DateTime> LastModified { get; set; }
        public System.Nullable<int> AidID { get; set; }
        public virtual BlogAid BlogAid{ get; set; }
        public System.Nullable<int> AuthorID { get; set; }
        public virtual BlogAuthor BlogAuthor { get; set; }
        public System.Nullable<int> CommentID { get; set; }
        public virtual ICollection<BlogComment> BlogComments { get; set; }
        public System.Nullable<int> TitleID { get; set; }
        public virtual BlogTitle BlogTitle { get; set; }
        public System.Nullable<int> ContentID { get; set; }
        public virtual BlogContent BlogContent { get; set; }
        public System.Nullable<int> NewsID { get; set; }
        public virtual BlogNews BlogNews { get; set; }

    }
    public class BlogAid
    {
        [Key]
        public int AidID { get; set; }
        public string Aids { get; set; }
    }
    public class BlogAuthor
    {
        [Key]
        public int AuthorID { get; set; }
        public virtual User UserID { get; set; }
        public string AuthorName { get; set; }
    }
    public class BlogComment
    {
        [Key]
        public int CommentID { get; set; }
        public string Comments { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime LastModifed { get; set; }
        public virtual Blog Blog { get; set; }
        public System.Nullable<int> BlogID { get; set; }
    }

    public class BlogType
    {
        [Key]
        public int BlogTypeID { get; set; }
        public string   BlogTypeName { get; set; }
    }
    public class BlogContent
    {
        [Key]
        public int ContentID { get; set; }
        public string Contents { get; set; }
    }
    public class BlogNews
    {
        [Key]
        public int NewsID { get; set; }
        public string NewsContent { get; set; }
    }
    public class BlogTitle
    {
        [Key]
        public int TitleID { get; set; }
        public string Titles { get; set; }
    }

    public class Tags
    {

        [Key]
        public int TagsID { get; set; }
        public int BlogID { get; set; }
        public string Tag { get; set; }
    }

    public class BlogPageModel
    {
        public Blog Blogs { get; set; }
        public BlogAid BlogAids { get; set; }
        public BlogAuthor BlogAuthors { get; set; }
        public BlogComment BlogComments { get; set; }
        public BlogContent BlogContents { get; set; }
        public BlogNews BlogNews { get; set; }
        public BlogTitle BlogTitles { get; set; }
        public Tags Tags { get; set; }
    }

   

}