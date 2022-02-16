using ChatApp.Mobile.Services.Interfaces;
using ChatApp.Services.Core;
using Newtonsoft.Json;
using Prism.Navigation;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Mobile.Services.Core
{
    public class BaseService
    {
        protected readonly ISessionService SessionService;
        private readonly INavigationService navigationService;

        protected string BaseAddress { get; set; }

        protected string BaseUrl
        {
            get => $"{BaseAddress}/";
        }


        public BaseService(ISessionService sessionService)
        {
            InitRoutes();
            SessionService = sessionService;
        }

        public BaseService(ISessionService sessionService, INavigationService navigationService)
        {
            InitRoutes();
            SessionService = sessionService;
            this.navigationService = navigationService;
        }

        protected async Task<HttpClient> GetClient(bool withoutToken = false)
        {
            return await GetClient(BaseUrl, withoutToken);
        }

        private void InitRoutes()
        {
            BaseAddress = ServiceSettings.BaseAddress;
        }

        private HttpClient GetClientRefresh()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(BaseUrl);
            return client;

        }

        protected async Task<HttpClient> GetClient(string baseUrl, bool withoutToken = false)
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            HttpClient client = new HttpClient(clientHandler);
            client.BaseAddress = new Uri(baseUrl);
            return client;
        }

        protected async Task Get(string url)
        {
            using (HttpClient client = await GetClient())
            {
                try
                {
                    var response = await client.GetAsync(url);
                    if (!response.IsSuccessStatusCode)
                    {
                        // TODO log.
                    }
                }
                catch (HttpRequestException ex)
                {
                    // TODO log.
                }
            }
        }

        protected async Task<T> Get<T>(string url, bool withoutToken = false)
        {
            using (HttpClient client = await GetClient(withoutToken))
            {
                try
                {
                    var response = await client.GetAsync(url);
                    if (!response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        return default;
                    }
                    else
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<T>(result);
                    }
                }
                catch (HttpRequestException exp)
                {
                    return default;
                }
            }
        }

        protected async Task<T> Post<T, M>(string url, M model)
        {
            var content = JsonConvert.SerializeObject(model);
            HttpContent contentPost = new StringContent(content, Encoding.UTF8, "application/json");
            using (HttpClient client = await GetClient())
            {
                try
                {
                    var response = await client.PostAsync(url, contentPost);

                    if (!response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();

                        return default;
                    }
                    else
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<T>(result);
                    }
                }
                catch (Exception exp)
                {
                    return default;
                }
            }
        }

        protected async Task<bool> Post(string url, MultipartFormDataContent model)
        {
            using (HttpClient client = await this.GetClient())
            {
                try
                {
                    var response = await client.PostAsync(url, model);


                    if (!response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        return false;
                    }
                    else
                    {
                        var result = await response.Content.ReadAsByteArrayAsync();
                        return true;
                    }
                }
                catch (Exception exp)
                {
                    return false;
                }
            }
        }
        protected async Task<T> PostRefresh<T, M>(string url, M model)
        {
            var content = JsonConvert.SerializeObject(model);
            HttpContent contentPost = new StringContent(content, Encoding.UTF8, "application/json");
            using (HttpClient client = this.GetClientRefresh())
            {
                try
                {
                    var response = await client.PostAsync(url, contentPost);

                    if (!response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        return default(T);
                    }
                    else
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<T>(result);
                    }
                }
                catch (Exception exp)
                {
                    return default(T);
                }
            }
        }

        protected async Task<T> PutAsync<T, M>(string url, M model)
        {
            var content = JsonConvert.SerializeObject(model);
            HttpContent contentPost = new StringContent(content, Encoding.UTF8, "application/json");
            using (HttpClient client = await GetClient())
            {
                try
                {
                    var response = await client.PutAsync(url, contentPost);

                    if (!response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        return default;
                    }
                    else
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<T>(result);
                    }
                }
                catch (Exception exp)
                {
                    return default;
                }
            }
        }

    }
}
