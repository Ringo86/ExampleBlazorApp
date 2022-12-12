using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ExampleBlazorApp.Client.Services
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService localStorageService;
        private ClaimsPrincipal anonymous = new ClaimsPrincipal(new ClaimsIdentity());

        public CustomAuthenticationStateProvider(ILocalStorageService localStorageService)
        {
            this.localStorageService = localStorageService;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                var tokenString = await localStorageService.GetItem<string>("token");
                if(string.IsNullOrEmpty(tokenString))
                    return await Task.FromResult(new AuthenticationState(anonymous));

                var jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(tokenString);
                var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(jwtToken.Claims, "jwtAuth"));
                return await Task.FromResult(new AuthenticationState(claimsPrincipal));
            }
            catch
            {
                return await Task.FromResult(new AuthenticationState(anonymous));
            }
        }

        public async Task UpdateAuthenticationStateAsync()
        {
            ClaimsPrincipal claimsPrincipal;
            var tokenString = await localStorageService.GetItem<string>("token");
            if (string.IsNullOrEmpty(tokenString))
            {
                claimsPrincipal = anonymous;
            }
            else
            {
                var jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(tokenString);
                claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(jwtToken.Claims, "jwtAuth"));
            }
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }
    }
}
