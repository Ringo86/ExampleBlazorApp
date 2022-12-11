using ExampleBlazorApp.Client.Helpers;
using ExampleBlazorApp.Client.Models.Account;
using ExampleBlazorApp.Client.Services;
using Microsoft.AspNetCore.Components;

namespace ExampleBlazorApp.Client.Pages.Account
{
    public partial class EditAccount : ComponentBase
    {
        RegistrationUpdate model = new RegistrationUpdate();
        private bool loading;

        [Inject]
        public IAccountService accountService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var regInfo = await accountService.GetInfoAsync();
            if (regInfo != null)
            {
                model.Email = regInfo.Email;
                model.FirstName = regInfo.FirstName;
                model.LastName = regInfo.LastName;
            }

            await base.OnInitializedAsync();
        }

        private async void OnValidSubmit()
        {
            // reset alerts on submit
            AlertService.Clear();

            loading = true;
            try
            {
                await AccountService.Update(model);
                var returnUrl = NavigationManager.QueryString("returnUrl") ?? "";
                NavigationManager.NavigateTo(returnUrl);
            }
            catch (Exception ex)
            {
                AlertService.Error(ex.Message);
            }
            loading = false;
            StateHasChanged();
        }
    }
}
