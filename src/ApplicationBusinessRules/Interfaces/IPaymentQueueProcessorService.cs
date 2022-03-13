using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EnterpriseBusinessRules.Entities;

namespace ApplicationBusinessRules.Interfaces
{
    public interface IPaymentQueueProcessorService
    {
        Task<Response<Payment>> PublishPaymentOrder(Payment payment);
    }
}
