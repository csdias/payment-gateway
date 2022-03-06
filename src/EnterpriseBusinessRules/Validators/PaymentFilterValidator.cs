using FluentValidation;
using EnterpriseBusinessRules.Entities;

namespace EnterpriseBusinessRules.Validators
{
    public class PaymentFilterValidator : AbstractValidator<PaymentFilter>
    {
        public PaymentFilterValidator()
        {
        }
    }
}
