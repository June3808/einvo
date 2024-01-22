using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace EInvoice;

public class InvoiceJournalsAppServiceTests : EInvoiceApplicationTestBase
{
    private readonly IInvoiceJournalsAppService _invoiceJournalsAppService;

    public InvoiceJournalsAppServiceTests()
    {
        _invoiceJournalsAppService = GetRequiredService<IInvoiceJournalsAppService>();
    }

    /*
    [Fact]
    public async Task Test1()
    {
        // Arrange

        // Act

        // Assert
    }
    */
}

