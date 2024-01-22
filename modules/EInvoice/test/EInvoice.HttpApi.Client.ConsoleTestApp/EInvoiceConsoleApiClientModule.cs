using Volo.Abp.Autofac;
using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace EInvoice;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(EInvoiceHttpApiClientModule),
    typeof(AbpHttpClientIdentityModelModule)
    )]
public class EInvoiceConsoleApiClientModule : AbpModule
{

}
