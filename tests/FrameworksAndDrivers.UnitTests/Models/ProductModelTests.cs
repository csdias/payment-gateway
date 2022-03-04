using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using FrameworksAndDrivers.Database.Models;
using FrameworksAndDrivers.UnitTests.Helpers;
using Xunit;
using Xunit.Abstractions;

namespace FrameworksAndDrivers.UnitTests.Models
{
    public class ProductModelTests
    {
        private readonly ITestOutputHelper _output;

        public ProductModelTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        [Trait("Model", "Product")]
        public void ShouldReturnValidOfTheProduct()
        {
            // Arrange && Act
            var product = new CreditCardModel
            {
                Id = TestHelpers.RandomString(8),
                Name = TestHelpers.RandomString(255),
                TypeId = TestHelpers.RandomString(8),
                GroupId = TestHelpers.RandomString(8),
                FamilyId = TestHelpers.RandomString(8),
                Flags = new Dictionary<string, bool>(),
                Others = new Dictionary<string, object>(),
                Type = new ProductTypeModel(),
                Group = new ProductGroupModel(),
                Family = new ProductFamilyModel()
            };

            // Assert
            var result = TestValidation.getValidationErros(product);
            result.Count().Should().Be(0);
        }

        [Fact]
        [Trait("Model", "Product")]
        public void ThereShould8ErrorMessagesFromRequiredFieldOfTheProduct()
        {
            // Arrange && Act
            var product = new CreditCardModel();

            // Assert
            var result = TestValidation.getValidationErros(product);
            result.Count().Should().Be(8);
        }

        [Theory]
        [InlineData(9, 255, 255, 8, 8, 8, "The field Id must be a string or array type with a maximum length of '8'.")]
        [InlineData(8, 300, 255, 8, 8, 8, "The field Name must be a string or array type with a maximum length of '255'.")]
        [InlineData(8, 255, 300, 8, 8, 8, "The field Description must be a string or array type with a maximum length of '255'.")]
        [InlineData(8, 255, 255, 9, 8, 8, "The field TypeId must be a string or array type with a maximum length of '8'.")]
        [InlineData(8, 255, 255, 8, 9, 8, "The field GroupId must be a string or array type with a maximum length of '8'.")]
        [InlineData(8, 255, 255, 8, 8, 9, "The field FamilyId must be a string or array type with a maximum length of '8'.")]
        [InlineData(0, 255, 255, 8, 8, 8, "The Id field is required.")]
        [InlineData(8, 0, 255, 8, 8, 8, "The Name field is required.")]
        [InlineData(8, 255, 255, 0, 8, 8, "The TypeId field is required.")]
        [InlineData(8, 255, 255, 8, 0, 8, "The GroupId field is required.")]
        [InlineData(8, 255, 255, 8, 8, 0, "The FamilyId field is required.")]
        [Trait("Model", "Product")]
        public void ShouldReturnTheErrorMessageWhenTheProductIsWrong(
            int idSize,
            int nameSize,
            int descriptionSize,
            int typeIdSize,
            int groupIdSize,
            int familyIdSize,
            string messageExpected)
        {
            // Arrange && Act
            var product = new CreditCardModel
            {
                Id = TestHelpers.RandomString(idSize),
                Name = TestHelpers.RandomString(nameSize),
                Description = TestHelpers.RandomString(descriptionSize),
                TypeId = TestHelpers.RandomString(typeIdSize),
                GroupId = TestHelpers.RandomString(groupIdSize),
                FamilyId = TestHelpers.RandomString(familyIdSize),
                Flags = new Dictionary<string, bool>(),
                Others = new Dictionary<string, object>(),
                Type = new ProductTypeModel(),
                Group = new ProductGroupModel(),
                Family = new ProductFamilyModel()
            };

            // Assert
            var result = TestValidation.getValidationErros(product);
            result.FirstOrDefault().ErrorMessage.Trim().ToLower().Should().Be(messageExpected.ToLower());
        }
    }
}
