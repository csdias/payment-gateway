using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using FrameworksAndDrivers.Database.Models;
using FrameworksAndDrivers.UnitTests.Helpers;
using Xunit;
using Xunit.Abstractions;

namespace FrameworksAndDrivers.UnitTests.Models
{
    public class ProductGroupModelTests
    {
        private readonly ITestOutputHelper _output;

        public ProductGroupModelTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        [Trait("Model", "ProductGroup")]
        public void ShouldReturnValidOfTheProductGroup()
        {
            // Arrange && Act
            var productGroup = new ProductGroupModel
            {
                Id = TestHelpers.RandomString(8),
                Name = TestHelpers.RandomString(255),
                Formula = TestHelpers.RandomString(255),
                FormulaWithoutCapital = TestHelpers.RandomString(255),
                Fields = new List<string>(),
                Defaults = new Dictionary<string, object>(),
                Flags = new Dictionary<string, bool>(),
                Others = new Dictionary<string, object>()
            };

            // Assert
            var result = TestValidation.getValidationErros(productGroup);
            result.Count().Should().Be(0);
        }

        [Fact]
        [Trait("Model", "ProductGroup")]
        public void ThereShould6ErrorMessagesFromRequiredFieldOfTheProductGroup()
        {
            // Arrange && Act
            var productGroup = new ProductGroupModel();

            // Assert
            var result = TestValidation.getValidationErros(productGroup);
            result.Count().Should().Be(6);
        }

        [Theory]
        [InlineData(9, 255, 255, 255, 255, "The field Id must be a string or array type with a maximum length of '8'.")]
        [InlineData(8, 256, 255, 255, 255, "The field Name must be a string or array type with a maximum length of '255'.")]
        [InlineData(8, 255, 256, 255, 255, "The field Description must be a string or array type with a maximum length of '255'.")]
        [InlineData(8, 255, 255, 256, 255, "The field Formula must be a string or array type with a maximum length of '255'.")]
        [InlineData(8, 255, 255, 255, 256, "The field FormulaWithoutCapital must be a string or array type with a maximum length of '255'.")]
        [InlineData(0, 255, 255, 255, 255, "The Id field is required.")]
        [InlineData(8, 0, 255, 255, 255, "The Name field is required.")]
        [InlineData(8, 255, 255, 0, 255, "The Formula field is required.")]
        [InlineData(8, 255, 255, 255, 0, "The FormulaWithoutCapital field is required.")]
        [Trait("Model", "ProductGroup")]
        public void ShouldReturnTheErrorMessageWhenTheProductGroupIsWrong(
            int idSize,
            int nameSize,
            int descriptionSize,
            int formulaSize,
            int formulaWithoutCapitalSize,
            string messageExpected)
        {
            // Arrange && Act
            var productGroup = new ProductGroupModel
            {
                Id = TestHelpers.RandomString(idSize),
                Name = TestHelpers.RandomString(nameSize),
                Description = TestHelpers.RandomString(descriptionSize),
                Formula = TestHelpers.RandomString(formulaSize),
                FormulaWithoutCapital = TestHelpers.RandomString(formulaWithoutCapitalSize),
                Fields = new List<string>(),
                Defaults = new Dictionary<string, object>(),
                Flags = new Dictionary<string, bool>(),
                Others = new Dictionary<string, object>()
            };

            // Assert
            var result = TestValidation.getValidationErros(productGroup);
            result.FirstOrDefault().ErrorMessage.Trim().ToLower().Should().Be(messageExpected.ToLower());
        }
    }
}
