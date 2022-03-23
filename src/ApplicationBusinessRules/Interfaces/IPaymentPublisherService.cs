using System.Threading.Tasks;
using EnterpriseBusinessRules.Sns.Entities;

namespace ApplicationBusinessRules.Services
{
    public interface IPaymentPublisherService
    {
        Task<bool> PublishMessage(OutboxMessage message);
    }
}
