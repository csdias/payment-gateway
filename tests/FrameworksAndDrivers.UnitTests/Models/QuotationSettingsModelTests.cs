using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using FrameworksAndDrivers.Database.Models;
using FrameworksAndDrivers.UnitTests.Helpers;
using Xunit;
using Xunit.Abstractions;

namespace FrameworksAndDrivers.UnitTests.Models
{
    public class QuotationSettingsModelTests
    {
        private readonly ITestOutputHelper _output;

        public QuotationSettingsModelTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        [Trait("Model", "QuotationSettings")]
        public void ShouldReturnValidOfTheQuotationSettings()
        {
            // Arrange && Act
            var quotationSettings = new QuotationSettingsModel
            {
                Id = TestHelpers.RandomString(255),
                Settings = new Dictionary<string, object>()
            };

            // Assert
            var result = TestValidation.getValidationErros(quotationSettings);
            result.Count().Should().Be(0);
        }

        [Fact]
        [Trait("Model", "QuotationSettings")]
        public void ThereShould1ErrorMessagesFromRequiredFieldOfTheQuotationSettings()
        {
            // Arrange && Act
            var quotationSettings = new QuotationSettingsModel();

            // Assert
            var result = TestValidation.getValidationErros(quotationSettings);
            result.Count().Should().Be(1);
        }

        [Theory]
        [InlineData(256, "The field Id must be a string or array type with a maximum length of '255'.")]
        [InlineData(0, "The Id field is required.")]
        [Trait("Model", "QuotationSettings")]
        public void ShouldReturnTheErrorMessageWhenTheQuotationSettingsIsWrong(
            int idSize,
            string messageExpected)
        {
            // Arrange && Act
            var quotationSettings = new QuotationSettingsModel
            {
                Id = TestHelpers.RandomString(idSize),
                Settings = new Dictionary<string, object>()
            };

            // Assert
            var result = TestValidation.getValidationErros(quotationSettings);
            result.FirstOrDefault().ErrorMessage.Trim().ToLower().Should().Be(messageExpected.ToLower());
        }
    }
}
