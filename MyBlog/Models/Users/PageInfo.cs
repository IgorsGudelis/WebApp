using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBlog.Models.Users
{
    public class PageInfo
    {
        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        public string Find { get; set; }
    }
}