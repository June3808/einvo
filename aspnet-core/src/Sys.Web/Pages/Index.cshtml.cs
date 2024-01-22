using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;

namespace Sys.Web.Pages;

public class IndexModel : SysPageModel
{
    public void OnGet()
    {

    }

    public async Task OnPostLoginAsync()
    {
        await HttpContext.ChallengeAsync("oidc");
    }
}
