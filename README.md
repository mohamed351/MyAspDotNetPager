# MyAspDotNetPager

## Razor Engine
The Two essential code  The deciration of Modal and The Pager itself 
```
@model IPager<MyEShopping.Models.Product>
 @Html.GetPager(new string[] { "pagination-sm", "no-margin", "pull-right" },Model,a=>Url.Action("Index",new { indexer =a  }),PagerHtmlHelpers.TypeOfTemplate.Bootstrap3)
 
 /*  orwithout aading a new class*/
   @Html.GetPager(Model, a => Url.Action("Index", new { indexer = a }), PagerHtmlHelpers.TypeOfTemplate.Bootstrap3)


```
## controller 
// this code The First Genric class you will added is Model or ViewModel That you want to sort  and The 2nd Genric class or 
Order you want to or by ID which is Int or Name Which is a string 
The First argument for This is DBContext , the 2nd is Where statement and the Last but not the Least The Index of the page 
and how many element in the page 
```
   public ActionResult Index(int indexer = 0,string search= "")
        {
            PagerController<Product, int> pager = new PagerController<Product, int>(db,
                a => a.ID, a => a.Name.Contains(search),
                indexer, 10);
          
            return View(pager);
        }
   ```



