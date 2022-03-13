using FluentValidation;
using EnterpriseBusinessRules.Entities;

namespace EnterpriseBusinessRules.Validators
{
    public class CreditCardValidator : AbstractValidator<CreditCard>
    {
        public CreditCardValidator()
        {
            RuleFor(c => c.ExpirationMonth)
                .NotEmpty()
                .WithMessage("{PropertyName} is required")
                .Length(2)
                .WithMessage("{PropertyName} should have {Length} characters");
            RuleFor(c => c.ExpirationYear)
                .NotEmpty()
                .WithMessage("{PropertyName} is required")
                .Length(4)
                .WithMessage("{PropertyName} should have {Length} characters");
            RuleFor(c => c.Cvv)
                .NotEmpty()
                .WithMessage("{PropertyName} is required")
                .Length(3)
                .WithMessage("{PropertyName} should have {Length} characters");
        }
    }
}
