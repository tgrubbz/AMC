using AMC.DAL.Interfaces;
using AMC.CORE.Models;
using System.Data;
using Dapper;
using System.Linq;
using System;
using System.Collections.Generic;

namespace AMC.DAL.Repositories
{
    public class InvoiceRepository : IInvoiceRepository
    {
        IDbConnection conn;
        public InvoiceRepository(IDbConnection connection)
        {
            conn = connection;
        }

        public int Create(Invoice_DB invoice)
        {
            return conn.Execute("INSERT INTO Invoices (DateCreated, NotaryUserId, BillFromLocationId, BillToLocationId, Description, Total) OUTPUT INSERTED.Id VALUES (@DateCreated, @NotaryUserId, @BillFromLocationId, @BillToLocationId, @Description, @Total)", invoice);
        }

        public DataTableResult<Invoice> GetTable(DataTableRequest request)
        {
            int count = conn.Query<int>("SELECT COUNT(*) FROM Users").SingleOrDefault();
            IEnumerable<Invoice> invoices = conn.Query<Invoice>("SELECT * FROM Invoices");
            return new DataTableResult<Invoice>(invoices, count);
        }

        public Invoice_DB Read(int invoiceNumber)
        {
            return conn.Query<Invoice_DB>("SELECT * FROM Invoices WHERE InvoiceNumber = @InvoiceNumber", new { InvoiceNumber = invoiceNumber }).SingleOrDefault();
        }
    }
}
