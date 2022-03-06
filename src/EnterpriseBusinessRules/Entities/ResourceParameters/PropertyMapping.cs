using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnterpriseBusinessRules.Entities.ResourceParameters
{
    public class PropertyMapping<TSource, TDestination>
    {
        public Dictionary<string, PropertyMappingValue> _mappingDictionary { get; private set; }

        public PropertyMapping(Dictionary<string, PropertyMappingValue> mappingDictionary)
        {
            _mappingDictionary = mappingDictionary ?? 
                throw new ArgumentNullException(nameof(mappingDictionary));
        }
    }
}
