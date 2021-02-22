using hamituslukan.PaymentSystem.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace hamituslukan.PaymentSystem.Data.Interfaces
{
    public interface ISubscriberRepository : IRepository<Subscriber>
    {
        Task<Subscriber> FindSubscriberAsync(string IdentityNumber);
    }
}