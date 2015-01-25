using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrustructure.Blog
{
    public abstract class Blog
    {
        public int BlogID { get; set; }
        public DateTime BlogTime { get; set; }
        public string BlogAid { get; set; }
        public string BlogContent { get; set; }
        public string BlogTitle{ get; set; }
        public string BlogAuthor { get; set; }
        public string BlogNews { get; set; }
    }

    public class MicroBlog:Blog
    {

    }
}
