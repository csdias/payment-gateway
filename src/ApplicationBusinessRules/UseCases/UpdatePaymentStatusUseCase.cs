using System.Threading.Tasks;
using System.Collections.Generic;
using EnterpriseBusinessRules.Entities;
using ApplicationBusinessRules.Interfaces;

namespace ApplicationBusinessRules.UseCases
{
    public class UpdatePaymentStatusUseCase: IUpdatePaymentStatusUseCase
    {
        private IPaymentRepository _paymentRepository;

        public UpdatePaymentStatusUseCase(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task<Response<Payment>> UpdatePaymentStatus(Payment payment)
        {
            return await _paymentRepository.UpdatePaymentStatus(payment);
        }
    }
}
