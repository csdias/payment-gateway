using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EnterpriseBusinessRules.Entities;

namespace ApplicationBusinessRules.Interfaces
{
    public interface IPaymentService
    {
        Task<Response<Payment>> GetPayment(Guid id);
        Task<Response<List<Payment>>> GetPayments(Payment payment);
        Task<Response<Payment>> CreatePayment(Payment payment);
        Task<Response<Payment>> UpdatePaymentStatus(Payment payment);
        Task<Response<CreditCard>> ValidateCard(CreditCard creditCard);
    }
}
