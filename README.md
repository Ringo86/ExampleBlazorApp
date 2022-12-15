# ExampleBlazorApp
### This is a work in progress in the very early phases!
### This project is an example Blazor WASM app using .NET 6.0
## Things not done yet:
### - CSS overhaul
### - MVVM implementation for complex UI
### - implement Mediator design pattern for inter-component events
### - exception handling service
### - any completed features other than account-related (waiting on API features)
### - several minor bugs in the UI
## What it's got:
### - jwt claims mapped to ASP.NET user principal via custom AuthenticationStateProvider
### - role-based view authorization
### - dependency injected services (http, local storage, account api, ...)
### - account features (login, register, edit info, password reset)
