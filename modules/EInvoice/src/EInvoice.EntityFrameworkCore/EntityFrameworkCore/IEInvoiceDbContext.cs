using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using EInvoice;

namespace EInvoice.EntityFrameworkCore;

[ConnectionStringName(EInvoiceDbProperties.ConnectionStringName)]
public interface IEInvoiceDbContext : IEfCoreDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * DbSet<Question> Questions { get; }
     */
    DbSet<InvoiceJournals> InvoiceJournals { get; set; }
}
