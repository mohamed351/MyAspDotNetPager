using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;

namespace WebApplication123.Helperss
{

    public static class PagerHtmlHelpers
    {
       
        public static IHtmlString GetPager(this HtmlHelper helper, IPagerComponent pager,Func<int,string> generateUrl)
        {
             
              
            TagBuilder tag = new TagBuilder("ul");
            var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);
            for (int i = 1; i <= pager.TotalOfPageBaseOnSearch; i++)
            {
                TagBuilder anchor = new TagBuilder("a");
                anchor.Attributes.Add("href", generateUrl.Invoke(i-1));
                anchor.InnerHtml = i.ToString();
                TagBuilder li = new TagBuilder("li");
                li.InnerHtml = anchor.ToString(TagRenderMode.Normal);
                tag.InnerHtml += li;

            }


            return new MvcHtmlString(tag.ToString(TagRenderMode.Normal));

        }


        public static IHtmlString GetPager(this HtmlHelper helper, IPagerComponent pager, Func<int, string> generateUrl,TypeOfTemplate typeOfTemplate = TypeOfTemplate.Bootstrap3)
        {
            switch (typeOfTemplate)
            {
                case TypeOfTemplate.AdminTLE:
                    return new MvcHtmlString(Bootstrap3Bulider(helper, pager, generateUrl).ToString(TagRenderMode.Normal));
                  
                case TypeOfTemplate.Bootstrap3:

                    return new MvcHtmlString( Bootstrap3Bulider(helper, pager, generateUrl).ToString(TagRenderMode.Normal));
                case TypeOfTemplate.Bootstrap4:
                    return new MvcHtmlString(Bootstrap3Bulider(helper, pager, generateUrl).ToString(TagRenderMode.Normal));
                default:
                    return new   MvcHtmlString(Bootstrap3Bulider(helper, pager, generateUrl).ToString(TagRenderMode.Normal));



            }

        

        }


        private static TagBuilder Bootstrap3Bulider(HtmlHelper helper , IPagerComponent pager, Func<int, string> generateUrl)
        {
            TagBuilder tag = new TagBuilder("ul");
            var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);
            for (int i = 0; i <= pager.TotalOfPageBaseOnSearch+1; i++)
            {
                if (i == 0)
                {
                    TagBuilder liper = new TagBuilder("li");
                    TagBuilder achorper = new TagBuilder("a");
                    TagBuilder span = new TagBuilder("span");
                    span.Attributes.Add("aria-hidden", "true");
                    span.InnerHtml = "&laquo;";
                    if (pager.Index == 0)
                    {
                        liper.AddCssClass("disabled");
                        achorper.Attributes.Add("href", "#");

                    }
                    else
                    {
                      
                        achorper.Attributes.Add("href", generateUrl.Invoke(pager.Index-1));

                    }
                    achorper.InnerHtml = span.ToString(TagRenderMode.Normal);
                    liper.InnerHtml = achorper.ToString(TagRenderMode.Normal);
                    tag.InnerHtml = liper.ToString(TagRenderMode.Normal);
                    continue;

                }
                if(i> pager.TotalOfPageBaseOnSearch)
                {
                    TagBuilder liper = new TagBuilder("li");
                    TagBuilder achorper = new TagBuilder("a");
                    TagBuilder span = new TagBuilder("span");
                    span.Attributes.Add("aria-hidden", "true");
                    span.InnerHtml = "&laquo;";
                    if (pager.Index == 0)
                    {
                        liper.AddCssClass("disabled");
                        achorper.Attributes.Add("href", "#");

                    }
                    else
                    {

                        achorper.Attributes.Add("href", generateUrl.Invoke(pager.Index - 1));

                    }
                    achorper.InnerHtml = span.ToString(TagRenderMode.Normal);
                    liper.InnerHtml = achorper.ToString(TagRenderMode.Normal);
                    tag.InnerHtml += liper.ToString(TagRenderMode.Normal);
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
            tag.AddCssClass("pagination");
            return tag;
        }
        public enum TypeOfTemplate
        {
            AdminTLE,
            Bootstrap3,
            Bootstrap4


        }


    }
  
    public static class PagerAjaxHelpers
    {
        public static IHtmlString GetPager (this AjaxHelper helper, AjaxOptions options, IPagerComponent pager, Func<int, string> generateUrl)
        {

            TagBuilder builder = new TagBuilder("ul");
           
          
        
            return new MvcHtmlString(builder.ToString(TagRenderMode.Normal));
        } 

    }

    public interface Ipager<T>: IPagerComponent

    {
     
        IEnumerable<T> ListofItems { get; set; }
    

    }
    public interface IPagerComponent
    {
        int Index { get; }
        int Size { get; }
        int NumberOfPage { get; }
        int TotalOfPage { get; }
        int TotalOfPageBaseOnSearch { get; set; }
    }

    

    public class PagerController<Tentity, Tkey> : Ipager<Tentity> where Tentity : class
    {
        private DbContext _context;
        public IEnumerable<Tentity> ListofItems { get; set; }
        private readonly Func<Tentity, Tkey> _functionOrderBy;
        private readonly Func<Tentity, bool> _functionWhere;

        public int NumberOfPage
        {
            get { return Index + 1; }

        }
        public int TotalOfPageBaseOnSearch { get; set; }
       

     


        public int Size { get; }
        public int Index { get; }
        public int TotalOfPage
        {
            get
            {
                return Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(_context.Set<Tentity>().Count()) / Convert.ToDecimal(Size)));
            }
        }

       

        public PagerController(DbContext context, Func<Tentity, Tkey> functionOrderby,Func<Tentity,bool> functionWhere, int Index, int size)
        {
            this._context = context;
            this._functionOrderBy = functionOrderby;
            this._functionWhere = functionWhere;
            this.Size = size;
            this.Index = Index;
            GetList();


        }

        private void GetList()
        {
           var items = _context.Set<Tentity>().Where(_functionWhere).OrderBy(_functionOrderBy)
                .Skip(Index * Size)
                .Take(Size).ToList();
            if(items == null)
            {
                ListofItems = new List<Tentity>();
            }
            else
            {
                ListofItems = items;
            }
            TotalOfPageBaseOnSearch = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(_context.Set<Tentity>().Where(_functionWhere).Count()) / Convert.ToDecimal(Size)));
        }













    }

}