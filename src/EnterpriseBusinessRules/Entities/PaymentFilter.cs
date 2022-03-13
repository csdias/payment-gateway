using FluentValidation.Results;
using EnterpriseBusinessRules.Interfaces;
using EnterpriseBusinessRules.Validators;
using System;

namespace EnterpriseBusinessRules.Entities
{
    public class PaymentFilter: IEntity
    {
        public Guid? Id { get; set; }

        public long MerchantId { get; set; }

        public string CreditCardNumber { get; set; }

        public decimal AmountFrom { get; set; }

        public decimal AmountTo { get; set; }

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
