using FluentValidation.Results;
using EnterpriseBusinessRules.Interfaces;
using EnterpriseBusinessRules.Validators;
using System;

namespace EnterpriseBusinessRules.Entities
{
    public class Payment: IEntity
    {
        public Guid? Id { get; set; }
        public string Client { get; set; }
        public CreditCard CreditCard { get; set; }
        public decimal Ammount { get; set; }

        public ValidationResult Validate() 
        {
            PaymentValidator validator = new PaymentValidator();
            return validator.Validate(this);
        }
    }
}
