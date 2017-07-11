using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Authentication;
using System.Text;
using IDI.Core.Common;
using IDI.Core.Common.Extensions;

namespace IDI.Central.Tests.Utils
{
    public sealed class HttpUtil : Singleton<HttpUtil>
    {
        private HttpClient client;
        private readonly string address = "http://localhost:50963";
        private readonly string clientId = "com.idi.central.testing";
        private readonly string clientSecret = "p@55w0rd";
        private string token = "";

        private HttpUtil()
        {
            var handler = new HttpClientHandler
            {
                UseDefaultCredentials = true,
                AutomaticDecompression = DecompressionMethods.GZip,
                SslProtocols = SslProtocols.Tls | SslProtocols.Tls11 | SslProtocols.Tls12,
                ServerCertificateCustomValidationCallback = (sender, certificate, chain, errors) => { return true; }
            };
            client = new HttpClient(handler);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("Device", "UnitTesting");
            client.BaseAddress = new Uri(address);
        }

        public void SetToken(string token)
        {
            this.token = token;
        }

        public string Post(string url, Dictionary<string, string> parameters = null)
        {
            parameters = parameters ?? new Dictionary<string, string>();

            if (url == "api/token")
            {
                string basic = Convert.ToBase64String(Encoding.UTF8.GetBytes(clientId + ":" + clientSecret));

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", basic);

                var response = client.PostAsync(url, new FormUrlEncodedContent(parameters)).Result;

                return response.Content.ReadAsStringAsync().Result;
            }
            else
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = client.PostAsync(url, new StringContent(parameters.ToJson(), Encoding.UTF8, "application/json")).Result;
                
                return response.Content.ReadAsStringAsync().Result;
            }
        }
    }
}
