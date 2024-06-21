using Inficare.Application.Admin.ExchangeRate.Queries;
using Inficare.Application.Common.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Inficare.Infrastructure.Services
{
    public class ExchangeRate : IExchangeRate
    {
        public async Task<Rate> getRateAsync(string currencyId)
        {
            HttpClient client = GetHttpClient(30);
            var response = new Rate();
            using HttpResponseMessage httpResponse = await client.GetAsync(client.BaseAddress);
            if (httpResponse.IsSuccessStatusCode)
            {
                string value = await httpResponse.Content.ReadAsStringAsync();
                var currentModel = JsonConvert.DeserializeObject<CurrencyExchangeModel>(value);
                var payload = currentModel.data.payload.Where(x => x.date == DateTimeOffset.UtcNow.ToString("yyyy-mm-dd")).FirstOrDefault();
                var rate = payload.rates.Where(w => w.currency.iso3.ToLower() == currencyId.ToLower()).FirstOrDefault();
                return rate;
            }
            return response;
        }

        private HttpClient GetHttpClient(int timeout)
        {
            HttpClient httpClient = new HttpClient(new HttpClientHandler
            {
                ClientCertificateOptions = ClientCertificateOption.Manual,
                ServerCertificateCustomValidationCallback = (HttpRequestMessage httpRequestMessage, X509Certificate2? cert, X509Chain? cetChain, SslPolicyErrors policyErrors) => true
            });
            var uri = new UriBuilder("https://www.nrb.org.np/api/forex/v1/rates");
            var query = HttpUtility.ParseQueryString(uri.Query);
            query["page"] = "1";
            query["per_page"] = "100";
            query["from"] = DateTimeOffset.UtcNow.ToString("yyyy-mm-dd");
            query["to"] = DateTimeOffset.UtcNow.ToString("yyyy-mm-dd");
            uri.Query = query.ToString();

            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.BaseAddress = uri.Uri;
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.Timeout = TimeSpan.FromSeconds(timeout);

            return httpClient;
        }
    }
}
