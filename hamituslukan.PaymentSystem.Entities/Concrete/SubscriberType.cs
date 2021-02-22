using hamituslukan.PaymentSystem.Entities.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;

namespace hamituslukan.PaymentSystem.Entities.Concrete
{
    public class SubscriberType : IEntity
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }
        public int IdentityLength { get; set; }
    }
}