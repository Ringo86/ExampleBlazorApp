using ExampleBlazorApp.Client.Models.Account;
using Microsoft.AspNetCore.Components;

namespace ExampleBlazorApp.Client.Services
{
    public interface IAccountService
    {
        string Token { get; }
        Task Initialize();
        Task<bool> Login(LoginRequest model);
        Task Logout();
        Task Register(RegisterNewAccount model);
        Task Update(RegistrationUpdate model);
        Task<RegistrationUpdate> GetInfoAsync();
        Task VerifyEmail(Guid secretGuid);
        Task<bool> CheckPasswordReset(CheckPasswordResetRequest checkRequest);
        Task RequestPasswordReset(string email);
        Task ResetPassword(PasswordResetRequest request);
    }

    public class AccountService : IAccountService
    {
        private IHttpService httpService;
        private NavigationManager navigationManager;
        private ILocalStorageService localStorageService;
        private const string TOKEN = "token";

        public string Token { get; private set; }

        public AccountService(
            IHttpService httpService,
            NavigationManager navigationManager,
            ILocalStorageService localStorageService)
        {
            this.httpService = httpService;
            this.navigationManager = navigationManager;
            this.localStorageService = localStorageService;
        }

        public async Task Initialize()
        {
            Token = await localStorageService.GetItem<string>(TOKEN);
        }

        public async Task<bool> Login(LoginRequest model)
        {
            var TokenResponse = await httpService.Post<TokenResponse>("account/login", model);
            if(TokenResponse == null)
                return false;
            Token = TokenResponse.Token;
            await localStorageService.SetItem(TOKEN, TokenResponse.Token);
            return true;
        }

        public async Task Logout()
        {
            Token = "";
            await localStorageService.RemoveItem(TOKEN);
            navigationManager.NavigateTo("account/login");
        }

        public async Task Register(RegisterNewAccount model)
        {
            await httpService.Post("account/create", model);
        }

        public async Task Update(RegistrationUpdate model)
        {
            await httpService.Put("account/update", model);
        }

        public async Task<RegistrationUpdate> GetInfoAsync()
        {
            var regInfo = await httpService.Get<RegistrationUpdate>("account/getInfo");
            return regInfo;
        }

        public async Task VerifyEmail(Guid secretGuid)
        {
            await httpService.Post($"account/verifyEmail?secretGuid={secretGuid}", null);
        }

        public async Task<bool> CheckPasswordReset(CheckPasswordResetRequest checkRequest)
        {
            return await httpService.Post<bool>($"account/checkPasswordReset", checkRequest);
        }

        public async Task RequestPasswordReset(string email)
        {
            await httpService.Post<bool>($"account/requestPasswordReset?email={email}", null);
        }

        public async Task ResetPassword(PasswordResetRequest request)
        {
            await httpService.Post<bool>($"account/resetPassword", request);
        }
    }
}
