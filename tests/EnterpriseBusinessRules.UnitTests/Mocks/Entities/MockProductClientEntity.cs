using System.Collections.Generic;
using EnterpriseBusinessRules.Entities;

namespace EnterpriseBusinessRules.UnitTests.Mocks.Entities
{
    public class MockProductClientEntity
    {

        public readonly static List<ProductClientEntity> Data = new List<ProductClientEntity> 
        {
            new ProductClientEntity 
            {
                Id = "A953DC88EB1B350CE0532C118C0A2285",
                Name = "Life Planner",
                Description = "Life Planner",
                Others = new Dictionary<string, object>()
            }
        };
      
    }
}