using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace hamituslukan.PaymentSystem.Entities.Concrete
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public List<Subscriber> Subscribers { get; set; }
    }
}