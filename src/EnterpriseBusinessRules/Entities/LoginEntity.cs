using System.Collections.Generic;
using FluentValidation.Results;
using EnterpriseBusinessRules.Validators;
using EnterpriseBusinessRules.Interfaces;

namespace EnterpriseBusinessRules.Entities
{
    public class LoginEntity: IEntity
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public ValidationResult Validate()
        {
            LoginValidator validator = new LoginValidator();
            return validator.Validate(this);
        }
    }
}