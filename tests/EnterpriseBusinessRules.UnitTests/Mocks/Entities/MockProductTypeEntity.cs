using System.Collections.Generic;
using EnterpriseBusinessRules.Entities;

namespace EnterpriseBusinessRules.UnitTests.Mocks.Entities
{
    public class MockProductTypeEntity
    {
        public readonly static List<ProductTypeEntity> Data = new List<ProductTypeEntity> 
        {
            new ProductTypeEntity
            {
                Id = "BASIC",
                Name = "Cobertura Básica",
                Description = "Cobertura Básica",
                Others = new Dictionary<string, object>()
            },
            new ProductTypeEntity
            {
                Id = "ADDT",
                Name = "Cobertura Adicional",
                Description = "Cobertura Adicional",
                Others = new Dictionary<string, object>()
            }
        };      
    }
}
