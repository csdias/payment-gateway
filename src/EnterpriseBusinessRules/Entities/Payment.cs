using FluentValidation.Results;
using EnterpriseBusinessRules.Interfaces;
using EnterpriseBusinessRules.Validators;
using System;

namespace EnterpriseBusinessRules.Entities
{
    public class Payment: IEntity
    {
        public Guid? Id { get; set; }

        public long MerchantId { get; set; }

        public long CreditCardId { get; set; }

        public CreditCard CreditCard { get; set; }

        public string SaleDescription { get; set; }

        public decimal Amount { get; set; }

        public string Currency { get; set; }

        public short StatusId { get; set; }

        public ValidationResult Validate() 
        {
            PaymentValidator validator = new PaymentValidator();
            return validator.Validate(this);
        }
    }
}
