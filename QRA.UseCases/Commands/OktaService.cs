using AutoMapper;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using QRA.Entities.Models;
using QRA.Entities.oktaModels;
using QRA.UseCases.contracts;
using System.Text;


namespace QRA.UseCases.Commands
{
    public  class OktaService : BaseHttpClient, IOktaService
    {
        public IConfiguration iconfiguration;
        static HttpClient client = new HttpClient();
        public OktaService(HttpClient client, IConfiguration configuration) : base(client) {
            iconfiguration = configuration;
        }
        
        public OktaToken getToken()
        {
            var path = "/oauth2/default/v1/token";

            var dict = new Dictionary<string, string>();
            dict.Add("grant_type", "client_credentials");
            dict.Add("scope", "scopeQRA");
            
        
            //tokenRequest body = new tokenRequest();
            //body.Grant_type = "client_credentials";
            //body.Scope = "scopeQRA";
            var authenticationString =  $"{iconfiguration["Okta:clientIdAPI"]}:{iconfiguration["Okta:secretAPI"]}";
            var base64Encode = Convert.ToBase64String(System.Text.ASCIIEncoding.UTF8.GetBytes(authenticationString));
            
            var headers = new Dictionary<string, string>
            {
                { "Connection", "keep-alive" },
                { "Accept", "application/json" },
                { "Authorization", "Basic " + base64Encode }
            };
            //OktaUserResponse
            var result = PostToken<OktaToken>(path, dict, headers).Result;

            return result;
        }

        public string CreateUser(OktaUser body ) 
        {
            var path = "/api/v1/users?activate=true";

            var headers = new Dictionary<string, string>
            {
                { "Connection", "keep-alive" },
                { "Accept", "application/json" },
                { "Authorization", "SSWS " + iconfiguration["Okta:api_key"]  }
            };
            //OktaUserResponse
            var result = Post<dynamic>(path, body, headers).Result;

            return result.status ;
        }
        public string CreateUserGroup(OktaUser body)
        {
            var path = "/api/v1/users?activate=true";

            var headers = new Dictionary<string, string>
            {
                { "Connection", "keep-alive" },
                { "Accept", "application/json" },
                { "Authorization", "SSWS " + iconfiguration["Okta:api_key"]  }
            };
            //OktaUserResponse
            var result = Post<dynamic>(path, body, headers).Result;

            return result.status;
        }

        public string CreateGroups(oktaGroup body)
        {
            var path = "/api/v1/groups";

            var headers = new Dictionary<string, string>
            {
                { "Connection", "keep-alive" },
                { "Accept", "application/json" },
                { "Authorization", "SSWS " + iconfiguration["Okta:api_key"]  }
            };
            //OktaUserResponse
            var result = Post<dynamic>(path, body, headers).Result;

            return result.status;
        }

        public string AddUsertoGroup(long groupID , long userid)
        {
            var path = "/api/v1/groups/"+groupID+"/users/"+userid;

            var headers = new Dictionary<string, string>
            {
                { "Connection", "keep-alive" },
                { "Accept", "application/json" },
                { "Authorization", "SSWS " + iconfiguration["Okta:api_key"]  }
            };
            
            var result = Put<dynamic>(path, null, headers).Result;

            return result.status;
        }
        public string DeleteUsertoGroup(long groupID, long userid)
        {
            var path = "/api/v1/groups/" + groupID + "/users/" + userid;

            var headers = new Dictionary<string, string>
            {
                { "Connection", "keep-alive" },
                { "Accept", "application/json" },
                { "Authorization", "SSWS " + iconfiguration["Okta:api_key"]  }
            };
           
            var result = Delete<dynamic>(path, headers).Result;

            return result.status;
        }

    }

    public abstract class BaseHttpClient
    {
        private readonly HttpClient _client;

        /// <summary>
        /// Constructor por defecto de <see cref="BaseHttpClient"/>.
        /// </summary>
        /// <param name="client">Cliente HTTP con la configuración base del API..</param>
        public BaseHttpClient(HttpClient client)
        {
            _client = client;
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="path"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public async Task<TResponse> Get<TResponse>(string path, Dictionary<string, string> headers)
        {
            HttpRequestMessage message = CreateMessage(path, HttpMethod.Get, headers);

            HttpResponseMessage response = await _client.SendAsync(message);

            //  response.EnsureSuccessStatusCode();

            TResponse model = await ParseResponse<TResponse>(response);

            return model;
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <returns></returns>
        public async Task<TResponse> PostToken<TResponse>(string path, Dictionary<string, string> body, Dictionary<string, string> headers)
        {

            HttpRequestMessage message = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = GetUri(path)
            };
            //string content  = new FormUrlEncodedContent(body);
            if (body != null)
                message.Content = new FormUrlEncodedContent(body);  //new StringContent(content, Encoding.UTF8, "application/json");

            AddHttpHeaders(message, headers);

            
            var task = Task.Run(() => _client.SendAsync(message));
            task.Wait();

            HttpResponseMessage response = task.Result;

            response.EnsureSuccessStatusCode();

            TResponse model = await ParseResponse<TResponse>(response);

            return model;
        }
        public async Task<TResponse> Post<TResponse>(string path, object body, Dictionary<string, string> headers)
        {


            HttpRequestMessage message = CreateMessage(path, HttpMethod.Post, headers, body);

            var task = Task.Run(() => _client.SendAsync(message));
            task.Wait();

            HttpResponseMessage response = task.Result;

            response.EnsureSuccessStatusCode();

            TResponse model = await ParseResponse<TResponse>(response);

            return model;
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <returns></returns>
        public async Task<TResponse> Put<TResponse>(string path, object body, Dictionary<string, string> headers)
        {
            HttpRequestMessage message = CreateMessage(path, HttpMethod.Put, headers, body);

            HttpResponseMessage response = await _client.SendAsync(message);

            response.EnsureSuccessStatusCode();

            TResponse model = await ParseResponse<TResponse>(response);

            return model;
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <returns></returns>
        public async Task<TResponse> Delete<TResponse>(string path, Dictionary<string, string> headers)
        {
            HttpRequestMessage message = CreateMessage(path, HttpMethod.Delete, headers);

            HttpResponseMessage response = await _client.SendAsync(message);

            response.EnsureSuccessStatusCode();

            TResponse model = await ParseResponse<TResponse>(response);

            return model;
        }

        private void AddHttpHeaders(HttpRequestMessage message, Dictionary<string, string> headers)
        {
            foreach (var header in headers)
                message.Headers.Add(header.Key, header.Value);
        }

        private async Task<TResponse> ParseResponse<TResponse>(HttpResponseMessage response)
        {
            string json = await response.Content.ReadAsStringAsync();

            TResponse model = JsonConvert.DeserializeObject<TResponse>(json);

            return model;
        }

        private HttpRequestMessage CreateMessage(string path,
            HttpMethod method,
            Dictionary<string, string> headers,
            object body = null)
        {
            HttpRequestMessage message = new HttpRequestMessage
            {
                Method = method,
                RequestUri = GetUri(path)
            };
            string content = JsonConvert.SerializeObject(body);
            if (body != null)
                message.Content = new StringContent(content, Encoding.UTF8, "application/json");

            AddHttpHeaders(message, headers);

            return message;
        }

        private HttpContent CreateBodyContent(object body = null)
        {
            HttpContent content = null;

            string bodyJson = JsonConvert.SerializeObject(body);
            if (body != null)
                content = new StringContent(bodyJson.ToString(), Encoding.UTF8, "application/json");


            return content;
        }

        private Uri GetUri(string path)
        {
            var uri = new Uri(_client.BaseAddress, path);

            return uri;
        }
    }
}
