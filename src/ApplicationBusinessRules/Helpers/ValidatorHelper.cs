using EnterpriseBusinessRules.Entities;
using EnterpriseBusinessRules.Interfaces;

namespace ApplicationBusinessRules.Helpers
{
    public class ValidatorHelper
    {
        public static Response<T> ValidateEntity<T>(IEntity item)
        {
            var validate = item.Validate();
            if(validate.IsValid) {
                return new Response<T>()
                    .SetSuccess(true);
            }
            return new Response<T>()
                .SetSuccess(false)
                .SetMessages(validate);    
        }
    }
}
