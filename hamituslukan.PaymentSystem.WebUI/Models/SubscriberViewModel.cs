using System;
using System.Collections.Generic;

namespace hamituslukan.PaymentSystem.WebUI.Models
{
    public class SubscriberViewModel
    {
        public Guid Id { get; set; }
        public SubscriberTypeViewModel Type { get; set; }
        public string IdentityNumber { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime? EndDate { get; set; }
        public CreateDepositViewModel Deposit { get; set; }
        public UserRegisterViewModel User { get; set; }
        public List<InvoiceViewModel> Invoices { get; set; }
    }
}