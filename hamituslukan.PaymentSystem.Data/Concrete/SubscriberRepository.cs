using hamituslukan.PaymentSystem.Data.Interfaces;
using hamituslukan.PaymentSystem.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hamituslukan.PaymentSystem.Data.Concrete
{
    public class SubscriberRepository : Repository<Subscriber>, ISubscriberRepository
    {
        private readonly PaymentSystemContext _context;

        public SubscriberRepository(PaymentSystemContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Subscriber> FindSubscriberAsync(string identityNumber)
        {
            return await _context.Set<Subscriber>()
                .Include(x => x.Deposit)
                .Include(x => x.Type)
                .Include(x => x.User)
                .Include(x => x.Invoices)
                .Where(x => x.IdentityNumber == identityNumber).FirstOrDefaultAsync();
        }
    }
}