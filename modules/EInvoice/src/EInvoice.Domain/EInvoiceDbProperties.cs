namespace EInvoice;

public static class EInvoiceDbProperties
{
    public static string DbTablePrefix { get; set; } = "DOT_";

    public static string? DbSchema { get; set; } = null;

    public const string ConnectionStringName = "EInvoice";
}
