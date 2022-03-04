using System.Threading.Tasks;
using EnterpriseBusinessRules.Entities;

namespace ApplicationBusinessRules.Interfaces
{
    public interface IUpdatePaymentStatusUseCase
    {       
        Task<Response<Payment>> UpdatePaymentStatus(Payment payment);     
    }
}
