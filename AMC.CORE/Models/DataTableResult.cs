using System.Collections.Generic;

namespace AMC.CORE.Models
{
    public class DataTableResult<T>
    {
        public IEnumerable<T> Items;
        public int Length;

        public DataTableResult()
        {
        }

        public DataTableResult(IEnumerable<T> items, int length)
        {
            Items = items;
            Length = length;
        }
    }
}
