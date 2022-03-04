using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using FrameworksAndDrivers.Database.Models;
using FrameworksAndDrivers.UnitTests.Helpers;
using Xunit;
using Xunit.Abstractions;

namespace FrameworksAndDrivers.UnitTests.Models
{
    public class ProductCapitalModelTests
    {
        private readonly ITestOutputHelper _output;

        public ProductCapitalModelTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact(DisplayName = "Should return valid of the capital")]
        [Trait("Model", "ProductCapital")]
        public void ShouldReturnValidOfTheProductCapital()
        {
            // Arrange && Act
            var productCapital = new PaymentModel
            {
                Id = TestHelpers.GenerateId(),
                ClientId = TestHelpers.RandomString(255),
                ProductId = TestHelpers.RandomString(8),
                Minimum = 5_000.00f,
                Maximum = 10_000.00f,
                Flags = new Dictionary<string, bool>(),
                Others = new Dictionary<string, object>(),
                Product = new CreditCardModel(),
                Client = new ProductClientModel()
            };

            // Assert
            var result = TestValidation.getValidationErros(productCapital);
            result.Should().NotBeNull();
            result.Count().Should().Be(0);

            productCapital.Should().BeOfType<PaymentModel>();
            productCapital.Should().NotBeNull();
            productCapital.Id.Should().NotBeEmpty();
        }

        [Fact]
        [Trait("Model", "ProductCapital")]
        public void ThereShould4ErrorMessagesFromRequiredFieldOfTheProductCapital()
        {
            // Arrange && Act
            var productCapital = new PaymentModel();

            // Assert
            var result = TestValidation.getValidationErros(productCapital);
            result.Count().Should().Be(4);
        }

        [Theory]
        [InlineData(300, 8, "The field ClientId must be a string or array type with a maximum length of '255'.")]
        [InlineData(255, 9, "The field ProductId must be a string or array type with a maximum length of '8'.")]
        [InlineData(0, 8, "The ClientId field is required.")]
        [InlineData(255, 0, "The ProductId field is required.")]
        [Trait("Model", "ProductCapital")]
        public void ShouldReturnTheErrorMessageWhenTheProductCapitalIsWrong(
            int clientIdSize,
            int productIdSize,
            string messageExpected)
        {
            // Arrange && Act
            var productCapital = new PaymentModel
            {
                Id = TestHelpers.GenerateId(),
                ClientId = TestHelpers.RandomString(clientIdSize),
                ProductId = TestHelpers.RandomString(productIdSize),
                Minimum = 5_000.00f,
                Maximum = 10_000.00f,
                Flags = new Dictionary<string, bool>(),
                Others = new Dictionary<string, object>(),
                Product = new CreditCardModel(),
                Client = new ProductClientModel()
            };

            // Assert
            var result = TestValidation.getValidationErros(productCapital);
            result.Should().NotBeNull();
            result.FirstOrDefault().ErrorMessage.ToLower().Should().Be(messageExpected.ToLower());
        }        
    }
}