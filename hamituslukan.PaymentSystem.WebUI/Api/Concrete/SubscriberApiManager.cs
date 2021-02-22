using hamituslukan.PaymentSystem.Dto.Concrete;
using hamituslukan.PaymentSystem.WebUI.Api.Interfaces;
using hamituslukan.PaymentSystem.WebUI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace hamituslukan.PaymentSystem.WebUI.Api.Concrete
{
    public class SubscriberApiManager : ISubscriberApiService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;

        public SubscriberApiManager(HttpClient httpClient, IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
            _httpClient.BaseAddress = new Uri($"{ _configuration.GetValue<string>("Api") }/Subscriber/");
        }

        public async Task<HttpResponseMessage> CreateSubscriber(CreateSubscriberViewModel model)
        {
            var token = _httpContextAccessor.HttpContext.Session.GetString("token");

            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var jsonData = JsonConvert.SerializeObject(model);
                var stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
                var responseMessage = await _httpClient.PostAsync("Create", stringContent);

                return responseMessage;
            }

            return null;
        }

        public async Task<HttpResponseMessage> CurrentSubscriber()
        {
            var token = _httpContextAccessor.HttpContext.Session.GetString("token");

            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var responseMessage = await _httpClient.GetAsync($"GetByCurrentUser");

                return responseMessage;
            }

            return null;
        }

        public async Task<List<SubscriberTypeDto>> GetSubscriberTypes()
        {
            var token = _httpContextAccessor.HttpContext.Session.GetString("token");

            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var responseMessage = await _httpClient.GetAsync("GetSubscriberTypes");

                if (responseMessage.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<List<SubscriberTypeDto>>(await responseMessage.Content.ReadAsStringAsync());
                }

                return null;
            }

            return null;
        }

        public async Task<HttpResponseMessage> SearchSubscriber(string IdentityNumber)
        {
            var token = _httpContextAccessor.HttpContext.Session.GetString("token");

            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var responseMessage = await _httpClient.GetAsync($"Get/{ IdentityNumber }");

                return responseMessage;
            }

            return null;
        }

        public async Task<HttpResponseMessage> TerminateSubscriber(string IdentityNumber)
        {
            var token = _httpContextAccessor.HttpContext.Session.GetString("token");

            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var responseMessage = await _httpClient.GetAsync($"Terminate/{ IdentityNumber }");

                return responseMessage;
            }

            return null;
        }
    }
}