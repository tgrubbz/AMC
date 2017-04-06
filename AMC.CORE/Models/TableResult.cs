using System.Collections.Generic;

namespace AMC.CORE.Models
{
    public class TableResult<T>
    {
        public IEnumerable<T> Items;
        public int Count;

        public TableResult()
        {
        }

        public TableResult(IEnumerable<T> items, int count)
        {
            Items = items;
            Count = count;
        }
    }
}
