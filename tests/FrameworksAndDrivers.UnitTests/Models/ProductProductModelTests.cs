using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using FrameworksAndDrivers.Database.Models;
using FrameworksAndDrivers.UnitTests.Helpers;
using Xunit;
using Xunit.Abstractions;

namespace FrameworksAndDrivers.UnitTests.Models
{
    public class ProductProductModelTests
    {
        private readonly ITestOutputHelper _output;

        public ProductProductModelTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        [Trait("Model", "ProductProduct")]
        public void ShouldReturnValidOfTheProductProduct()
        {
            // Arrange && Act
            var productProduct = new ProductProductModel
            {
                Id = TestHelpers.GenerateId(),
                ParentId = TestHelpers.RandomString(8),
                ProductId = TestHelpers.RandomString(8),
                Flags = new Dictionary<string, bool>(),
                Others = new Dictionary<string, object>(),
                Parent = new CreditCardModel(),
                Product = new CreditCardModel()
            };

            // Assert
            var result = TestValidation.getValidationErros(productProduct);
            result.Count().Should().Be(0);

            productProduct.Should().BeOfType<ProductProductModel>();
            productProduct.Should().NotBeNull();
            productProduct.Id.Should().NotBeEmpty();
        }

        [Fact]
        [Trait("Model", "ProductProduct")]
        public void ThereShould4ErrorMessagesFromRequiredFieldOfTheProductProduct()
        {
            // Arrange && Act
            var productProduct = new ProductProductModel();

            // Assert
            var result = TestValidation.getValidationErros(productProduct);
            result.Count().Should().Be(4);
        }

        [Theory]
        [InlineData(9, 8, "The field ParentId must be a string or array type with a maximum length of '8'.")]
        [InlineData(8, 9, "The field ProductId must be a string or array type with a maximum length of '8'.")]
        [InlineData(0, 8, "The ParentId field is required.")]
        [InlineData(8, 0, "The ProductId field is required.")]
        [Trait("Model", "ProductProduct")]
        public void ShouldReturnTheErrorMessageWhenTheProductProductIsWrong(
            int parentIdSize,
            int productIdSize,
            string messageExpected)
        {
            // Arrange && Act
            var productProduct = new ProductProductModel
            {
                Id = TestHelpers.GenerateId(),
                ParentId = TestHelpers.RandomString(parentIdSize),
                ProductId = TestHelpers.RandomString(productIdSize),
                Flags = new Dictionary<string, bool>(),
                Others = new Dictionary<string, object>(),
                Parent = new CreditCardModel(),
                Product = new CreditCardModel()
            };

            // Assert
            var result = TestValidation.getValidationErros(productProduct);
            result.FirstOrDefault().ErrorMessage.Trim().ToLower().Should().Be(messageExpected.ToLower());
        }

    }
}
