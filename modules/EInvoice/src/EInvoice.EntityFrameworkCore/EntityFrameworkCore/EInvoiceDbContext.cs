using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using EInvoice;
using EasyAbp.FileManagement.EntityFrameworkCore;

namespace EInvoice.EntityFrameworkCore;

[ConnectionStringName(EInvoiceDbProperties.ConnectionStringName)]
public class EInvoiceDbContext : AbpDbContext<EInvoiceDbContext>, IEInvoiceDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * public DbSet<Question> Questions { get; set; }
     */
    public DbSet<InvoiceJournals> InvoiceJournals { get; set; }

    public EInvoiceDbContext(DbContextOptions<EInvoiceDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigureEInvoice();
    }
}
