using System.Linq;
using System.Collections.Generic;
using EnterpriseBusinessRules.Entities;

namespace EnterpriseBusinessRules.UnitTests.Mocks.Entities
{
    public class MockProductEntity
    {
        public readonly static List<ProductEntity> Data = new List<ProductEntity> 
        {
            new ProductEntity
            {
                Id = "WLUPF",
                Name = "Vida Inteira Único",
                Description = "Vida Inteira Único - Portfolio F - WLUPF",
                Others = new Dictionary<string, object>(),
                Type = MockProductTypeEntity.Data.FirstOrDefault(a=>a.Id == "BASIC"),
                Group = MockProductGroupEntity.Data.FirstOrDefault(a => a.Id == "DEFAULT"),
                Family = MockProductFamilyEntity.Data.FirstOrDefault(a => a.Id == "WDU"),
                Capital = new ProductCapitalEntity { Minimum = 0, Maximum = 0},
                Rates = new List<ProductRateEntity>()                    
            },
            new ProductEntity
            {
                Id = "AB05F",
                Name = "Morte Acidental por 5 anos",
                Description = "Morte Acidental por 5 anos - Portfolio F - AB05F",
                Others = new Dictionary<string, object>(),
                Type = MockProductTypeEntity.Data.FirstOrDefault(a=>a.Id == "ADDT"),
                Group = MockProductGroupEntity.Data.FirstOrDefault(a => a.Id == "DEFAULT"),
                Family = MockProductFamilyEntity.Data.FirstOrDefault(a => a.Id == "ADBU"),
                Capital = new ProductCapitalEntity { Minimum = 0, Maximum = 0},
                Rates = new List<ProductRateEntity>()            
            }
        };      
    }
}
