using hamituslukan.PaymentSystem.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace hamituslukan.PaymentSystem.Entities.Concrete
{
    public class Subscriber : IEntity
    {
        [Key]
        public Guid Id { get; set; }

        public SubscriberType Type { get; set; }
        public string IdentityNumber { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Deposit Deposit { get; set; }
        public ApplicationUser User { get; set; }
        public List<Invoice> Invoices { get; set; }
    }
}