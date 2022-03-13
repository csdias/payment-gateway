using System.Threading.Tasks;
using EnterpriseBusinessRules.Entities;
using ApplicationBusinessRules.Interfaces;

namespace ApplicationBusinessRules.UseCases
{
    public class CreatePaymentUseCase: ICreatePaymentUseCase
    {
        private IPaymentRepository _paymentRepository;

        public CreatePaymentUseCase(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task<Response<Payment>> CreatePayment(Payment payment)
        {
            return await _paymentRepository.CreatePayment(payment);
        }
    }
}
