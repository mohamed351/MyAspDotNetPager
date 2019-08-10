using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace PagerHelper
{
    public class PagerController<Tentity, Tkey> : IPager<Tentity> where Tentity : class
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



        public PagerController(DbContext context, Func<Tentity, Tkey> functionOrderby, Func<Tentity, bool> functionWhere, int Index, int size)
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
            if (items == null)
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
