using System.Collections.Generic;
using FrameworksAndDrivers.Database.Models;

namespace FrameworksAndDrivers.UnitTests.Mocks.Database.Contexts
{
    public class MockProductClientModel
    {
        public readonly static List<ProductClientModel> Data = new List<ProductClientModel>
        {
            new ProductClientModel 
            {
                Id = "A953DC88EB1B350CE0532C118C0A2285",
                Name = "Life Planner",
                Description = "Life Planner",
                Others = new Dictionary<string, object>()
            }
        };
    }
}