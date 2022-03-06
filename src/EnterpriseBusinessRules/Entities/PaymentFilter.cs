using FluentValidation.Results;
using EnterpriseBusinessRules.Interfaces;
using EnterpriseBusinessRules.Validators;
using System;

namespace EnterpriseBusinessRules.Entities
{
    public class PaymentFilter: IEntity
    {
        public Guid? Id { get; set; }
        public string ClientId { get; set; }
        public CreditCard CreditCard { get; set; }
        public decimal Ammount { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public short StatusId { get; set; }

        public ValidationResult Validate() 
        {
            PaymentFilterValidator validator = new PaymentFilterValidator();
            return validator.Validate(this);
        }
    }
}
