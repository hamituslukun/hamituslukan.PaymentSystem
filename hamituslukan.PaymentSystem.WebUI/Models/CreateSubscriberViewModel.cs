using System;

namespace hamituslukan.PaymentSystem.WebUI.Models
{
    public class CreateSubscriberViewModel
    {
        public SubscriberTypeViewModel Type { get; set; }
        public string IdentityNumber { get; set; }
        public DateTime BeginDate { get; set; }
        public CreateDepositViewModel Deposit { get; set; }
        public UserRegisterViewModel User { get; set; }
    }
}