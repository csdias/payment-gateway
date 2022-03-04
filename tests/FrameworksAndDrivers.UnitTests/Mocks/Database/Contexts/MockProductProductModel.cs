using System;
using System.Linq;
using System.Collections.Generic;
using FrameworksAndDrivers.Database.Models;

namespace FrameworksAndDrivers.UnitTests.Mocks.Database.Contexts
{
    public class MockProductProductModel
    {
        public readonly static IList<ProductProductModel> Data = new List<ProductProductModel>
        {
            new ProductProductModel
            {
                Id = Guid.Parse("4e4c000c-3278-42f6-bc81-9d6d28a588f0"),
                ParentId = MockProductModel.Data.FirstOrDefault(a=>a.Id == "WLUPF").Id,
                ProductId = MockProductModel.Data.FirstOrDefault(a=>a.Id == "AB05F").Id,
                Others = new Dictionary<string, object>(),
                Parent = MockProductModel.Data.FirstOrDefault(a=>a.Id == "WLUPF"),
                Product = MockProductModel.Data.FirstOrDefault(a=>a.Id == "AB05F")
            },
            new ProductProductModel
            {
                Id = Guid.Parse("a8c86600-9604-46a5-b513-8e1cf393ce48"),
                ParentId = MockProductModel.Data.FirstOrDefault(a=>a.Id == "WLUPF").Id,
                ProductId = MockProductModel.Data.FirstOrDefault(a=>a.Id == "WLUPF").Id,
                Others = new Dictionary<string, object>(),
                Parent = MockProductModel.Data.FirstOrDefault(a=>a.Id == "WLUPF"),
                Product = MockProductModel.Data.FirstOrDefault(a=>a.Id == "WLUPF")
            },
            new ProductProductModel
            {
                Id = Guid.Parse("a8c86600-9604-46a5-b513-8e1cf393ce48"),
                ParentId = MockProductModel.Data.FirstOrDefault(a=>a.Id == "AB05F").Id,
                ProductId = MockProductModel.Data.FirstOrDefault(a=>a.Id == "AB05F").Id,
                Others = new Dictionary<string, object>(),
                Parent = MockProductModel.Data.FirstOrDefault(a=>a.Id == "AB05F"),
                Product = MockProductModel.Data.FirstOrDefault(a=>a.Id == "AB05F")
            }
        };
    }
}