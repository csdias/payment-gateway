using System.Threading.Tasks;
using EnterpriseBusinessRules.Entities;

namespace ApplicationBusinessRules.Interfaces
{
    public interface ICreatePaymentUseCase
    {       
        Task<Response<Payment>> CreatePayment(Payment payment);
    }
}
