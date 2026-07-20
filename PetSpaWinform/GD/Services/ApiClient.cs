using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GD.Services
{
    public class ApiClient
    {
        private static ApiClient _instance;
        private readonly HttpClient _httpClient;
        private string _authToken;

        public string BaseUrl { get; private set; } = "http://localhost:5014";

        public HttpClient Client => _httpClient;

        private ApiClient()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(BaseUrl);
        }

        public static ApiClient Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ApiClient();
                }
                return _instance;
            }
        }

        public void SetBaseUrl(string baseUrl)
        {
            BaseUrl = baseUrl;
            _httpClient.BaseAddress = new Uri(baseUrl);
        }

        public void SetAuthToken(string token)
        {
            _authToken = token;
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _authToken);
        }

        public void Logout()
        {
            _authToken = null;
            _httpClient.DefaultRequestHeaders.Authorization = null;
        }

        public async Task<T> GetAsync<T>(string endpoint)
        {
            var response = await _httpClient.GetAsync(endpoint);
            return await HandleResponse<T>(response);
        }

        public async Task<T> PostAsync<T>(string endpoint, object data)
        {
            var json = JsonConvert.SerializeObject(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(endpoint, content);
            return await HandleResponse<T>(response);
        }

        public async Task<T> PutAsync<T>(string endpoint, object data)
        {
            var json = JsonConvert.SerializeObject(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync(endpoint, content);
            return await HandleResponse<T>(response);
        }

        public async Task<T> DeleteAsync<T>(string endpoint)
        {
            var response = await _httpClient.DeleteAsync(endpoint);
            return await HandleResponse<T>(response);
        }

        public async Task<T> PostMultipartAsync<T>(string endpoint, MultipartFormDataContent content)
        {
            var response = await _httpClient.PostAsync(endpoint, content);
            return await HandleResponse<T>(response);
        }

        public async Task<T> PutMultipartAsync<T>(string endpoint, MultipartFormDataContent content)
        {
            var response = await _httpClient.PostAsync(endpoint, content);
            return await HandleResponse<T>(response);
        }

        private async Task<T> HandleResponse<T>(HttpResponseMessage response)
        {
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                try
                {
                    var errorObj = JsonConvert.DeserializeObject<dynamic>(content);
                    throw new Exception(errorObj?.message?.ToString() ?? $"API Error: {response.StatusCode}");
                }
                catch
                {
                    throw new Exception($"API Error: {response.StatusCode} - {content}");
                }
            }
            return JsonConvert.DeserializeObject<T>(content);
        }
    }
}