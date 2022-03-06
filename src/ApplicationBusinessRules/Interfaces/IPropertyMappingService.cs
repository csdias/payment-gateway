using System.Collections.Generic;
using EnterpriseBusinessRules.Entities;
using EnterpriseBusinessRules.Entities.ResourceParameters;

namespace ApplicationBusinessRules.Interfaces
{
    public interface IPropertyMappingService
    {
        Dictionary<string, PropertyMappingValue> GetPropertyMapping<TSource, TDestination>();
        bool ValidMappingExistsFor<TSource, TDestination>(string fields);
    }
}
