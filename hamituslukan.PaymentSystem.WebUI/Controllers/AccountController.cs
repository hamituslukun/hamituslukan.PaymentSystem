using hamituslukan.PaymentSystem.Dto.Concrete;
using hamituslukan.PaymentSystem.WebUI.Api.Interfaces;
using hamituslukan.PaymentSystem.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace hamituslukan.PaymentSystem.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountApiService _accountApiService;

        public AccountController(IAccountApiService accountApiService)
        {
            _accountApiService = accountApiService;
        }

        public IActionResult Login()
        {
            var model = new UserLoginViewModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginViewModel model)
        {
            var response = await _accountApiService.Login(model);

            if (response.IsSuccessStatusCode)
            {
                var applicationUserDto = await _accountApiService.CurrentUser();

                if (applicationUserDto.Roles.Contains("Admin"))
                    return RedirectToAction("Index", "Admin");

                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("Email", await response.Content.ReadAsStringAsync());

            return View(model);
        }

        public IActionResult Register()
        {
            var model = new UserRegisterViewModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterViewModel model)
        {
            var response = await _accountApiService.Register(model);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Login");
            }

            var errors = JsonConvert.DeserializeObject<List<IdentityError>>(await response.Content.ReadAsStringAsync());

            foreach (var error in errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _accountApiService.Logout();

            return RedirectToAction("Login");
        }
    }
}