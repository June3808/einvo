using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace EInvoice;

public static class InvoiceJournalsEfCoreQueryableExtensions
{
    public static IQueryable<InvoiceJournals> IncludeDetails(this IQueryable<InvoiceJournals> queryable, bool include = true)
    {
        if (!include)
        {
            return queryable;
        }

        return queryable
            // .Include(x => x.xxx) // TODO: AbpHelper generated
            ;
    }
}
