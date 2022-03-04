using System;
using System.Linq;
using System.Collections.Generic;
using FrameworksAndDrivers.Database.Models;

namespace FrameworksAndDrivers.UnitTests.Mocks.Database.Contexts
{
    public class MockProductRateModel
    {
        public readonly static IList<ProductRateModel> Data = new List<ProductRateModel>
        {
            new ProductRateModel
            {
                Id = Guid.Parse("f2ef44a1-7b02-4985-b948-d1a7416d3697"),
                ProductId = MockProductModel.Data.FirstOrDefault(a => a.Id == "WLUPF").Id,
                Fields = new Dictionary<string, object> { {"age", 22}, {"class", "STANDARD"}, {"gender", "F"}},
                Others = new Dictionary<string, object>(),
                Rate = 300.00f,
                Product = MockProductModel.Data.FirstOrDefault(a => a.Id == "WLUPF")
            },
            new ProductRateModel
            {
                Id = Guid.Parse("3fa5f8f2-4a58-43ba-a92f-0b9043be4912"),
                ProductId = MockProductModel.Data.FirstOrDefault(a => a.Id == "WLUPF").Id,
                Fields = new Dictionary<string, object> { {"age", 22}, {"class", "STANDARD"}, {"gender", "M"}},
                Others = new Dictionary<string, object>(),
                Rate = 200.00f,
                Product = MockProductModel.Data.FirstOrDefault(a => a.Id == "WLUPF")
            }
        };
    }
}