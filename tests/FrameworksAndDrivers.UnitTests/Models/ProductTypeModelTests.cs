using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using FrameworksAndDrivers.Database.Models;
using FrameworksAndDrivers.UnitTests.Helpers;
using Xunit;
using Xunit.Abstractions;

namespace FrameworksAndDrivers.UnitTests.Models
{
    public class ProductTypeModelTests
    {
        private readonly ITestOutputHelper _output;

        public ProductTypeModelTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        [Trait("Model", "ProductType")]
        public void ShouldReturnValidOfTheProductType()
        {
            // Arrange && Act
            var productType = new ProductTypeModel
            {
                Id = TestHelpers.RandomString(255),
                Name = TestHelpers.RandomString(255),
                Flags = new Dictionary<string, bool>(),
                Others = new Dictionary<string, object>()
            };

            // Assert
            var result = TestValidation.getValidationErros(productType);
            result.Count().Should().Be(0);
        }

        [Fact]
        [Trait("Model", "ProductType")]
        public void ThereShould2ErrorMessagesFromRequiredFieldOfTheProductType()
        {
            // Arrange && Act
            var productType = new ProductTypeModel();

            // Assert
            var result = TestValidation.getValidationErros(productType);
            result.Count().Should().Be(2);
        }

        [Theory]
        [InlineData(300, 255, 255, "The field Id must be a string or array type with a maximum length of '255'.")]
        [InlineData(255, 300, 255, "The field Name must be a string or array type with a maximum length of '255'.")]
        [InlineData(255, 255, 300, "The field Description must be a string or array type with a maximum length of '255'.")]
        [InlineData(0, 255, 255, "The Id field is required.")]
        [InlineData(255, 0, 255, "The Name field is required.")]
        [Trait("Model", "ProductType")]
        public void ShouldReturnTheErrorMessageWhenTheProductTypeIsWrong(
            int idSize,
            int nameSize,
            int descriptionSize,
            string messageExpected)
        {
            // Arrange && Act
            var productType = new ProductTypeModel
            {
                Id = TestHelpers.RandomString(idSize),
                Name = TestHelpers.RandomString(nameSize),
                Description = TestHelpers.RandomString(descriptionSize),
                Flags = new Dictionary<string, bool>(),
                Others = new Dictionary<string, object>()
            };

            // Assert
            var result = TestValidation.getValidationErros(productType);
            result.FirstOrDefault().ErrorMessage.Trim().ToLower().Should().Be(messageExpected.ToLower());
        }
    }
}
