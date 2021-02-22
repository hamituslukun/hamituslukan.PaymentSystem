using hamituslukan.PaymentSystem.WebUI.Api.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace hamituslukan.PaymentSystem.WebUI.Api.Concrete
{
    public class InvoiceApiManager : IInvoiceApiService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;

        public InvoiceApiManager(HttpClient httpClient, IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
            _httpClient.BaseAddress = new Uri($"{ _configuration.GetValue<string>("Api") }/Invoice/");
        }

        public async Task<HttpResponseMessage> PayInvoice(string id)
        {
            var token = _httpContextAccessor.HttpContext.Session.GetString("token");

            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var responseMessage = await _httpClient.GetAsync($"Pay/{ id }");

                return responseMessage;
            }

            return null;
        }
    }
}