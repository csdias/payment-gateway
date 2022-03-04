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

        public PaymentService (
            IGetPaymentUseCase getPayment,
            ICreatePaymentUseCase createPayment,
            IUpdatePaymentStatusUseCase updatePaymentStatus
        ) 
        {
            this._getPayment = getPayment;
            this._createPayment = createPayment;
            this._updatePaymentStatus = updatePaymentStatus;
        }

        public async Task<Response<Payment>> GetPayment(Guid id)
        {
            return await this._getPayment.GetPayment(id);
        }

        public async Task<Response<List<Payment>>> GetPayments(Payment payment) 
        {
            return await this._getPayment.GetPayments(payment);
        }

        public async Task<Response<bool>> CreatePayment(Payment payment)
        {
            return await this._createPayment.CreatePayment(payment);
        }

        public async Task<Response<Payment>> UpdatePaymentStatus(Payment payment)
        {
            return await this._updatePaymentStatus.UpdatePaymentStatus(payment);
        }
    }
}
