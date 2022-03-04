using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using FrameworksAndDrivers.Database.Models;
using FrameworksAndDrivers.UnitTests.Helpers;
using Xunit;
using Xunit.Abstractions;

namespace FrameworksAndDrivers.UnitTests.Models
{
    public class ProductClientModelTests
    {
        private readonly ITestOutputHelper _output;

        public ProductClientModelTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        [Trait("Model", "ProductClient")]
        public void ShouldReturnValidOfTheProductClient()
        {
            // Arrange && Act
            var productClient = new ProductClientModel
            {
                Id = TestHelpers.RandomString(255),
                Name = TestHelpers.RandomString(255),
                Flags = new Dictionary<string, bool>(),
                Others = new Dictionary<string, object>()
            };

            // Assert
            var result = TestValidation.getValidationErros(productClient);
            result.Count().Should().Be(0);
        }

        [Fact]
        [Trait("Model", "ProductClient")]
        public void ThereShould2ErrorMessagesFromRequiredFieldOfTheProductClient()
        {
            // Arrange && Act
            var productClient = new ProductClientModel();

            // Assert
            var result = TestValidation.getValidationErros(productClient);
            result.Count().Should().Be(2);
        }

        [Theory]
        [InlineData(300, 255, 255, "The field Id must be a string or array type with a maximum length of '255'.")]
        [InlineData(255, 300, 255, "The field Name must be a string or array type with a maximum length of '255'.")]
        [InlineData(255, 255, 300, "The field Description must be a string or array type with a maximum length of '255'.")]
        [InlineData(0, 255, 255, "The Id field is required.")]
        [InlineData(255, 0, 255, "The Name field is required.")]
        [Trait("Model", "ProductClient")]
        public void ShouldReturnTheErrorMessageWhenTheProductClientIsWrong(
            int idSize,
            int nameSize,
            int descriptionSize,
            string messageExpected)
        {
            // Arrange && Act
            var productClient = new ProductClientModel
            {
                Id = TestHelpers.RandomString(idSize),
                Name = TestHelpers.RandomString(nameSize),
                Description = TestHelpers.RandomString(descriptionSize),
                Flags = new Dictionary<string, bool>(),
                Others = new Dictionary<string, object>()
            };

            // Assert
            var result = TestValidation.getValidationErros(productClient);
            result.FirstOrDefault().ErrorMessage.Trim().ToLower().Should().Be(messageExpected.ToLower());
        }
    }
}
