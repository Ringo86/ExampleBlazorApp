using ExampleBlazorApp.Client.Helpers;
using ExampleBlazorApp.Client.Services;
using Microsoft.AspNetCore.Components;

namespace ExampleBlazorApp.Client.Pages.Account
{
    public partial class Login: ComponentBase
    {
        protected override void OnInitialized()
        {
            base.OnInitialized();
            viewModel.PropertyChanged += (s,e) => StateHasChanged();
        }

        private async void OnValidSubmit()
        {
            // reset alerts on submit
            AlertService.Clear();
            if (await viewModel.AttemptLogin())
            {
                await ((CustomAuthenticationStateProvider)AuthenticationStateProvider).UpdateAuthenticationStateAsync();
                var returnUrl = NavigationManager.QueryString("returnUrl") ?? "";
                NavigationManager.NavigateTo(returnUrl);
            }
        }
    }
}
