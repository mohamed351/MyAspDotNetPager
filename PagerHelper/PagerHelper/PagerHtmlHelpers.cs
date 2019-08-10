using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PagerHelper
{
    public static class PagerHtmlHelpers
    {

        public static IHtmlString GetPager(this HtmlHelper helper, IPagerComponent pager, Func<int, string> generateUrl)
        {


            TagBuilder tag = new TagBuilder("ul");
            var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);
            for (int i = 1; i <= pager.TotalOfPageBaseOnSearch; i++)
            {
                TagBuilder anchor = new TagBuilder("a");
                anchor.Attributes.Add("href", generateUrl.Invoke(i - 1));
                anchor.InnerHtml = i.ToString();
                TagBuilder li = new TagBuilder("li");
                li.InnerHtml = anchor.ToString(TagRenderMode.Normal);
                tag.InnerHtml += li;

            }


            return new MvcHtmlString(tag.ToString(TagRenderMode.Normal));

        }


        public static IHtmlString GetPager(this HtmlHelper helper, IPagerComponent pager, Func<int, string> generateUrl, TypeOfTemplate typeOfTemplate = TypeOfTemplate.Bootstrap3)
        {
            switch (typeOfTemplate)
            {

                case TypeOfTemplate.Bootstrap3:
                    return new MvcHtmlString(Bootstrap3Bulider(helper, pager, generateUrl).ToString(TagRenderMode.Normal));
                case TypeOfTemplate.Bootstrap4:
                    return new MvcHtmlString(Bootstrap3Bulider(helper, pager, generateUrl).ToString(TagRenderMode.Normal));
                default:
                    return new MvcHtmlString(Bootstrap3Bulider(helper, pager, generateUrl).ToString(TagRenderMode.Normal));



            }



        }

        public static IHtmlString GetPager(this HtmlHelper helper, string[] strclasses, IPagerComponent pager, Func<int, string> generateUrl, TypeOfTemplate typeOfTemplate = TypeOfTemplate.Bootstrap3)
        {
            switch (typeOfTemplate)
            {

                case TypeOfTemplate.Bootstrap3:
                    return new MvcHtmlString(Bootstrap3Bulider(helper, strclasses, pager, generateUrl).ToString(TagRenderMode.Normal));
                case TypeOfTemplate.Bootstrap4:
                    return new MvcHtmlString(Bootstrap3Bulider(helper, strclasses, pager, generateUrl).ToString(TagRenderMode.Normal));
                default:
                    return new MvcHtmlString(Bootstrap3Bulider(helper, strclasses, pager, generateUrl).ToString(TagRenderMode.Normal));



            }



        }


        private static TagBuilder Bootstrap3Bulider(HtmlHelper helper, IPagerComponent pager, Func<int, string> generateUrl)
        {
            TagBuilder tag = new TagBuilder("ul");
            var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);
            tag.InnerHtml += HelperPev(pager, generateUrl);
            for (int i = pager.Index; i <= pager.Index + 5; i++)
            {
                if (i <= 0)
                {
                    continue;
                }
                if (i > pager.TotalOfPageBaseOnSearch)
                {
                    continue;
                }
                TagBuilder anchor = new TagBuilder("a");
                anchor.Attributes.Add("href", generateUrl.Invoke(i - 1));

                anchor.InnerHtml = i.ToString();

                TagBuilder li = new TagBuilder("li");

                if (pager.NumberOfPage == i)
                {
                    li.AddCssClass("active");
                }

                li.InnerHtml = anchor.ToString(TagRenderMode.Normal);
                tag.InnerHtml += li;

            }
            tag.InnerHtml += HelperNext(pager, generateUrl);
            tag.AddCssClass("pagination");
            return tag;
        }
        private static TagBuilder Bootstrap3Bulider(HtmlHelper helper, string[] strclasses, IPagerComponent pager, Func<int, string> generateUrl)
        {
            TagBuilder tag = new TagBuilder("ul");
            var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);
            tag.InnerHtml += HelperPev(pager, generateUrl);
            for (int i = pager.Index; i <= pager.Index + 5; i++)
            {
                if (i <= 0)
                {
                    continue;
                }
                if (i > pager.TotalOfPageBaseOnSearch)
                {
                    continue;
                }
                TagBuilder anchor = new TagBuilder("a");
                anchor.Attributes.Add("href", generateUrl.Invoke(i - 1));

                anchor.InnerHtml = i.ToString();

                TagBuilder li = new TagBuilder("li");

                if (pager.NumberOfPage == i)
                {
                    li.AddCssClass("active");
                }

                li.InnerHtml = anchor.ToString(TagRenderMode.Normal);
                tag.InnerHtml += li;

            }
            tag.InnerHtml += HelperNext(pager, generateUrl);
            tag.AddCssClass("pagination");
            foreach (var item in strclasses)
            {
                tag.AddCssClass(item);
            }
            return tag;
        }
        private static TagBuilder HelperPev(IPagerComponent pager, Func<int, string> generateUrl)
        {


            TagBuilder liper = new TagBuilder("li");
            if (pager.Index != 0)
            {
                TagBuilder achorper = new TagBuilder("a");
                TagBuilder span = new TagBuilder("span");
                span.Attributes.Add("aria-hidden", "true");
                span.InnerHtml = "&laquo;";
                achorper.Attributes.Add("href", generateUrl.Invoke(pager.Index - 1));
                achorper.InnerHtml = span.ToString(TagRenderMode.Normal);
                liper.InnerHtml = achorper.ToString(TagRenderMode.Normal);
            }
            return liper;


        }
        private static TagBuilder HelperNext(IPagerComponent pager, Func<int, string> generateUrl)

        {
            TagBuilder liper = new TagBuilder("li");
            if (pager.Index + 1 < pager.TotalOfPageBaseOnSearch)
            {
                TagBuilder achorper = new TagBuilder("a");
                TagBuilder span = new TagBuilder("span");
                span.Attributes.Add("aria-hidden", "true");
                span.InnerHtml = "&raquo;";
                achorper.Attributes.Add("href", generateUrl.Invoke(pager.Index + 1));
                achorper.InnerHtml = span.ToString(TagRenderMode.Normal);
                liper.InnerHtml = achorper.ToString(TagRenderMode.Normal);
            }
            return liper;
        }
        public enum TypeOfTemplate
        {

            Bootstrap3,
            Bootstrap4


        }
    }
}
