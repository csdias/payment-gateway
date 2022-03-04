using System.Collections.Generic;
using FrameworksAndDrivers.Database.Models;

namespace FrameworksAndDrivers.UnitTests.Mocks.Database.Contexts
{
    public class MockProductTypeModel
    {
        public readonly static List<ProductTypeModel> Data = new List<ProductTypeModel>
        {
            new ProductTypeModel
            {
                Id = "BASIC",
                Name = "Cobertura Básica",
                Description = "Cobertura Básica",
                Others = new Dictionary<string, object>()
            },
            new ProductTypeModel
            {
                Id = "ADDT",
                Name = "Cobertura Adicional",
                Description = "Cobertura Adicional",
                Others = new Dictionary<string, object>()
            }
        };
    }
}