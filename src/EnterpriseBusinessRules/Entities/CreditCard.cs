using FluentValidation.Results;
using EnterpriseBusinessRules.Validators;
using EnterpriseBusinessRules.Interfaces;
using System;

namespace EnterpriseBusinessRules.Entities
{
    public class CreditCard: IEntity
    {
        public Guid Id { get; set; }
        public string CardNumber { get; set; }
        public string ExpirationMonth { get; set; }
        public string ExpirationYear { get; set; }
        public string Cvv { get; set; }

        public ValidationResult Validate() 
        {
            CreditCardValidator validator = new CreditCardValidator();
            return validator.Validate(this);
        }
    }
}
