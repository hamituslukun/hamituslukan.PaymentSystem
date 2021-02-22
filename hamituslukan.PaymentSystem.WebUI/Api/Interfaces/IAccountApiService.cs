using hamituslukan.PaymentSystem.Dto.Concrete;
using hamituslukan.PaymentSystem.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace hamituslukan.PaymentSystem.WebUI.Api.Interfaces
{
    public interface IAccountApiService
    {
        Task<HttpResponseMessage> Login(UserLoginViewModel model);

        Task<HttpResponseMessage> Register(UserRegisterViewModel model);

        Task Logout();

        Task<ApplicationUserDto> CurrentUser();
    }
}