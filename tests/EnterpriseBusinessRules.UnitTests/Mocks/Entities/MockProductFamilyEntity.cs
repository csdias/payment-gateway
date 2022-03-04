using System.Collections.Generic;
using EnterpriseBusinessRules.Entities;

namespace EnterpriseBusinessRules.UnitTests.Mocks.Entities
{
    public class MockProductFamilyEntity
    {

        public readonly static List<ProductFamilyEntity> Data = new List<ProductFamilyEntity> 
        {
            new ProductFamilyEntity
            {
                Id = "ADBU",
                Name = "Morte Acidental",
                Description = "Morte Acidental",
                Others = new Dictionary<string, object>()
            },
             new ProductFamilyEntity
            {
                Id = "WDU",
                Name = "Vida Inteira Modificado",
                Description = "Vida Inteira Modificado",
                Others = new Dictionary<string, object>()
            }
        };
      
    }
}
