using hamituslukan.PaymentSystem.Entities.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;

namespace hamituslukan.PaymentSystem.Entities.Concrete
{
    public class Deposit : IEntity
    {
        [Key]
        public Guid Id { get; set; }

        public decimal Amount { get; set; }
        public DateTime ReceiveDate { get; set; }
        public DateTime? ReturnDate { get; set; }
    }
}