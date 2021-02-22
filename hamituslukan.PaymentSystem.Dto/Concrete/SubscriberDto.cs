using System;
using System.Collections.Generic;

namespace hamituslukan.PaymentSystem.Dto.Concrete
{
    public class SubscriberDto
    {
        public Guid Id { get; set; }
        public SubscriberTypeDto Type { get; set; }
        public string IdentityNumber { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DepositDto Deposit { get; set; }
        public ApplicationUserDto User { get; set; }
        public List<InvoiceDto> Invoices { get; set; }
    }
}