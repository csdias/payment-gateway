using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EnterpriseBusinessRules.Entities;

namespace ApplicationBusinessRules.Interfaces
{
    public interface IPaymentRepository
    {  
        Task<Response<Payment>> GetPayment(Guid id);
        Task<Response<List<Payment>>> GetPayments(Payment payment);
        Task<Response<bool>> CreatePayment(Payment payment);
        Task<Response<Payment>> UpdatePaymentStatus(Payment payment);
    }
}
