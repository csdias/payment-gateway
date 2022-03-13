using System.Collections.Generic;
using FluentValidation.Results;
using EnterpriseBusinessRules.Validators;
using EnterpriseBusinessRules.Interfaces;

namespace EnterpriseBusinessRules.Entities
{
    public class Merchant: IEntity
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string BankAccountDetails { get; set; }

        public ValidationResult Validate()
        {
            MerchantValidator validator = new MerchantValidator();
            return validator.Validate(this);
        }
    }
}
