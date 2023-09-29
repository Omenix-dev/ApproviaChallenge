using ApproviaChallenge.TaskManager.Core.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ApproviaChallenge.TaskManager.Core.ExternalServices
{
    public class ClientFactory : IClient
    {
        private readonly HttpClient _client;
        public ClientFactory(IHttpClientFactory clientfactory)
        {
            _client = clientfactory.CreateClient();
        }
        public async Task<TRes> PostRequest<TRes, TReq>(TReq requestModel, string requestUrl)
            where TRes : class
            where TReq : class
        {
            return await PostRequestAsync<TReq, TRes>(requestUrl, requestModel);
        }
        public async Task<TRes> GetRequest<TRes>(string requestUrl)
           where TRes : class
        {
            return await GetRequestAsync<TRes>(requestUrl);
        }
        public async Task<TRes> DeleteRequest<TRes>(string id)
           where TRes : class
        {
            return await DeleteRequestAsync<TRes>(id);
        }
        private async Task<TRes> GetRequestAsync<TRes>(string requestUrl) where TRes : class
        {
            var client = CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Get, requestUrl);

            return await GetResponseResultAsync<TRes>(client, request);
        }
        private async Task<TRes> DeleteRequestAsync<TRes>(string id) where TRes : class
        {
            var client = CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Delete, id.ToString());
            return await GetResponseResultAsync<TRes>(client, request);
        }
        private async Task<TRes> PostRequestAsync<TReq, TRes>(string requestUrl, TReq requestModel)
            where TReq : class
            where TRes : class
        {
            var caller = CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Post, requestUrl)
            {
                Content = new StringContent(JsonConvert.SerializeObject(requestModel), Encoding.UTF8, "application/json")
            };
            return await GetResponseResultAsync<TRes>(caller, request);
        }
        private async Task<TRes> GetResponseResultAsync<TRes>(HttpClient clientCaller, HttpRequestMessage request) where TRes : class
        {
            var response = await clientCaller.SendAsync(request);
            if (!response.IsSuccessStatusCode) throw new ArgumentException(response.ReasonPhrase);
            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<TRes>(responseString);
            return result;
        }
        private HttpClient CreateClient()
        {
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return _client;
        }
        public void Dispose()
        {
            _client.Dispose();
        }
    }
}