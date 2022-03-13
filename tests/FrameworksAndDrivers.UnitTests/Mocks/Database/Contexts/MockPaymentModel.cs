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
                MerchantId = 1,
                CreditCardId = 1,
                CreditCard = new CreditCardModel()
                {
                    Id = 1,
                    Number = "379354508162306",
                    HolderName = "Antônio J. Penteado",
                    HolderAddress = "Heroic St. 195",
                    ExpirationMonth = "12",
                    ExpirationYear = "2025",
                    Cvv = "323",
                    StatusId = 1
                },
                Amount = 15.99m,
                Currency = "EUR",
                SaleDescription = "Final soccer match",
                StatusId = 1
            }
        };
    }
}
