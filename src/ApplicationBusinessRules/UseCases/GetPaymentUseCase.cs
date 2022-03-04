using System.Threading.Tasks;
using System.Collections.Generic;
using EnterpriseBusinessRules.Entities;
using ApplicationBusinessRules.Interfaces;
using System;

namespace ApplicationBusinessRules.UseCases
{
    public class GetPaymentUseCase: IGetPaymentUseCase
    {
        private IPaymentRepository _paymentRepository;

        public GetPaymentUseCase(IPaymentRepository paymenRepository)
        {
            _paymentRepository = paymenRepository;
        }

        public async  Task<Response<Payment>> GetPayment(Guid id)
        {
            return await _paymentRepository.GetPayment(id);
        }

        public async Task<Response<List<Payment>>> GetPayments(Payment payment)
        {
            return await _paymentRepository.GetPayments(payment);
        }
    }
}
