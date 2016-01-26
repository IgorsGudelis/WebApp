using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BLL;
using BLL.Posts;

namespace MyBlog.Models.Posts
{
    public class IndexModel
    {
        public List<Post> Posts { get; set; } 
    }
}