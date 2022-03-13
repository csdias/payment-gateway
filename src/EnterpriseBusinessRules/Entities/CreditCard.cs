using FluentValidation.Results;
using EnterpriseBusinessRules.Validators;
using EnterpriseBusinessRules.Interfaces;

namespace EnterpriseBusinessRules.Entities
{
    public class CreditCard: IEntity
    {
        public long Id { get; set; }

        public string Number { get; set; }

        public string HolderName { get; set; }

        public string HolderAddress { get; set; }

        public string ExpirationMonth { get; set; }

        public string ExpirationYear { get; set; }

        public string Cvv { get; set; }

        public short StatusId { get; set; }

        public ValidationResult Validate() 
        {
            CreditCardValidator validator = new CreditCardValidator();
            return validator.Validate(this);
        }
    }
}
