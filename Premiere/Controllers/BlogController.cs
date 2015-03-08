using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data;
using LeduInfo.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Text;
using System.Web.Security;

namespace LeduInfo.Controllers
{
    public class BlogController : Controller
    {
        
        // GET: /Blog/
        leduInfoDB DB = new leduInfoDB();
        public ActionResult Index()
        {
                List<BlogPageModel> modelList = new List<BlogPageModel>();
                List<BlogTitle> titles = new List<Models.BlogTitle>();
                BlogPageModel blogpagemodel = new BlogPageModel();
            if (Request.IsAuthenticated)
            {
               
                //modelList.Add(new BlogPageModel{Blogs = from p in DB.Blogtbl where p.BlogID>0 ,BlogTitles=from t in DB.BlogTitletbl.ToList()});

                IQueryable<BlogPageModel> blogmodellist = (from b in DB.Blogtbl
                                                          join t in DB.BlogTitletbl on b.TitleID equals t.TitleID
                                                          join c in DB.BlogContenttbl on b.ContentID equals c.ContentID
                                                          join a in DB.BlogAuthortbl on b.AuthorID equals a.AuthorID
                                                          where b.BlogID > 0 && t.Titles != null
                                                          select new BlogPageModel { BlogTitles = t, BlogContents = c, Blogs = b, BlogAuthors = a }).OrderByDescending(m=>m.Blogs.LastModified);

                blogpagemodel.Blogs = new Blog();
                blogpagemodel.BlogTitles = new Models.BlogTitle();
                blogpagemodel.BlogContents = new BlogContent();
                blogpagemodel.BlogAuthors = new BlogAuthor();
                blogpagemodel.BlogAuthors.AuthorName = Membership.GetUser().UserName;
                //IEnumerable<BlogPageModel> titleList = blogPageModelList.TakeWhile(m => m.Title != null);


                foreach (var item in blogmodellist)
                {
                    //blogpagemodel.BlogComments.CommentID = (int)item.cid;
                    blogpagemodel.Blogs.BlogID = item.Blogs.BlogID;
                    blogpagemodel.BlogTitles.Titles = item.BlogTitles.Titles;
                    blogpagemodel.BlogContents.Contents = item.BlogContents.Contents;
                    blogpagemodel.BlogAuthors.AuthorName = item.BlogAuthors.AuthorName;
                    modelList.Add(blogpagemodel);
                }
                return View(blogmodellist.ToList());
            }
            else
            {

                RedirectToAction("Index", "Home", null);
            }
           

            //var titlesList = blogPageModelList
            //        .Select(m => m.Title)
            //        .ToList();
                    
            //modelList.AddRange(blogPageModelList.ToList());
            return RedirectToAction("Index", "Home", null);
        }

        public ActionResult BlogHome()
        {
            List<BlogPageModel> modelList = new List<BlogPageModel>();
            List<BlogTitle> titles = new List<Models.BlogTitle>();
            BlogPageModel blogpagemodel = new BlogPageModel();
            //modelList.Add(new BlogPageModel{Blogs = from p in DB.Blogtbl where p.BlogID>0 ,BlogTitles=from t in DB.BlogTitletbl.ToList()});

            IQueryable<BlogPageModel> blogmodellist = from b in DB.Blogtbl
                                                      join t in DB.BlogTitletbl on b.TitleID equals t.TitleID
                                                      join c in DB.BlogContenttbl on b.ContentID equals c.ContentID
                                                      join a in DB.BlogAuthortbl on b.AuthorID equals a.AuthorID
                                                      //join m in DB.BlogCommenttbl on b.CommentID equals m.CommentID
                                                      where b.BlogID > 0 && t.Titles != null 
                                                      select new BlogPageModel { BlogTitles = t, BlogContents = c, Blogs = b, BlogAuthors=a };

            blogpagemodel.Blogs = new Blog();
            blogpagemodel.BlogTitles = new BlogTitle();
            blogpagemodel.BlogContents = new BlogContent();
            blogpagemodel.BlogAuthors = new BlogAuthor();
            foreach (var item in blogmodellist)
            {
                blogpagemodel.Blogs.BlogID = item.Blogs.BlogID;
                blogpagemodel.BlogTitles.Titles = item.BlogTitles.Titles;
                blogpagemodel.BlogContents.Contents = item.BlogContents.Contents;
                if (item.BlogAuthors !=null)
                {
                blogpagemodel.BlogAuthors.AuthorName = item.BlogAuthors.AuthorName;
                }
                modelList.Add(blogpagemodel);
            }

            //var titlesList = blogPageModelList
            //        .Select(m => m.Title)
            //        .ToList();

            //modelList.AddRange(blogPageModelList.ToList());
            return View(blogmodellist.ToList());
        }

        public string BlogQuotation(int id)
        {
            StringBuilder sb = new StringBuilder();
            var Quatation = from p in DB.Blogtbl
                            join c in DB.BlogContenttbl on p.ContentID equals c.ContentID
                            where p.BlogID == id
                            select c.Contents;

           string temp= Quatation.First();

           //var img = from i in temp
           //          where i.ToString().Contains("<img")
           //          select i;
            
           //string img_str = img.ToString();
           if (temp.Contains("<img"))
           {
               int place = temp.IndexOf("<img");
               int endplace= temp.LastIndexOf("/>");
               string imgPath=  temp.Substring(place, endplace - place);
               imgPath += "class='img-responsive' />";
               ViewBag.imagePath = imgPath;
           }
            sb.Append("<p>");
            sb.Append(temp +"</p>");
            if (sb.Capacity>300)
            {
                sb.Remove(300,sb.Capacity-300);
            }

            return sb.AppendLine("....").ToString();
        }

  
        public ActionResult BlogPost(BlogPageModel blogData)
        {
            
            if (TempData["blog"] != null)
            {
                blogData = (BlogPageModel)TempData["blog"];
                blogData.Blogs.LastModified = DateTime.Now;
                
            }
            return View(blogData);
        }
        [HttpGet]
        public ActionResult BlogPost(int id)
        {
            BlogPageModel bpm = new BlogPageModel();

            if (TempData["blog"]!=null)
            {
                bpm = (BlogPageModel)TempData["blog"];
            }


            if (bpm.BlogContents ==null)
            {
                var blog = from b in DB.Blogtbl
                           join t in DB.BlogTitletbl on b.TitleID equals t.TitleID
                           join c in DB.BlogContenttbl on b.ContentID equals c.ContentID
                           where b.BlogID == id
                           select new { Title = b.BlogTitle, Content = c.Contents};


                bpm.BlogContents = new BlogContent();
                bpm.Tags = new Tags();
                bpm.BlogTitles = blog.FirstOrDefault().Title;
                bpm.BlogContents.Contents = blog.FirstOrDefault().Content;

                string content= bpm.BlogContents.Contents.ToString();
                if (content.Contains("<img"))
                {
                    int place = content.IndexOf("<img");
                    int endplace = content.LastIndexOf("/>");
                    string imgPath = content.Substring(place,endplace - place);
                    imgPath+= "class='img-responsive '/>";
                    ViewBag.IMG = imgPath;
                }
                if (bpm.Tags.Tag ==null)
                {
                    var tags = from t in DB.Tagstbl
                               where t.BlogID == id
                               select t.Tag;
                    bpm.Tags.Tag = tags.FirstOrDefault();
                }
            }

            var comments = from c in DB.BlogCommenttbl
                           join b in DB.Blogtbl on c.BlogID equals b.BlogID
                           where b.BlogID == id
                           select new { Comments = c.Comments, LastModified=c.LastModifed};
                bpm.Blogs = new Blog();
                bpm.Blogs.BlogComments = new List<BlogComment>();
            if (comments.Any())
            {
                foreach (var item in comments.ToList())
                {
                    bpm.Blogs.BlogComments.Add(new BlogComment{Comments=item.Comments, LastModifed=item.LastModified});
                }
            }
            return View(bpm);
        }

        public ActionResult BLogCreate()
        {
            return View();
        }

        [HttpGet]
        public ActionResult BlogCommentsLoad()
        {
            var commentList = from c in DB.BlogCommenttbl
                              where c.Comments != null 
                              select c;

            var lists= commentList.ToList();
            return View(lists);
        }
        [HttpPost]
        public ActionResult BlogCommentsCommit(string message)
        {
            string s = message;
            //string obj_str = JsonConvert.SerializeObject(comment_pkg);
            //JObject o = (JObject)obj_str;
            //string comments = (string)o.SelectToken("message");
            //DateTime date = (DateTime)o.SelectToken("date");
            //JsonTextReader reader = new JsonTextReader(new StringReader(obj));
            //var item=from m in obj[""]
            //         where m
            string id = Url.RequestContext.RouteData.Values["id"].ToString();

            int cid = int.Parse(id);
            DB.BlogCommenttbl.Add(new BlogComment { Comments = message, LastModifed=DateTime.Now,BlogID=cid});
            
            DB.SaveChanges();
            //while (reader.Read())
            //{
            //    if (reader.Value!=null)
            //    {
            //        DB.BlogCommenttbl.Add(new BlogComment{Comments=})
            //    }
            //}
            return View();
        }
        //[HttpPost]
        //public ActionResult BlogTagSave(string tags)
        //{
        //    char[] cones={','};
        //    string[] tagValue = tags.Split(cones, 90);
        //    RedirectToAction("BlogSubmit");
        //    BlogPageModel bpm=null;
        //    bpm = (BlogPageModel)TempData["blog"];
        //    foreach (var item in tagValue)
        //    {
        //        DB.Tagstbl.Add(new Tags { Tag = item });
        //    }
        //    DB.SaveChanges();
        //    return View();
        //}
        public string BlogTitle(int id)
        {
            var blogTitle = from b in DB.Blogtbl
                            join t in DB.BlogTitletbl on b.TitleID equals t.TitleID
                            where b.BlogID == id 
                            select t.Titles;
            return blogTitle.ToString();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult BlogSubmit(BlogPageModel blogData)
        {

            DB.BlogContenttbl.Add(new BlogContent { Contents = blogData.BlogContents.Contents });
            DB.BlogTitletbl.Add(new BlogTitle { Titles = blogData.BlogTitles.Titles });
            string name = Membership.GetUser().UserName;


            var authorName = from a in DB.BlogAuthortbl
                             where a.AuthorName == name
                             select a.AuthorName;


            DB.BlogAuthortbl.Add(new BlogAuthor { AuthorName = Membership.GetUser().UserName });
            // DB.Blogtbl.Add(new Blog { LastModified = DateTime.Now, BlogContent = blogData.BlogContents, BlogTitle = blogData.BlogTitles });
            DB.SaveChanges();
                 
            blogData.BlogAuthors.AuthorName = Membership.GetUser().UserName;

            var blogTitleID = from t in DB.BlogTitletbl
                              where t.Titles == blogData.BlogTitles.Titles
                              select t.TitleID;
            var blogContentID = from c in DB.BlogContenttbl
                                where c.Contents == blogData.BlogContents.Contents
                                select c.ContentID;
            var blogAuthorID = from a in DB.BlogAuthortbl
                               where a.AuthorName == blogData.BlogAuthors.AuthorName
                               select a.AuthorID;

            int blogTitleIDToInt = Convert.ToInt32(blogTitleID.First());
            int blogContentIDToInt = Convert.ToInt32(blogContentID.First());
            int blogAuthorIDToInt = Convert.ToInt32(blogAuthorID.First());

            DB.Blogtbl.Add(new Blog { TitleID = blogTitleIDToInt, ContentID = blogContentIDToInt, AuthorID = blogAuthorIDToInt, LastModified = DateTime.Now });


            DB.BlogAuthortbl.Add(new BlogAuthor { AuthorName = Membership.GetUser().UserName });
            // DB.Blogtbl.Add(new Blog { LastModified = DateTime.Now, BlogContent = blogData.BlogContents, BlogTitle = blogData.BlogTitles });
            DB.SaveChanges();

            var blogID = from b in DB.Blogtbl
                         join t in DB.BlogTitletbl on b.TitleID equals blogTitleIDToInt
                         join a in DB.BlogAuthortbl on b.AuthorID equals blogAuthorIDToInt
                         join o in DB.BlogContenttbl on b.ContentID equals blogContentIDToInt
                         where b.BlogID != null
                         select b.BlogID;

            blogData.Blogs.BlogID = Convert.ToInt32(blogID.First());
            blogData.BlogAuthors.AuthorName = Membership.GetUser().UserName;
            DB.Tagstbl.Add(new Tags { Tag = blogData.Tags.Tag, BlogID = blogData.Blogs.BlogID });
            DB.SaveChanges();
            TempData["blog"] = blogData;
            return RedirectToAction("BlogPost", new { @id = blogData.Blogs.BlogID });

        }

        

        public ActionResult BlogSqlBulk()
        {

            string connectionString = @"Server=localhost\sqlexpress;DataBase=AdventureWorks2012;Trusted_Connection=true";
            string targetConnectionString = @"Server=localhost\sqlexpress;DataBase=LeduInfoDB;Trusted_Connection=true";

            using (SqlConnection sourceData = new SqlConnection(connectionString))
            {
                DataTable dt = new DataTable();
                SqlCommand myCommand = new SqlCommand("SELECT * FROM [AdventureWorks2012].[HumanResources].[Employee]", sourceData);
                sourceData.Open();
                SqlDataReader reader = myCommand.ExecuteReader();

                using (SqlConnection destinationConnection = new SqlConnection(targetConnectionString))
                {
                    destinationConnection.Open();

                    using (SqlBulkCopy blogSQLbuldCopy = new SqlBulkCopy(destinationConnection))
                    {
                        blogSQLbuldCopy.DestinationTableName = "Users";
                        blogSQLbuldCopy.ColumnMappings.Add("BusinessEntityID", "UserID");
                        blogSQLbuldCopy.ColumnMappings.Add("LoginID", "UserName");
                        //blogSQLbuldCopy.ColumnMappings.Add("CurrentFlag", "Active");
                        //BusinessEntityIDblogSQLbuldCopy.ColumnMappings.Add("ModifiedDate", "Created");
                        blogSQLbuldCopy.WriteToServer(reader);
                    }
                }
                reader.Close();
            }
            return View();
        }

        public ActionResult DeleteBlog(int bid)
        {
            var deleteItem = from b in DB.Blogtbl
                             where b.BlogID == bid
                             select b;
            DB.Database.ExecuteSqlCommand("EXEC sp_DeleteBlogbyID @bid", new SqlParameter("@bid", bid));
            DB.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult TravelModel(string style)
        {       
            switch (style)
            {
                case "grayscale":
                    return RedirectToAction("GrayScale");

                   // add for some more style
            }
            return View();
        }

#region Blog Style list

       public ActionResult GrayScale()
      {
          return View();
      }

#endregion


    }
}
