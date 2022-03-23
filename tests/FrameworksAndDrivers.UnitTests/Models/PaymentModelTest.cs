using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using EnterpriseBusinessRules.Entities;
using FrameworksAndDrivers.Database.Models;
using FrameworksAndDrivers.UnitTests.Helpers;
using Xunit;
using Xunit.Abstractions;
using System;

namespace FrameworksAndDrivers.UnitTests.Models
{
    public class PaymentModelTest
    {
        private readonly ITestOutputHelper _output;

        public PaymentModelTest(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact(Skip = "Just skip")]
        [Trait("Model", "Payment")]
        public void Should_BeOk()
        {
            // Arrange && Act
            var payment = new PaymentModel
            {

            };

            // Assert
            var result = TestValidation.GetValidationErros(payment);
            result.Count().Should().Be(0);

            payment.Should().BeOfType<PaymentModel>();
            payment.Should().NotBeNull();
            payment.Id.Should().NotBeEmpty();
        }

        [Fact(Skip = "Just skip")]
        [Trait("Model", "Payment")]
        public void NumberOfRequiredFieldErrors_ShouldBe2()
        {
            // Arrange && Act
            var payment = new PaymentModel();

            // Assert
            var result = TestValidation.GetValidationErros(payment);
            result.Count().Should().Be(2);
        }

        //[Theory]
        //[InlineData("", 1, "","The field Currency must be a string or array type with a maximum length of '3'.")]
        //[InlineData("EUR", 0, "", "The MerchantId field is required.")]
        //[Trait("Model", "Payment")]
        //public void Should_ReturnErrorMessages_WhenPaymentIsInvalid(
        //    string currency, int merchantId, string guid,
        //    string expectedMessage)
        //{
        //    // Arrange && Act
        //    var payment = new PaymentModel
        //    {
        //        Id = Guid.Parse(guid),
        //        Currency = currency,
        //        MerchantId = merchantId
        //    };

        //    // Assert
        //    var result = TestValidation.GetValidationErros(payment);
        //    result.FirstOrDefault().ErrorMessage.Trim().ToLower().Should().Be(expectedMessage.ToLower());
        //}
    }
}
