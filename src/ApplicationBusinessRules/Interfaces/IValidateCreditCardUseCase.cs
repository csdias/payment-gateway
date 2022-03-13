using System.Threading.Tasks;
using System.Collections.Generic;
using EnterpriseBusinessRules.Entities;
using System;

namespace ApplicationBusinessRules.Interfaces
{
    public interface IValidateCreditCardUseCase
    {
        Task<Response<CreditCard>> Validate(CreditCard creditCard);
    }
}
