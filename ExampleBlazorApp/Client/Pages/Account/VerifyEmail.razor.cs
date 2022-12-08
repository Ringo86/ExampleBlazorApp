using ExampleBlazorApp.Client.Helpers;
using Microsoft.AspNetCore.Components;

namespace ExampleBlazorApp.Client.Pages.Account
{
    public partial class VerifyEmail : ComponentBase
    {
        bool isVerificationProcessDone = false;
        bool verificationpassed = false;

        protected override async Task OnInitializedAsync()
        {
            string verificationGuidString = NavigationManager.QueryString("guid") ?? "";
            
            if (!string.IsNullOrEmpty(verificationGuidString))
            {
                if (Guid.TryParse(verificationGuidString, out Guid parsedGuid))
                {
                    await AccountService.VerifyEmail(parsedGuid);
                    verificationpassed = true;
                }   
            }
            isVerificationProcessDone = true;
            await base.OnInitializedAsync();
        }
    }
}
