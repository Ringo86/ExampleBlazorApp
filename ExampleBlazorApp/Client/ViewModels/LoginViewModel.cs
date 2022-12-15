using ExampleBlazorApp.Client.Models.Account;
using ExampleBlazorApp.Client.Services;

namespace ExampleBlazorApp.Client.ViewModels
{
    public class LoginViewModel :BaseViewModel
    {
        private readonly IAccountService accountService;

        public LoginRequest Model
        {
            get;
            private set;
        } = new();

        public LoginViewModel(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        public async Task<bool> AttemptLogin()
        {
            IsBusy = true;
            bool loginSucceeded = await accountService.Login(Model);
            IsBusy = false;
            return loginSucceeded;
        }
    }
}
