# MyAspDotNetPager
##Razor Engine
```
@model IPager<MyEShopping.Models.Product>
 @Html.GetPager(new string[] { "pagination-sm", "no-margin", "pull-right" },Model,a=>Url.Action("Index",new { indexer =a  }),PagerHtmlHelpers.TypeOfTemplate.Bootstrap3)
 
 or /*without aading a new class*/
   @Html.GetPager(Model, a => Url.Action("Index", new { indexer = a }), PagerHtmlHelpers.TypeOfTemplate.Bootstrap3)


```
