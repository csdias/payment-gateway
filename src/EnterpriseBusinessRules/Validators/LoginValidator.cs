using FluentValidation;
using EnterpriseBusinessRules.Entities;

namespace EnterpriseBusinessRules.Validators
{
    public class LoginValidator : AbstractValidator<LoginEntity>
    {
        public LoginValidator()
        {
            RuleFor(c => c.Email)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull().WithMessage("{PropertyName} is required")
                .EmailAddress().WithMessage("{PropertyName} is invalid");
                
            RuleFor(c => c.Password)
                .NotEmpty().WithMessage("{PropertyName} is required");
        }
    }
}