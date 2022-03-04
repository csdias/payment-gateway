using System.Collections.Generic;
using EnterpriseBusinessRules.Entities;

namespace EnterpriseBusinessRules.UnitTests.Mocks.Entities
{
    public class MockProductGroupEntity
    {

        public readonly static List<ProductGroupEntity> Data = new List<ProductGroupEntity> 
        {
            new ProductGroupEntity
            {
                Id = "DEFAULT",
                Name = "Regra padrão",
                Description = "Regra padrão",
                Formula = "({rate}*{capital})/1000",
                FormulaWithoutCapital = "({amount}*1000)/{rate}",
                Fields = new List<string> { "age", "gender" },
                Defaults = new Dictionary<string, object> {{"class", "STANDARD"}},
                Others = new Dictionary<string, object>()
            }
        };
      
    }
}
