using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using MyBlog.Models.Users;

namespace MyBlog.Helpers
{
    public static class PagingHelpers
    {
        public static MvcHtmlString PageLinks(this HtmlHelper html,
            PageInfo pageInfo)
        {
            var result = new StringBuilder();
            const int displayPagesCount = 4;
                    
            var firstPage = DetectFirstPageOfPaginator(pageInfo, displayPagesCount);

            RenderHtmlTags(pageInfo, displayPagesCount, firstPage, result);

            return MvcHtmlString.Create(result.ToString());
        }

        private static void RenderHtmlTags(PageInfo pageInfo, int displayPagesCount, int currentPage, StringBuilder result)
        {
            for (int i = 0; i < displayPagesCount; i++)
            {
                var tag = new TagBuilder("button") {InnerHtml = currentPage.ToString()};

                if (currentPage == pageInfo.CurrentPage)
                {
                    tag.AddCssClass("selected");
                    tag.AddCssClass("btn-primary");
                }

                tag.AddCssClass("btn btn-default linkCurrentPageUsers");
                result.Append(tag);

                if (currentPage == pageInfo.TotalPages)
                {
                    break;
                }

                currentPage++;
            }
        }

        private static int DetectFirstPageOfPaginator(PageInfo pageInfo, int displayPagesCount)
        {
            int firstPage;
            int indxFirstPage;

            bool currentPageIsOnInterval = (pageInfo.CurrentPage % displayPagesCount) != 0;

            if (currentPageIsOnInterval)
            {
                indxFirstPage = (pageInfo.CurrentPage/displayPagesCount)*displayPagesCount;

                firstPage = (indxFirstPage != pageInfo.TotalPages) ? indxFirstPage + 1 : pageInfo.TotalPages;
            }
            else
            {
                indxFirstPage = (pageInfo.CurrentPage/displayPagesCount - 1)*displayPagesCount;

                firstPage = indxFirstPage + 1;
            }

            return firstPage;
        }
    }
}