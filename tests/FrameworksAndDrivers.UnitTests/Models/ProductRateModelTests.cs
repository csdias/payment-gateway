using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using FrameworksAndDrivers.Database.Models;
using FrameworksAndDrivers.UnitTests.Helpers;
using Xunit;
using Xunit.Abstractions;

namespace FrameworksAndDrivers.UnitTests.Models
{
    public class ProductRateModelTests
    {
        private readonly ITestOutputHelper _output;

        public ProductRateModelTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        [Trait("Model", "ProductRate")]
        public void ShouldReturnValidOfTheProductRate()
        {
            // Arrange && Act
            var productRate = new ProductRateModel
            {
                Id = TestHelpers.GenerateId(),
                ProductId = TestHelpers.RandomString(8),
                Fields = new Dictionary<string, object>(),
                Flags = new Dictionary<string, bool>(),
                Others = new Dictionary<string, object>(),
                Rate = 1_000F,
                Product = new CreditCardModel()
            };

            // Assert
            var result = TestValidation.getValidationErros(productRate);
            result.Count().Should().Be(0);

            productRate.Should().BeOfType<ProductRateModel>();
            productRate.Should().NotBeNull();
            productRate.Id.Should().NotBeEmpty();
            productRate.Rate.Should().Be(1_000F);
        }

        [Fact]
        [Trait("Model", "ProductRate")]
        public void ThereShould3ErrorMessagesFromRequiredFieldOfTheProductRate()
        {
            // Arrange && Act
            var productRate = new ProductRateModel();

            // Assert
            var result = TestValidation.getValidationErros(productRate);
            result.Count().Should().Be(3);
        }

        [Theory]
        [InlineData(9, "The field ProductId must be a string or array type with a maximum length of '8'.")]
        [InlineData(0, "The ProductId field is required.")]
        [Trait("Model", "ProductRate")]
        public void ShouldReturnTheErrorMessageWhenTheProductRateIsWrong(
            int productIdSize,
            string messageExpected)
        {
            // Arrange && Act
            var productRate = new ProductRateModel
            {
                Id = TestHelpers.GenerateId(),
                ProductId = TestHelpers.RandomString(productIdSize),
                Fields = new Dictionary<string, object>(),
                Flags = new Dictionary<string, bool>(),
                Others = new Dictionary<string, object>(),
                Product = new CreditCardModel()
            };

            // Assert
            var result = TestValidation.getValidationErros(productRate);
            result.FirstOrDefault().ErrorMessage.Trim().ToLower().Should().Be(messageExpected.ToLower());
        }
    }
}
