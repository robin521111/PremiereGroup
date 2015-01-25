using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.IO;
using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
using ImageProcessor;
using System.Web;


namespace Infrustructure
{
    public abstract class StorageFile
    {
        public string Name { get; set; }
        public Guid Guid { get; set; }
        public abstract List<string> GetFileNames();
        public abstract List<string> GetFilePaths();
        
    }

    
    public class Image:StorageFile
    {

        public string FilePath { get; set; }

        public List<string> GetFilePaths(string path )
        {
            List<string> paths = new List<string>();
            DirectoryInfo dirc = new DirectoryInfo(path);
            foreach (var item in dirc.GetFiles())
            {
                paths.Add(item.FullName.ToString());
            }
            return paths;
        }

        public override List<string> GetFileNames()
        {
            return null;
        }

        public override List<string> GetFilePaths()
        {
            return null;
        }

        public List<string> GetFileNames(string path)
        {
            List<string> names = new List<string>();
            DirectoryInfo dirc = new DirectoryInfo(path);
            foreach (var item in dirc.GetFiles())
            {
                names.Add(item.Name.ToString());
            }
            return names;
        }



        /// <summary>
        /// Thumbnail image to specific path
        /// </summary>
        /// <param name="inputImagePath"></param>
        public void MakeThumbnail(string inputImagePath)
        {
            string outputImagePath = System.Web.HttpContext.Current.Server.MapPath("~/Images/ImgFolder/ResourceImg/");
            byte[] photoBytes = File.ReadAllBytes(inputImagePath);
            int quality = 70;
            ImageFormat formate = ImageFormat.Jpeg;
            Size size = new Size(700, 450);

            using (var inStream = new MemoryStream(photoBytes))
            {
                using (var outStream = new MemoryStream())
                {
                    using (ImageFactory imageFactory = new ImageFactory())
                    {
                        imageFactory.Load(inStream)
                                    .Resize(size)
                                    .Format(formate)
                                    .Quality(quality)
                                    .Save(outStream);

                        imageFactory.Load(outStream).Save(outputImagePath);
                    }


                }


            }
                
            
        }

        

        public string MakeThumbnialReturnPath(string inputImagePath)
        {
            string outputImagePath = ConfigurationManager.AppSettings["ImgThumbnailPath"].ToString();
            byte[] photoBytes = File.ReadAllBytes(inputImagePath);
            int quality = 70;
            ImageFormat formate = ImageFormat.Jpeg;
            Size size = new Size(1200, 300);

            using (var inStream = new MemoryStream(photoBytes))
            {
                using (var outStream = new MemoryStream())
                {
                    using (ImageFactory imageFactory = new ImageFactory())
                    {
                        imageFactory.Load(inStream)
                                    .Resize(size)
                                    .Format(formate)
                                    .Quality(quality)
                                    .Save(outStream);

                        imageFactory.Load(outStream).Save(outputImagePath);
                    }



                }


            }


            return outputImagePath;
        }

        

        

                
    }

    public class Media:StorageFile
    {
        public override List<string> GetFileNames()
        {
            return null;
        }

        public override List<string> GetFilePaths()
        {
            throw new NotImplementedException();
        }
    }


}
