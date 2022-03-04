using System.Threading.Tasks;
using EnterpriseBusinessRules.Entities;

namespace ApplicationBusinessRules.Interfaces
{
    public interface ICreatePaymentUseCase
    {       
        Task<Response<bool>> CreatePayment(Payment payment);
    }
}
