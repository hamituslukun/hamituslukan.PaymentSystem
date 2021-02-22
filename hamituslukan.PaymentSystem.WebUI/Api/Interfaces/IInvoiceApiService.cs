using System.Net.Http;
using System.Threading.Tasks;

namespace hamituslukan.PaymentSystem.WebUI.Api.Interfaces
{
    public interface IInvoiceApiService
    {
        Task<HttpResponseMessage> PayInvoice(string id);
    }
}