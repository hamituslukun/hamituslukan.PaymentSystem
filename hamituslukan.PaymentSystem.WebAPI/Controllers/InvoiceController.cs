using hamituslukan.PaymentSystem.Business.Interfaces;
using hamituslukan.PaymentSystem.Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hamituslukan.PaymentSystem.WebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class InvoiceController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IService<Invoice> _invoiceService;

        public InvoiceController(UserManager<ApplicationUser> userManager, IService<Invoice> invoiceService)
        {
            _userManager = userManager;
            _invoiceService = invoiceService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Pay(Guid id)
        {
            var invoice = await _invoiceService.FindAsync(x => x.Id == id);

            invoice.PaidDate = DateTime.Now;

            await _invoiceService.UpdateAsync(invoice);

            return Ok();
        }
    }
}