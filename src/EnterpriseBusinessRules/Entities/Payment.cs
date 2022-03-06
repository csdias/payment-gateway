using FluentValidation.Results;
using EnterpriseBusinessRules.Interfaces;
using EnterpriseBusinessRules.Validators;
using System;

namespace EnterpriseBusinessRules.Entities
{
    public class Payment: IEntity
    {
        public Guid? Id { get; set; }
        public string ClientId { get; set; }
        public CreditCard CreditCard { get; set; }
        public decimal Ammount { get; set; }
        public DateTime Date { get; set; }
        public short StatusId { get; set; }

        public ValidationResult Validate() 
        {
            PaymentValidator validator = new PaymentValidator();
            return validator.Validate(this);
        }
    }
}
