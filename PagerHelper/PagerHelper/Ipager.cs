using System.Collections.Generic;

namespace PagerHelper
{
    public interface IPager<T> : IPagerComponent
    {
        IEnumerable<T> ListofItems { get; set; }

    }
}
