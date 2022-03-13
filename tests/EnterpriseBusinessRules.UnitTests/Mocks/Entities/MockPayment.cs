using System;
using System.Collections.Generic;
using EnterpriseBusinessRules.Entities;

namespace EnterpriseBusinessRules.UnitTests.Mocks.Entities
{
    public class MockPayment
    {
        public readonly static List<Payment> Data = new List<Payment>
        {
            new Payment
            {
                Id = Guid.Parse("fc782e65-0117-4c5e-b6d0-afa845effa3e"),
                MerchantId = 1,
                CreditCardId = 1,
                CreditCard = new CreditCard()
                {
                    Id = 1,
                    Number = "379354508162306",
                    HolderName = "Antônio J. Penteado",
                    HolderAddress = "Heroic St. 195",
                    ExpirationMonth = "12",
                    ExpirationYear = "2025",
                    Cvv = "317",
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
