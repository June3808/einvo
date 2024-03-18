using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EInvoice.Api
{
    public class ApiClient : IDisposable
    {
        private TimeSpan _retryInterval = new TimeSpan(5000000);
        private HttpClient _client;

        public ApiClient(string baseUrl, string authToken = null)
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri(baseUrl)
            };
            _client.Timeout = TimeSpan.FromMinutes(5);

            if (authToken != null)
            {
                _client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", authToken);
            }

            //
            // Disable http cache
            // https://docs.microsoft.com/en-us/dotnet/api/system.net.http.headers.cachecontrolheadervalue.nocache?view=net-6.0#System_Net_Http_Headers_CacheControlHeaderValue_NoCache
            _client.DefaultRequestHeaders.CacheControl = new CacheControlHeaderValue
            {
                NoCache = true
            };
        }

        public void RefreshToken(string authToken)
        {
            _client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", authToken);
        }

        ~ApiClient()
        {
            Dispose(false);
        }

        public Uri BaseAddress
        {
            get { return _client.BaseAddress; }
        }

        public HttpRequestHeaders Headers
        {
            get { return _client.DefaultRequestHeaders; }
        }

        public TimeSpan Timeout { get; set; }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public T Post<T>(string relativeUriString, List<KeyValuePair<string, string>> keyValuePair, T value = default(T))
        {
            HttpContent content = null;

            if (keyValuePair != null && keyValuePair.Count > 0)
            {
                _client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/x-www-form-urlencoded");
                content = new FormUrlEncodedContent(keyValuePair);
            }
            else if (value != null)
            {
                var contentAsJson = JsonConvert.SerializeObject(value);
                content = new StringContent(contentAsJson, Encoding.UTF8, "application/json");
            }

            using (var response = _client.PostAsync(relativeUriString, content).Result)
            {
                EnsureSuccessStatusCode(response);

                var stringContent = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<T>(stringContent);
            }
        }

        public T Put<T>(string relativeUriString, T value = default(T))
        {
            HttpContent content = null;
            if (value != null)
            {
                var contentAsJson = JsonConvert.SerializeObject(value);
                content = new StringContent(contentAsJson, Encoding.UTF8, "application/json");
            }

            using (var response = _client.PutAsync(relativeUriString, content).Result)
            {
                EnsureSuccessStatusCode(response);

                var stringContent = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<T>(stringContent);
            }
        }

        public T Get<T>(string relativeUriString)
        {
            using (var response = _client.GetAsync(relativeUriString).Result)
            {
                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    return default(T);
                }
                else if (response.StatusCode != HttpStatusCode.OK)
                {
                    EnsureSuccessStatusCode(response);
                }

                var stringContent = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<T>(stringContent);
            }
        }

        public async Task<T> GetAsync<T>(string relativeUriString, int retryCount = 3)
        {
            try
            {
                var result = default(T);
                var response = await _client.GetAsync(relativeUriString);
                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    return default(T);
                }
                else if (response.StatusCode != HttpStatusCode.OK)
                {
                    EnsureSuccessStatusCode(response);
                }
                var stringContent = await response.Content.ReadAsStringAsync();
                try
                {
                    result = JsonConvert.DeserializeObject<T>(stringContent);
                }
                catch (JsonReaderException ex)
                {
                    if (retryCount > 0)
                    {
                        await Task.Delay(1000);
                        retryCount -= 1;
                        result = await this.GetAsync<T>(relativeUriString, retryCount);
                    }
                    else
                    {
                        throw ex;
                    }
                }
                return result;
            }
            catch (Exception httpEx)
            {
                throw httpEx;
            }
        }

        public long GetCount(string relativeUriString)
        {
            long count = 0;

            using (var response = _client.GetAsync(relativeUriString).Result)
            {
                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    return default;
                }
                else if (response.StatusCode != HttpStatusCode.OK)
                {
                    EnsureSuccessStatusCode(response);
                }

                var stringContent = response.Content.ReadAsStringAsync().Result;
                var returnJson = JsonConvert.DeserializeObject<dynamic>(stringContent);
                var strCount = ((JObject)returnJson)?.First?.Next?.First;
                count = (strCount != null) ? Convert.ToInt64(strCount) : 0;

                return count;
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_client != null)
                {
                    _client.Dispose();
                    _client = null;
                }
            }
        }

        private void EnsureSuccessStatusCode(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                return;
            }

            var content = response.Content.ReadAsStringAsync().Result;
            throw new Exception(content);
            //throw new Exception((int)response.StatusCode, content);
        }

        private void Retry(Func<bool> task, TimeSpan timeout)
        {
            var timer = Stopwatch.StartNew();
            while (timer.Elapsed < timeout)
            {
                Thread.Sleep(_retryInterval);

                if (task())
                {
                    return;
                }
            }

            throw new TimeoutException("Retry timeout exceed.");
        }
    }
}
