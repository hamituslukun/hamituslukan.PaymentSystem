using hamituslukan.PaymentSystem.Entities.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;

namespace hamituslukan.PaymentSystem.Entities.Concrete
{
    public class Invoice : IEntity
    {
        [Key]
        public Guid Id { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime DueDate { get; set; }
        public decimal Amount { get; set; }
        public DateTime? PaidDate { get; set; }
        public Subscriber Subscriber { get; set; }
    }
}