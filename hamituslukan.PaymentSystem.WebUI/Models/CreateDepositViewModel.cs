using System;

namespace hamituslukan.PaymentSystem.WebUI.Models
{
    public class CreateDepositViewModel
    {
        public decimal Amount { get; set; }
        public DateTime ReceiveDate { get; set; }
        public DateTime? ReturnDate { get; set; }
    }
}