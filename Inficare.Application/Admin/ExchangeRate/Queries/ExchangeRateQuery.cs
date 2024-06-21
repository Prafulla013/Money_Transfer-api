using Inficare.Application.Common.Interfaces;
using Inficare.Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Web;

namespace Inficare.Application.Admin.ExchangeRate.Queries
{
    public class ExchangeRateHandler : IRequestHandler<ListExchangeRateQuery, CurrencyExchangeModel>
    {
        public async Task<CurrencyExchangeModel> Handle(ListExchangeRateQuery request, CancellationToken cancellationToken)
        {
            HttpClient client = GetHttpClient(30, request);
            var response = new CurrencyExchangeModel();
            try
            {
                using HttpResponseMessage httpResponse = await client.GetAsync(client.BaseAddress);
                if (httpResponse.IsSuccessStatusCode)
                {
                    string value = await httpResponse.Content.ReadAsStringAsync();
                    response = JsonConvert.DeserializeObject<CurrencyExchangeModel>(value);
                }
                return response;
            }
            finally
            {
                client.Dispose();
            }
        }
        private HttpClient GetHttpClient(int timeout, ListExchangeRateQuery exchangeQuery)
        {
            HttpClient httpClient = new HttpClient(new HttpClientHandler
            {
                ClientCertificateOptions = ClientCertificateOption.Manual,
                ServerCertificateCustomValidationCallback = (HttpRequestMessage httpRequestMessage, X509Certificate2? cert, X509Chain? cetChain, SslPolicyErrors policyErrors) => true
            });
            var uri = new UriBuilder("https://www.nrb.org.np/api/forex/v1/rates");
            var query = HttpUtility.ParseQueryString(uri.Query);
            query["page"] = exchangeQuery.Page.ToString();
            query["per_page"] = exchangeQuery.PerPage.ToString();
            query["from"] = exchangeQuery.FromDate;
            query["to"] = exchangeQuery.ToDate;
            uri.Query = query.ToString();

            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.BaseAddress = uri.Uri;
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.Timeout = TimeSpan.FromSeconds(timeout);

            return httpClient;
        }


    }

    public class ListExchangeRateQuery : IRequest<CurrencyExchangeModel>
    {
        public int Page { get; set; } = 1;
        public int PerPage { get; set; } = 5;
        public string FromDate { get; set; } = DateTimeOffset.UtcNow.LocalDateTime.ToString("yyyy-mm-dd");
        public string ToDate { get; set; } = DateTimeOffset.UtcNow.LocalDateTime.ToString("yyyy-mm-dd");
    }
}
