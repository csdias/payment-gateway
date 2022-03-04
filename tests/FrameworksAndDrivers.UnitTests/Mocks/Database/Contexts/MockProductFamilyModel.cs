using System.Collections.Generic;
using FrameworksAndDrivers.Database.Models;

namespace FrameworksAndDrivers.UnitTests.Mocks.Database.Contexts
{
    public class MockProductFamilyModel
    {
        public readonly static List<ProductFamilyModel> Data = new List<ProductFamilyModel>
        {
            new ProductFamilyModel
            {
                Id = "ADBU",
                Name = "Morte Acidental",
                Description = "Morte Acidental",
                Others = new Dictionary<string, object>()
            },
             new ProductFamilyModel
            {
                Id = "WDU",
                Name = "Vida Inteira Modificado",
                Description = "Vida Inteira Modificado",
                Others = new Dictionary<string, object>()
            }
        };
    }
}