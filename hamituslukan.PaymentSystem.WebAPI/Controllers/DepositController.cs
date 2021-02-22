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
    public class DepositController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ISubscriberService _subscriberService;
        private readonly IService<Deposit> _depositService;

        public DepositController(UserManager<ApplicationUser> userManager, ISubscriberService subscriberService, IService<Deposit> depositService)
        {
            _userManager = userManager;
            _subscriberService = subscriberService;
            _depositService = depositService;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Return(string id)
        {
            var subscriber = await _subscriberService.FindSubscriberAsync(id);

            var deposit = subscriber.Deposit;
            deposit.ReturnDate = DateTime.Now;

            await _depositService.UpdateAsync(deposit);

            return Ok();
        }
    }
}