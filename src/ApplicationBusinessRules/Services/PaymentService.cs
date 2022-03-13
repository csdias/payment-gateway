using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using EnterpriseBusinessRules.Entities;
using ApplicationBusinessRules.Interfaces;
using ApplicationBusinessRules.Helpers;

namespace ApplicationBusinessRules.Services
{
    public class PaymentService : IPaymentService 
    {
        private readonly IGetPaymentUseCase _getPayment;
        private readonly ICreatePaymentUseCase _createPayment;
        private readonly IUpdatePaymentStatusUseCase _updatePaymentStatus;
        private readonly IValidateCreditCardUseCase _validateCreditCard;
        private readonly IPaymentQueueProcessorService _queueProcessor;


        public PaymentService (
            IGetPaymentUseCase getPayment,
            ICreatePaymentUseCase createPayment,
            IUpdatePaymentStatusUseCase updatePaymentStatus,
            IPaymentQueueProcessorService queueProcessor,
            IValidateCreditCardUseCase validateCreditCard
        ) 
        {
            _getPayment = getPayment;
            _createPayment = createPayment;
            _updatePaymentStatus = updatePaymentStatus;
            _queueProcessor = queueProcessor;
            _validateCreditCard = validateCreditCard;
    }

        public async Task<Response<Payment>> GetPayment(Guid id)
        {
            return await _getPayment.GetPayment(id);
        }

        public async Task<Response<List<Payment>>> GetPayments(Payment payment) 
        {
            return await _getPayment.GetPayments(payment);
        }

        public async Task<Response<Payment>> CreatePayment(Payment payment)
        {
            var result = new Response<Payment>();

            // Validate Payment
            result = await _createPayment.CreatePayment(payment);

            // Insert Payment in the Queue
            var publish = await _queueProcessor.PublishPaymentOrder(payment);

            // Validate Item Queue

            return result;
        }

        public async Task<Response<Payment>> UpdatePaymentStatus(Payment payment)
        {
            return await _updatePaymentStatus.UpdatePaymentStatus(payment);
        }

        public async Task<Response<CreditCard>> ValidateCard(CreditCard creditCard)
        {
            return await _validateCreditCard.Validate(creditCard);
        }
    }
}
