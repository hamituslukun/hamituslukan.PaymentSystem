using hamituslukan.PaymentSystem.Business.Interfaces;
using hamituslukan.PaymentSystem.Data.Interfaces;
using hamituslukan.PaymentSystem.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace hamituslukan.PaymentSystem.Business.Concrete
{
    public class SubscriberManager : Manager<Subscriber>, ISubscriberService
    {
        private readonly ISubscriberRepository _subscriberRepository;
        private readonly IRepository<Subscriber> _entityRepository;

        public SubscriberManager(ISubscriberRepository subscriberRepository, IRepository<Subscriber> entityRepository) : base(entityRepository)
        {
            _subscriberRepository = subscriberRepository;
            _entityRepository = entityRepository;
        }

        public async Task<Subscriber> FindSubscriberAsync(string identityNumber)
        {
            return await _subscriberRepository.FindSubscriberAsync(identityNumber);
        }
    }
}