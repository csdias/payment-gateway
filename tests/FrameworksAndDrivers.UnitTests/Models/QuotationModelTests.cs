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
    public class QuotationModelTests
    {
        private readonly ITestOutputHelper _output;

        public QuotationModelTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        [Trait("Model", "Quotation")]
        public void ShouldReturnValidOfTheQuotation()
        {
            // Arrange && Act
            var quotation = new QuotationModel
            {
                Id = TestHelpers.GenerateId(),
                Code = TestHelpers.RandomString(255),
                ClientId = TestHelpers.RandomString(255),
                ProductId = TestHelpers.RandomString(8),
                Description = TestHelpers.RandomString(255),
                Capital = 1_000F,
                Information = new Dictionary<string, object>(),
                Parameters = new Dictionary<string, object>(),
                Settings = new QuotationSettingsEntity(),
                Price = new QuotationPriceEntity(),
                Product = new ProductQuotationEntity()
            };

            // Assert
            var result = TestValidation.getValidationErros(quotation);
            result.Count().Should().Be(0);
            
            quotation.Should().BeOfType<QuotationModel>();
            quotation.Should().NotBeNull();
            quotation.Id.Should().NotBeEmpty();
        }

        [Fact]
        [Trait("Model", "Quotation")]
        public void ThereShould7ErrorMessagesFromRequiredFieldOfTheQuotation()
        {
            // Arrange && Act
            var quotation = new QuotationModel();

            // Assert
            var result = TestValidation.getValidationErros(quotation);
            result.Count().Should().Be(7);
        }

        [Theory]
        [InlineData(256, 255, 8, 255, "The field Code must be a string or array type with a maximum length of '255'.")]
        [InlineData(255, 256, 8, 255, "The field ClientId must be a string or array type with a maximum length of '255'.")]
        [InlineData(255, 255, 9, 255, "The field ProductId must be a string or array type with a maximum length of '8'.")]
        [InlineData(255, 255, 8, 256, "The field Description must be a string or array type with a maximum length of '255'.")]
        [InlineData(255, 0, 8, 255, "The ClientId field is required.")]
        [InlineData(255, 255, 0, 255, "The ProductId field is required.")]
        [Trait("Model", "Quotation")]
        public void ShouldReturnTheErrorMessageWhenTheQuotationIsWrong(
            int codeSize,
            int clientcodeSize,
            int productcodeSize,
            int descriptionSize,
            string messageExpected)
        {
            // Arrange && Act
            var quotation = new QuotationModel
            {
                Id = TestHelpers.GenerateId(),
                Code = TestHelpers.RandomString(codeSize),
                ClientId = TestHelpers.RandomString(clientcodeSize),
                ProductId = TestHelpers.RandomString(productcodeSize),
                Description = TestHelpers.RandomString(descriptionSize),
                Capital = 1_000F,
                Information = new Dictionary<string, object>(),
                Parameters = new Dictionary<string, object>(),
                Settings = new QuotationSettingsEntity(),
                Price = new QuotationPriceEntity(),
                Product = new ProductQuotationEntity()
            };

            // Assert
            var result = TestValidation.getValidationErros(quotation);
            result.FirstOrDefault().ErrorMessage.Trim().ToLower().Should().Be(messageExpected.ToLower());
        }
    }
}
