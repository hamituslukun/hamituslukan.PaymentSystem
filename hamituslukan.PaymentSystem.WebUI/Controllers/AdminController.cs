using hamituslukan.PaymentSystem.Dto.Concrete;
using hamituslukan.PaymentSystem.WebUI.Api.Interfaces;
using hamituslukan.PaymentSystem.WebUI.CustomFilters;
using hamituslukan.PaymentSystem.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hamituslukan.PaymentSystem.WebUI.Controllers
{
    [JwtAuthorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ISubscriberApiService _subscriberApiService;
        private readonly IDepositApiService _depositApiService;
        private readonly IInvoiceApiService _invoiceApiService;

        public AdminController(ISubscriberApiService subscriberApiService, IDepositApiService depositApiService, IInvoiceApiService invoiceApiService)
        {
            _subscriberApiService = subscriberApiService;
            _depositApiService = depositApiService;
            _invoiceApiService = invoiceApiService;
        }

        public IActionResult Index()
        {
            var model = new SearchSubscriberViewModel();

            return View(model);
        }

        public async Task<IActionResult> CreateSubscriber()
        {
            var subscriberTypes = await _subscriberApiService.GetSubscriberTypes();
            ViewBag.SubscriberTypes = subscriberTypes;

            var model = new CreateSubscriberViewModel();
            model.BeginDate = DateTime.Today;
            model.Deposit = new CreateDepositViewModel { ReceiveDate = DateTime.Today };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSubscriber(CreateSubscriberViewModel model)
        {
            var responseMessage = await _subscriberApiService.CreateSubscriber(model);

            if (responseMessage.IsSuccessStatusCode)
            {
                return Redirect($"Subscriber?IdentityNumber={ model.IdentityNumber }");
            }
            else
            {
                var errors = await responseMessage.Content.ReadAsStringAsync();

                if (errors.Contains("errorMessage"))
                {
                    var validationErrors = JsonConvert.DeserializeObject<List<ValidationError>>(errors);

                    foreach (var error in validationErrors)
                    {
                        ModelState.AddModelError("", error.ErrorMessage);
                    }
                }
                else
                {
                    var identityErrors = JsonConvert.DeserializeObject<List<IdentityError>>(errors);

                    foreach (var error in identityErrors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }

            var subscriberTypes = await _subscriberApiService.GetSubscriberTypes();
            ViewBag.SubscriberTypes = subscriberTypes;

            return View(model);
        }

        public async Task<IActionResult> Subscriber(string IdentityNumber)
        {
            var response = await _subscriberApiService.SearchSubscriber(IdentityNumber);

            if (response.IsSuccessStatusCode)
            {
                var subscriber = await response.Content.ReadAsStringAsync();

                var subscriberModel = JsonConvert.DeserializeObject<SubscriberViewModel>(subscriber);

                return View(subscriberModel);
            }
            else
            {
                TempData["Message"] = "Abone bulunamadı";
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DepositReturn(string IdentityNumber)
        {
            var response = await _depositApiService.DepositReturn(IdentityNumber);

            if (response.IsSuccessStatusCode)
            {
                TempData["Message"] = "Deposit başarıyla geri ödendi";
            }
            else
            {
                TempData["Message"] = "Deposit geri ödemesi gerçekleştirilemedi";
            }

            return Redirect(Request.Headers["Referer"].ToString());
        }

        public async Task<IActionResult> TerminateSubscriber(string IdentityNumber)
        {
            var response = await _subscriberApiService.TerminateSubscriber(IdentityNumber);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            var responseMessage = await response.Content.ReadAsStringAsync();

            TempData["Message"] = responseMessage;

            return Redirect(Request.Headers["Referer"].ToString());
        }

        public async Task<IActionResult> PayInvoice(string id)
        {
            var response = await _invoiceApiService.PayInvoice(id);

            if (response.IsSuccessStatusCode)
            {
                TempData["Message"] = "Fatura başarıyla ödendi";
            }
            else
            {
                TempData["Message"] = "Fatura ödemesi gerçekleştirilemedi";
            }

            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
}