using System.Threading.Tasks;
using System.Collections.Generic;
using EnterpriseBusinessRules.Entities;
using System;

namespace ApplicationBusinessRules.Interfaces
{
    public interface IGetPaymentUseCase
    {
        Task<Response<Payment>> GetPayment(Guid id);
        Task<Response<List<Payment>>> GetPayments(Payment payment);
    }
}
