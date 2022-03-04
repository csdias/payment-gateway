using System;
using System.Linq;
using System.Collections.Generic;
using FrameworksAndDrivers.Database.Models;

namespace FrameworksAndDrivers.UnitTests.Mocks.Database.Contexts
{
    public class MockProductCapitalModel
    {     
        public readonly static IList<PaymentModel> Data = new List<PaymentModel>
        {
            new PaymentModel
            {
                Id = Guid.Parse("fc782e65-0117-4c5e-b6d0-afa845effa3e"),
                ClientId = MockProductClientModel.Data.FirstOrDefault().Id,
                ProductId = MockProductModel.Data.FirstOrDefault(a => a.Id == "WLUPF").Id,
                Minimum = 10_000.00f,
                Maximum = 300_000.00f,
                Others = new Dictionary<string, object>(),
            },
            new PaymentModel
            {
                Id = Guid.Parse("a114b1d6-1d17-401d-a6e7-4c696e15b596"),
                ClientId = MockProductClientModel.Data.FirstOrDefault().Id,
                ProductId = MockProductModel.Data.FirstOrDefault(a => a.Id == "AB05F").Id,
                Minimum = 5_000.00f,
                Maximum = 25_000.00f,
                Others = new Dictionary<string, object>(),
            }
        };
    }
}