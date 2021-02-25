using AutoMapper;
using FluentValidation;
using hamituslukan.PaymentSystem.Business.Interfaces;
using hamituslukan.PaymentSystem.Dto.Concrete;
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
    public class SubscriberController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ISubscriberService _subscriberService;
        private readonly IService<Deposit> _depositService;
        private readonly IService<SubscriberType> _subscriberTypeService;
        private readonly IService<Invoice> _invoiceService;
        private readonly IValidator<SubscriberDto> _subscriberValidator;
        private readonly IValidator<DepositDto> _depositValidator;
        private readonly IMapper _mapper;

        public SubscriberController(UserManager<ApplicationUser> userManager,
            ISubscriberService subscriberService, IService<Deposit> depositService,
            IService<SubscriberType> subscriberTypeService,
            IValidator<SubscriberDto> subscriberValidator,
            IValidator<DepositDto> depositValidator,
            IMapper mapper, IService<Invoice> invoiceService)
        {
            _userManager = userManager;
            _subscriberService = subscriberService;
            _depositService = depositService;
            _subscriberTypeService = subscriberTypeService;
            _subscriberValidator = subscriberValidator;
            _depositValidator = depositValidator;
            _mapper = mapper;
            _invoiceService = invoiceService;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(SubscriberDto request)
        {
            var subscriberType = await _subscriberTypeService.FindAsync(x => x.Id == request.Type.Id);
            request.Type = _mapper.Map<SubscriberTypeDto>(subscriberType);

            var subscriberValidationResult = _subscriberValidator.Validate(request);
            var depositValidationResult = _depositValidator.Validate(request.Deposit);

            if (subscriberValidationResult.IsValid && depositValidationResult.IsValid)
            {
                var user = new ApplicationUser { Email = request.User.Email, Name = request.User.Name, UserName = request.User.Email };

                var userResult = await _userManager.CreateAsync(user, request.User.Password);

                if (userResult.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "User");
                }
                else
                {
                    return BadRequest(userResult.Errors);
                }

                var deposit = _mapper.Map<Deposit>(request.Deposit);
                await _depositService.AddAsync(deposit);

                var subscriber = _mapper.Map<Subscriber>(request);
                subscriber.Type = subscriberType;
                subscriber.User = user;
                subscriber.Deposit = deposit;
                await _subscriberService.AddAsync(subscriber);

                for (int i = 0; i < 12; i++)
                {
                    var invoice = new Invoice();
                    invoice.StartDate = new DateTime(DateTime.Now.Year, 1, 1).AddMonths(i);
                    invoice.EndDate = invoice.StartDate.AddDays(DateTime.DaysInMonth(invoice.StartDate.Year, invoice.StartDate.Month) - 1);
                    invoice.DueDate = invoice.EndDate.AddDays(10);
                    invoice.Amount = 500;
                    invoice.Subscriber = subscriber;
                    await _invoiceService.AddAsync(invoice);
                }

                return Ok("Kayıt oluşturuldu");
            }

            if (!depositValidationResult.IsValid)
                return BadRequest(depositValidationResult.Errors);

            return BadRequest(subscriberValidationResult.Errors);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var subscriber = await _subscriberService.FindSubscriberAsync(id);

            if (subscriber != null)
            {
                var subscriberDto = _mapper.Map<SubscriberDto>(subscriber);

                return Ok(subscriberDto);
            }

            return NotFound("Abone bulunamadı");
        }

        [Authorize(Roles = "User")]
        [HttpGet]
        public async Task<IActionResult> GetByCurrentUser()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var subscriber = await _subscriberService.FindAsync(x => x.User.Id == user.Id);

            subscriber = await _subscriberService.FindSubscriberAsync(subscriber.IdentityNumber);

            if (subscriber != null)
            {
                var subscriberDto = _mapper.Map<SubscriberDto>(subscriber);

                return Ok(subscriberDto);
            }

            return NotFound("Abone bulunamadı");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Terminate(string id)
        {
            var subscriber = await _subscriberService.FindSubscriberAsync(id);

            if (subscriber != null)
            {
                foreach (var invoice in subscriber.Invoices)
                {
                    if (invoice.PaidDate == null)
                    {
                        return BadRequest("Ödenmemiş fatura bulunmaktadır");
                    }
                }

                if (subscriber.Deposit != null && subscriber.Deposit.ReturnDate == null)
                {
                    return BadRequest("Depozito iade yapılmadan abonelik kapatılamaz");
                }

                subscriber.EndDate = DateTime.Now;

                await _subscriberService.UpdateAsync(subscriber);
            }

            return NotFound("Abone bulunamadı");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetSubscriberTypes()
        {
            var subscriberTypes = await _subscriberTypeService.GetAsync();

            var mapped = _mapper.Map<List<SubscriberTypeDto>>(subscriberTypes);

            return Ok(mapped);
        }
    }
}