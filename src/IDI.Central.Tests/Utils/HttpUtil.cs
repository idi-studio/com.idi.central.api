using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using IDI.Core.Common;
using IDI.Core.Common.Extensions;

namespace IDI.Central.Tests.Utils
{
    public sealed class HttpUtil : Singleton<HttpUtil>
    {
        private HttpClient client;
        private readonly string address = "http://10.248.36.91/";
        //private readonly string address = "https://10.248.36.91/";
        private string token = "";

        private HttpUtil()
        {
            var handler = new HttpClientHandler
            {
                UseDefaultCredentials = true,
                AutomaticDecompression = DecompressionMethods.GZip
            };
            client = new HttpClient(handler);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("Device", "UnitTesting");
            client.BaseAddress = new Uri(address);

            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            //ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
        }

        public void SetToken(string token)
        {
            this.token = token;
        }

        public string Post(string url, Dictionary<string, string> parameters = null)
        {
            parameters = parameters ?? new Dictionary<string, string>();

            if (url == "/token")
            {
                string clientId = "mslmoveapp_sit", clientSecret = "Qazwsx1!";
                string basic = Convert.ToBase64String(Encoding.UTF8.GetBytes(clientId + ":" + clientSecret));

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", basic);

                return client.PostAsync(url, new FormUrlEncodedContent(parameters)).Result.Content.ReadAsStringAsync().Result;
            }
            else
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                return client.PostAsync(url, new StringContent(parameters.ToJson(), Encoding.UTF8, "application/json")).Result.Content.ReadAsStringAsync().Result;
            }
        }

        //private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        //{
        //    return true;
        //}
    }
}
