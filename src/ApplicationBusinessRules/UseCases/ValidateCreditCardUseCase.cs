using System.Threading.Tasks;
using System.Collections.Generic;
using EnterpriseBusinessRules.Entities;
using ApplicationBusinessRules.Interfaces;
using System;

namespace ApplicationBusinessRules.UseCases
{
    public class ValidateCreditCardUseCase : IValidateCreditCardUseCase
    {
        public ValidateCreditCardUseCase()
        {
        }

        public async Task<Response<CreditCard>> Validate(CreditCard creditCard)
        {
            return await Task<Response<CreditCard>>.FromResult(new Response<CreditCard>());
        }
    }
}
