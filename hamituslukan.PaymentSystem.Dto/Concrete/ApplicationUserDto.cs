using System.Collections.Generic;

namespace hamituslukan.PaymentSystem.Dto.Concrete
{
    public class ApplicationUserDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public List<SubscriberDto> Subscribers { get; set; }
        public string Roles { get; set; }
    }
}