using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using PagedList;
using System;
using System.Text;

namespace AdvanceCRM.Web.Helpers
{
    public enum PagedListDisplayMode
    {
        Always,
        IfNeeded,
        Never
    }

    public class PagedListRenderOptions
    {
        public PagedListDisplayMode Display { get; set; } = PagedListDisplayMode.Always;
        public bool DisplayPageCountAndCurrentLocation { get; set; }
        public bool DisplayItemSliceAndTotal { get; set; }
    }

    public static class PagedListExtensions
    {
        public static IHtmlContent PagedListPager<T>(this IHtmlHelper html, IPagedList<T> pagedList, Func<int, string> generatePageUrl, PagedListRenderOptions options)
        {
            if (pagedList == null)
                throw new ArgumentNullException(nameof(pagedList));
            if (generatePageUrl == null)
                throw new ArgumentNullException(nameof(generatePageUrl));

            if (options?.Display == PagedListDisplayMode.Never ||
                (options?.Display == PagedListDisplayMode.IfNeeded && pagedList.PageCount <= 1))
            {
                return HtmlString.Empty;
            }

            var sb = new StringBuilder();
            sb.Append("<ul class=\"pagination\">");
            for (int i = 1; i <= pagedList.PageCount; i++)
            {
                var url = generatePageUrl(i);
                var active = i == pagedList.PageNumber ? " class=\"active\"" : string.Empty;
                sb.AppendFormat("<li{2}><a href=\"{0}\">{1}</a></li>", url, i, active);
            }
            sb.Append("</ul>");

            if (options?.DisplayPageCountAndCurrentLocation == true)
            {
                sb.AppendFormat("<span class=\"pagedlist-pagecount\">Page {0} of {1}</span>", pagedList.PageNumber, pagedList.PageCount);
            }

            if (options?.DisplayItemSliceAndTotal == true)
            {
                sb.AppendFormat("<span class=\"pagedlist-itemscount\">({0} - {1} of {2})</span>",
                    pagedList.FirstItemOnPage, pagedList.LastItemOnPage, pagedList.TotalItemCount);
            }

            return new HtmlString(sb.ToString());
        }
    }
}

