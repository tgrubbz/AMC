namespace AMC.CORE.Models
{
    public class DataTableRequest
    {
        public int Draw { get; set; }
        public DataTableColumn[] Columns { get; set; }
        public DataTableOrderItem[] Order { get; set; }
        public int Start { get; set; }
        public int Length { get; set; }
        public DataTableColumnSearch Search { get; set; }

        public DataTableRequest()
        {
            Columns = new DataTableColumn[0];
            Order = new DataTableOrderItem[0];
            Search = new DataTableColumnSearch();
        }
    }

    public class DataTableColumn
    {
        public string Data { get; set; }
        public bool Searchable { get; set; }
        public bool Orderable { get; set; }
        public DataTableColumnSearch Search { get; set; }

        public DataTableColumn()
        {
            Search = new DataTableColumnSearch();
        }
    }

    public class DataTableColumnSearch
    {
        public string Value { get; set; }
        public bool Regex { get; set; }
    }

    public class DataTableOrderItem
    {
        public int Column { get; set; }
        public string Dir { get; set; }
    }
}
