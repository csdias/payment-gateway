using System.Threading.Tasks;
using EnterpriseBusinessRules.Entities;
using ApplicationBusinessRules.Interfaces;

namespace ApplicationBusinessRules.Services
{
    public class PaymentQueueProcessorService : IPaymentQueueProcessorService 
    {

        public async Task<Response<CreditCard>> ValidateCard(CreditCard creditCard)
        {
            var response = new Response<CreditCard>();
            response.SetSuccess(true);
            response.SetResponse(creditCard);
            return await Task<Response<CreditCard>>.FromResult(response);
        }

        public async Task<Response<Payment>> PublishPaymentOrder(Payment payment)
        {
            var response = new Response<Payment>();
            response.SetSuccess(true);
            response.SetResponse(payment);
            return await Task<Response<Payment>>.FromResult(response);
        }
    }
}
