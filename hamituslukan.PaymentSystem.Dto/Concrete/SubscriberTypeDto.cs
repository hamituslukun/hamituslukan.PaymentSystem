using System;

namespace hamituslukan.PaymentSystem.Dto.Concrete
{
    public class SubscriberTypeDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int IdentityLength { get; set; }
    }
}