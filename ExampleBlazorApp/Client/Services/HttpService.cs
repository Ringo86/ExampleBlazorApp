using ExampleBlazorApp.Client.Helpers;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace ExampleBlazorApp.Client.Services
{
    public interface IHttpService
    {
        Task<T> Get<T>(string uri);
        Task Post(string uri, object value);
        Task<T> Post<T>(string uri, object value);
        Task Put(string uri, object value);
        Task<T> Put<T>(string uri, object value);
        Task Delete(string uri);
        Task<T> Delete<T>(string uri);
    }

    public class HttpService : IHttpService
    {
        private HttpClient httpClient;
        private NavigationManager navigationManager;
        private ILocalStorageService localStorageService;
        private IConfiguration config;
        private const string _tokenKey = "token";

        public HttpService(
            HttpClient httpClient,
            NavigationManager navigationManager,
            ILocalStorageService localStorageService,
            IConfiguration configuration
        )
        {
            this.httpClient = httpClient;
            this.navigationManager = navigationManager;
            this.localStorageService = localStorageService;
            config = configuration;
        }

        public async Task<T> Get<T>(string uri)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            return await sendRequest<T>(request);
        }

        public async Task Post(string uri, object value)
        {
            var request = createRequest(HttpMethod.Post, uri, value);
            await sendRequest(request);
        }

        public async Task<T> Post<T>(string uri, object value)
        {
            var request = createRequest(HttpMethod.Post, uri, value);
            return await sendRequest<T>(request);
        }

        public async Task Put(string uri, object value)
        {
            var request = createRequest(HttpMethod.Put, uri, value);
            await sendRequest(request);
        }

        public async Task<T> Put<T>(string uri, object value)
        {
            var request = createRequest(HttpMethod.Put, uri, value);
            return await sendRequest<T>(request);
        }

        public async Task Delete(string uri)
        {
            var request = createRequest(HttpMethod.Delete, uri);
            await sendRequest(request);
        }

        public async Task<T> Delete<T>(string uri)
        {
            var request = createRequest(HttpMethod.Delete, uri);
            return await sendRequest<T>(request);
        }

        // helper methods

        private HttpRequestMessage createRequest(HttpMethod method, string uri, object value = null)
        {
            var request = new HttpRequestMessage(method, uri);
            if (value != null)
                request.Content = new StringContent(JsonSerializer.Serialize(value), Encoding.UTF8, "application/json");
            return request;
        }

        private async Task sendRequest(HttpRequestMessage request)
        {
            await addJwtHeader(request);

            // send request
            using var response = await httpClient.SendAsync(request);

            // auto logout on 401 response
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                navigationManager.NavigateTo("account/logout");
                return;
            }

            await handleErrors(response);
        }

        private async Task<T> sendRequest<T>(HttpRequestMessage request)
        {
            await addJwtHeader(request);

            // send request
            using var response = await httpClient.SendAsync(request);

            // auto logout on 401 response
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                navigationManager.NavigateTo("account/logout");
                return default;
            }

            await handleErrors(response);

            var options = new JsonSerializerOptions();
            options.PropertyNameCaseInsensitive = true;
            options.Converters.Add(new StringConverter());
            return await response.Content.ReadFromJsonAsync<T>(options);
        }

        private async Task addJwtHeader(HttpRequestMessage request)
        {
            // add jwt auth header if user is logged in and request is to the api url
            string token = await localStorageService.GetItem<string>(_tokenKey);
            var isApiUrl = !request.RequestUri.IsAbsoluteUri;
            if (!string.IsNullOrEmpty(token) && isApiUrl)
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        private async Task handleErrors(HttpResponseMessage response)
        {
            // throw exception on error response
            if (!response.IsSuccessStatusCode)
            {
                try
                {
                    string mediaTypeString = string.Empty;
                    if (response.Content.Headers.ContentType !=null)
                        mediaTypeString = response.Content.Headers.ContentType.MediaType;
                    if (mediaTypeString == "application/json")
                    {
                        var error = await response.Content.ReadFromJsonAsync<Dictionary<string, string>>();
                        throw new Exception(error["message"]);
                    }
                    //else if (mediaTypeString == "application/problem+json")
                    //{//TODO: deal with specifically application/problem+json response types
                    //    //string responseString = await response.Content.ReadAsStringAsync();
                    //    var details = await JsonSerializer.DeserializeAsync<ValidationProblemDetails>(response.Content.ReadAsStream());
                    //    string errorList = string.Join(',',details.Errors.Values);
                    //    throw new Exception(errorList);
                    //}
                    else
                    {
                        string responseString = await response.Content.ReadAsStringAsync();
                        throw new Exception(responseString);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}
