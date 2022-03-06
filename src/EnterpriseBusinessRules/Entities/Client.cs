using System.Collections.Generic;
using FluentValidation.Results;
using EnterpriseBusinessRules.Validators;
using EnterpriseBusinessRules.Interfaces;

namespace EnterpriseBusinessRules.Entities
{
    public class Client: IEntity
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }

        public ValidationResult Validate()
        {
            ClientValidator validator = new ClientValidator();
            return validator.Validate(this);
        }
    }
}
