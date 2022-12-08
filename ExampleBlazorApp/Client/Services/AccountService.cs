using ExampleBlazorApp.Client.Models.Account;
using Microsoft.AspNetCore.Components;

namespace ExampleBlazorApp.Client.Services
{
    public interface IAccountService
    {
        string Token { get; }
        Task Initialize();
        Task Login(Login model);
        Task Logout();
        Task Register(RegisterNewAccount model);
        Task UpdateRegistration(RegistrationUpdate model);
        Task<RegistrationUpdate> GetRegistrationInfoAsync();
        Task VerifyEmail(Guid secretGuid);
    }

    public class AccountService : IAccountService
    {
        private IHttpService httpService;
        private NavigationManager _navigationManager;
        private ILocalStorageService _localStorageService;
        private const string _tokenKey = "token";

        public string Token { get; private set; }

        public AccountService(
            IHttpService httpService,
            NavigationManager navigationManager,
            ILocalStorageService localStorageService
        )
        {
            this.httpService = httpService;
            _navigationManager = navigationManager;
            _localStorageService = localStorageService;
        }

        public async Task Initialize()
        {
            Token = await _localStorageService.GetItem<string>(_tokenKey);
        }

        public async Task Login(Login model)
        {
            var TokenResponse = await httpService.Post<TokenResponse>("account/login", model);
            await _localStorageService.SetItem(_tokenKey, TokenResponse.Token);
        }

        public async Task Logout()
        {
            Token = null;
            await _localStorageService.RemoveItem(_tokenKey);
            _navigationManager.NavigateTo("account/login");
        }

        public async Task Register(RegisterNewAccount model)
        {
            await httpService.Post("account/register", model);
        }

        public async Task UpdateRegistration(RegistrationUpdate model)
        {
            await httpService.Put("account/UpdateRegistration", model);
        }

        public async Task<RegistrationUpdate> GetRegistrationInfoAsync()
        {
            var regInfo = await httpService.Get<RegistrationUpdate>("account/getRegistrationInfo");
            return regInfo;
        }

        public async Task VerifyEmail(Guid secretGuid)
        {
            await httpService.Post($"account/verifyEmail?secretGuid={secretGuid}", null);
        }
    }
}
