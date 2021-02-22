using System.Net.Http;
using System.Threading.Tasks;

namespace hamituslukan.PaymentSystem.WebUI.Api.Interfaces
{
    public interface IDepositApiService
    {
        Task<HttpResponseMessage> DepositReturn(string IdentityNumber);
    }
}