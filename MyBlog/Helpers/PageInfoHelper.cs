using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyBlog.Models.Users;

namespace MyBlog.Helpers
{

    public static class PageInfoHelper
    {
        public static PageInfo SetPageInfo(int totalPages, int currentPage = 1)
        {
            var pageInfo = new PageInfo()
            {
                TotalPages = totalPages,
                CurrentPage = currentPage
            };

            return pageInfo;
        }
    }
}