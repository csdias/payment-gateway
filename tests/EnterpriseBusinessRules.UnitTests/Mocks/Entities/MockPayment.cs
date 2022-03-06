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
                ClientId = "A953DC88EB1B350CE0532C118C0A2285",
                CreditCard = new CreditCard {
                    Number = "",
                    Name = "Test",
                    ExpirationMonth = "",
                    ExpirationYear = "",
                    Cvv = ""
                },
                Ammount = 1_272.50m,
                StatusId = 1
            }
        };
    }
}
