using ExampleBlazorApp.Client.Models.Account;
using ExampleBlazorApp.Client.Services;
using Microsoft.AspNetCore.Components;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ExampleBlazorApp.Client.ViewModels
{
    public partial class LoginViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private readonly IAccountService accountService;

        private bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set
            {
                isBusy = value;
                OnPropertyChanged();
            }
        }

        private LoginRequest model = new();

        public LoginRequest Model
        {
            get { return model; }
            set
            {
                model = value;
                OnPropertyChanged();
            }
        }

        public LoginViewModel(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        public async Task<bool> AttemptLogin()
        {
            IsBusy = true;
            bool loginSucceeded = await accountService.Login(model);
            IsBusy = false;
            return loginSucceeded;
        }

        // Create the OnPropertyChanged method to raise the event
        // The calling member's name will be used as the parameter.
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
