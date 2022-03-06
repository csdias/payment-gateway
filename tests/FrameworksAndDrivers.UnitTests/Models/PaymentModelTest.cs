using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using EnterpriseBusinessRules.Entities;
using FrameworksAndDrivers.Database.Models;
using FrameworksAndDrivers.UnitTests.Helpers;
using Xunit;
using Xunit.Abstractions;

namespace FrameworksAndDrivers.UnitTests.Models
{
    public class PaymentModelTest
    {
        private readonly ITestOutputHelper _output;

        public PaymentModelTest(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
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

        [Fact]
        [Trait("Model", "Payment")]
        public void NumberOfRequiredFieldErrors_ShouldBe2()
        {
            // Arrange && Act
            var payment = new PaymentModel();

            // Assert
            var result = TestValidation.GetValidationErros(payment);
            result.Count().Should().Be(2);
        }

        [Theory]
        [InlineData(256, "The field ClientId must be a string or array type with a maximum length of '255'.")]
        [InlineData(0, "The ClientId field is required.")]
        [Trait("Model", "Payment")]
        public void Should_ReturnErrorMessages_WhenPaymentIsInvalid(
            int clientIdSize,
            string expectedMessage)
        {
            // Arrange && Act
            var payment = new PaymentModel
            {
                Id = TestHelpers.GenerateId(),
                ClientId = TestHelpers.RandomString(clientIdSize)
            };

            // Assert
            var result = TestValidation.GetValidationErros(payment);
            result.FirstOrDefault().ErrorMessage.Trim().ToLower().Should().Be(expectedMessage.ToLower());
        }
    }
}
