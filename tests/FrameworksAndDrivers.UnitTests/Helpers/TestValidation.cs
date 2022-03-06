using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FrameworksAndDrivers.UnitTests.Helpers
{
    public static class TestValidation
    {
        public static IEnumerable<ValidationResult> GetValidationErros(object obj)
        {
            var validationResult = new List<ValidationResult>();
            var context = new ValidationContext(obj, null, null);
            Validator.TryValidateObject(obj, context, validationResult, true);
            return validationResult;
        }
    }
}
