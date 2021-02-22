using hamituslukan.PaymentSystem.WebUI.Api.Interfaces;
using hamituslukan.PaymentSystem.WebUI.CustomFilters;
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
    [JwtAuthorize(Roles = "User")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ISubscriberApiService _subscriberApiService;
        private readonly IInvoiceApiService _invoiceApiService;

        public HomeController(ILogger<HomeController> logger, ISubscriberApiService subscriberApiService, IInvoiceApiService invoiceApiService)
        {
            _logger = logger;
            _subscriberApiService = subscriberApiService;
            _invoiceApiService = invoiceApiService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Invoices()
        {
            var model = new SubscriberViewModel();

            var subscriber = await _subscriberApiService.CurrentSubscriber();

            if (subscriber.IsSuccessStatusCode)
            {
                model = JsonConvert.DeserializeObject<SubscriberViewModel>(await subscriber.Content.ReadAsStringAsync());
            }

            return View(model);
        }

        public async Task<IActionResult> Timeline()
        {
            var model = new SubscriberViewModel();

            var subscriber = await _subscriberApiService.CurrentSubscriber();

            if (subscriber.IsSuccessStatusCode)
            {
                model = JsonConvert.DeserializeObject<SubscriberViewModel>(await subscriber.Content.ReadAsStringAsync());
            }

            return View(model);
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}