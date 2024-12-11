using ASAPTask.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASAPTask.Applications.Common.Interfaces
{
    public interface IASAPTaskDbContext
    {
        public DbSet<ASAPTask.Domain.Entities.Item> Items { get; set; }
        public DbSet<InvoiceDetails> InvoiceDetails { get; set; }
        public DbSet<ASAPTask.Domain.Entities.Invoice> Invoices { get; set; }
        DbSet<T> GetDbSet<T>() where T : class;
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        ValueTask DisposeAsync();
        void Dispose();
    }
}
