using hamituslukan.PaymentSystem.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace hamituslukan.PaymentSystem.Business.Interfaces
{
    public interface ISubscriberService : IService<Subscriber>
    {
        Task<Subscriber> FindSubscriberAsync(string identityNumber);
    }
}