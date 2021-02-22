using System;

namespace hamituslukan.PaymentSystem.Dto.Concrete
{
    public class DepositDto
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime ReceiveDate { get; set; }
        public DateTime? ReturnDate { get; set; }
    }
}