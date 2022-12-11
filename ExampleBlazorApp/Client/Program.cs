using ExampleBlazorApp.Client;
using ExampleBlazorApp.Client.Services;
using ExampleBlazorApp.Client.ViewModels;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services
                .AddScoped<IAccountService, AccountService>()
                .AddScoped<IAlertService, AlertService>()
                .AddScoped<IHttpService, HttpService>()
                .AddScoped<ILocalStorageService, LocalStorageService>()
                .AddScoped<IMessageBoardService, MessageBoardService>();

builder.Services.AddTransient<IMessageBoardViewModel,MessageBoardViewModel>();


// configure http client
builder.Services.AddScoped(x =>
{
    var uriString = builder.Configuration["ApiUrl"];
    var apiUrl = new Uri(uriString);
    return new HttpClient() { BaseAddress = apiUrl };
});

var host = builder.Build();

var accountService = host.Services.GetRequiredService<IAccountService>();
await accountService.Initialize();

await host.RunAsync();
