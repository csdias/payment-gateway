using FluentValidation.Results;

namespace EnterpriseBusinessRules.Interfaces
{
    public interface IEntity
    {
        public ValidationResult Validate();
    }
}
