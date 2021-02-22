using System;

namespace hamituslukan.PaymentSystem.Dto.Concrete
{
    public class InvoiceDto
    {
        public Guid Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime DueDate { get; set; }
        public decimal Amount { get; set; }
        public DateTime? PaidDate { get; set; }
        public SubscriberDto Subscriber { get; set; }
    }
}