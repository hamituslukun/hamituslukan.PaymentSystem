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
    public class AccountApiManager : IAccountApiService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;

        public AccountApiManager(HttpClient httpClient, IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
            _httpClient.BaseAddress = new Uri($"{ _configuration.GetValue<string>("Api") }/Auth/");
        }

        public async Task<HttpResponseMessage> Login(UserLoginViewModel model)
        {
            var jsonData = JsonConvert.SerializeObject(model);
            var stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await _httpClient.PostAsync("Login", stringContent);

            if (responseMessage.IsSuccessStatusCode)
            {
                var token = await responseMessage.Content.ReadAsStringAsync();

                _httpContextAccessor.HttpContext.Session.SetString("token", token);
            }

            return responseMessage;
        }

        public async Task Logout()
        {
            _httpContextAccessor.HttpContext.Session.Remove("token");
        }

        public async Task<HttpResponseMessage> Register(UserRegisterViewModel model)
        {
            var jsonData = JsonConvert.SerializeObject(model);
            var stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await _httpClient.PostAsync("Register", stringContent);

            return responseMessage;
        }

        public async Task<ApplicationUserDto> CurrentUser()
        {
            var token = _httpContextAccessor.HttpContext.Session.GetString("token");

            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var responseMessage = await _httpClient.GetAsync("CurrentUser");

                if (responseMessage.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<ApplicationUserDto>(await responseMessage.Content.ReadAsStringAsync());
                }

                return null;
            }

            return null;
        }
    }
}