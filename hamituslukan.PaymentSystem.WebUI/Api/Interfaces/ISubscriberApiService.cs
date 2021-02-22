using hamituslukan.PaymentSystem.Dto.Concrete;
using hamituslukan.PaymentSystem.WebUI.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace hamituslukan.PaymentSystem.WebUI.Api.Interfaces
{
    public interface ISubscriberApiService
    {
        Task<List<SubscriberTypeDto>> GetSubscriberTypes();

        Task<HttpResponseMessage> CreateSubscriber(CreateSubscriberViewModel model);

        Task<HttpResponseMessage> SearchSubscriber(string IdentityNumber);

        Task<HttpResponseMessage> CurrentSubscriber();

        Task<HttpResponseMessage> TerminateSubscriber(string IdentityNumber);
    }
}