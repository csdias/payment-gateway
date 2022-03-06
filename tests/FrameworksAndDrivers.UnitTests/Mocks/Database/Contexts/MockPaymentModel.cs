using System;
using System.Collections.Generic;
using FrameworksAndDrivers.Database.Models;

namespace FrameworksAndDrivers.UnitTests.Mocks.Database.Contexts
{
    public class MockPaymentModel
    {
        public readonly static List<PaymentModel> Data = new List<PaymentModel>
        {
            new PaymentModel
            {
                Id = Guid.Parse("fc782e65-0117-4c5e-b6d0-afa845effa3e"),
                ClientId = "A953DC88EB1B350CE0532C118C0A2285",
                CreditCard = new Dictionary<string, object>() {
                    {"name", "CARLOS SOARES DIAS"},
                    {"expiration-month", 12},
                    {"expiration-year", 2028}
                },
                Ammount = 1_272.50m,
                StatusId = 1
            },
            new PaymentModel
            {
                Id = Guid.Parse("a114b1d6-1d17-401d-a6e7-4c696e15b596"),
                ClientId = "A953DC88EB1B350CE0532C118C0A2285",
                CreditCard = new Dictionary<string, object>() {
                    {"name", "JHON SMITH"},
                    {"expiration-month", 07},
                    {"expiration-year", 2025}
                },
                Ammount = 3_422.70m,
                StatusId = 2
            }
        };
    }
}
