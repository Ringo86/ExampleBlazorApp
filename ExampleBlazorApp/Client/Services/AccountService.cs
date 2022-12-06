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
        Task Register(AddUser model);
        Task<IList<User>> GetAll();
        Task<User> GetById(string id);
        Task Update(string id, EditUser model);
        Task Delete(string id);
    }

    public class AccountService : IAccountService
    {
        private IHttpService _httpService;
        private NavigationManager _navigationManager;
        private ILocalStorageService _localStorageService;
        private string _tokenKey = "token";

        public string Token { get; private set; }

        public AccountService(
            IHttpService httpService,
            NavigationManager navigationManager,
            ILocalStorageService localStorageService
        )
        {
            _httpService = httpService;
            _navigationManager = navigationManager;
            _localStorageService = localStorageService;
        }

        public async Task Initialize()
        {
            Token = await _localStorageService.GetItem<string>(_tokenKey);
        }

        public async Task Login(Login model)
        {
            var TokenResponse = await _httpService.Post<TokenResponse>("account/login", model);
            await _localStorageService.SetItem(_tokenKey, TokenResponse.Token);
        }

        public async Task Logout()
        {
            Token = null;
            await _localStorageService.RemoveItem(_tokenKey);
            _navigationManager.NavigateTo("account/login");
        }

        public async Task Register(AddUser model)
        {
            await _httpService.Post("/users/register", model);
        }

        public async Task<IList<User>> GetAll()
        {
            return await _httpService.Get<IList<User>>("/users");
        }

        public async Task<User> GetById(string id)
        {
            return await _httpService.Get<User>($"/users/{id}");
        }

        public async Task Update(string id, EditUser model)
        {
            //await _httpService.Put($"/users/{id}", model);

            //// update stored user if the logged in user updated their own record
            //if (id == Token.Id)
            //{
            //    // update local storage
            //    Token.FirstName = model.FirstName;
            //    Token.LastName = model.LastName;
            //    Token.Username = model.Username;
            //    await _localStorageService.SetItem(_tokenKey, Token);
            //}
        }

        public async Task Delete(string id)
        {
            await _httpService.Delete($"/users/{id}");

            // auto logout if the logged in user deleted their own record
            //if (id == Token.Id)
            //    await Logout();
        }
    }
}
