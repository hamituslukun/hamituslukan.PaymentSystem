using System;

namespace hamituslukan.PaymentSystem.WebUI.Models
{
    public class InvoiceViewModel
    {
        public Guid Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime DueDate { get; set; }
        public decimal Amount { get; set; }
        public DateTime? PaidDate { get; set; }
    }
}