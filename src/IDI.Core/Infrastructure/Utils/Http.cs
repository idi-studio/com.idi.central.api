using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Authentication;
using System.Text;
using IDI.Core.Common;
using IDI.Core.Common.Extensions;

namespace IDI.Core.Infrastructure.Utils
{
    public sealed class Http : Singleton<Http>
    {
        private readonly HttpClientHandler handler;

        private Http()
        {
            handler = new HttpClientHandler
            {
                UseDefaultCredentials = true,
                AutomaticDecompression = DecompressionMethods.GZip,
                SslProtocols = SslProtocols.Tls | SslProtocols.Tls11 | SslProtocols.Tls12,
                ServerCertificateCustomValidationCallback = (sender, certificate, chain, errors) => { return true; }
            };
        }

        public string Post(string address, string url, object parameter, AuthenticationHeaderValue authorization = null)
        {
            parameter = parameter ?? new object();

            var client = BuildClient(address);

            if (authorization != null)
                client.DefaultRequestHeaders.Authorization = authorization;

            var response = client.PostAsync(url, new StringContent(parameter.ToJson(), Encoding.UTF8, "application/json")).Result;

            if (response.IsSuccessStatusCode)
                return response.Content.ReadAsStringAsync().Result;

            throw new HttpRequestException(response.ReasonPhrase);
        }

        public string Get(string address, string url, AuthenticationHeaderValue authorization = null)
        {
            var client = BuildClient(address);

            if (authorization != null)
                client.DefaultRequestHeaders.Authorization = authorization;

            var response = client.GetAsync(url).Result;

            if (response.IsSuccessStatusCode)
                return response.Content.ReadAsStringAsync().Result;

            throw new HttpRequestException(response.ReasonPhrase);
        }

        private HttpClient BuildClient(string address)
        {
            var client = new HttpClient(handler);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.BaseAddress = new Uri(address);

            return client;
        }
    }
}
