using FluentValidation;
using EnterpriseBusinessRules.Entities;

namespace EnterpriseBusinessRules.Validators
{
    public class PaymentValidator : AbstractValidator<Payment>
    {
        public PaymentValidator()
        {
            RuleFor(c => c.Amount)
                .NotEmpty()
                .WithMessage("{PropertyName} is required");
            RuleFor(c => c.CreditCard).NotEmpty().WithMessage("{PropertyName} is required");
            RuleFor(c => c.CreditCard).SetValidator(new CreditCardValidator());
        }
    }
}
