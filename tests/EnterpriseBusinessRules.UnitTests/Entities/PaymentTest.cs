using System.Collections.Generic;
using Xunit;
using FluentAssertions;
using FluentValidation.Results;
using EnterpriseBusinessRules.UnitTests.Helpers;
using EnterpriseBusinessRules.Entities;
using System;

namespace EnterpriseBusinessRules.UnitTests.Entities
{
    public class PaymentTest
    {
        [Fact]
        public void Validate_Should_BeOk()
        {
            // Assert
            var payment = GetPayment();

            // Act
            var validation = payment.Validate();

            // Assert 
            validation.IsValid.Should().BeTrue();
        }

        private Payment GetPayment()
        {
            var payment = new Payment
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
                    Cvv = "323",
                    StatusId = 1
                },
                Amount = 15.99m,
                Currency = "EUR",
                SaleDescription = "Final soccer match",
                StatusId = 1
            };
            return payment;
        }
    }
}
