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
            var payment = new Payment
            {
                Ammount = 15,
                ClientId = TestHelpers.RandomString(255),
                CreditCard = new CreditCard()
                {
                    Number = TestHelpers.RandomString(16),
                    Cvv = TestHelpers.RandomString(3),
                    ExpirationMonth = "12",
                    ExpirationYear = "2025"
                },
                Id = Guid.NewGuid()
            };

            // Act
            var validation = payment.Validate();

            // Assert 
            validation.IsValid.Should().BeTrue();
        }
    }
}
