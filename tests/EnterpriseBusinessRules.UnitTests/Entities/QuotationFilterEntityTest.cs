using Xunit;
using FluentAssertions;
using FluentValidation.Results;
using EnterpriseBusinessRules.UnitTests.Helpers;
using EnterpriseBusinessRules.Entities;

namespace EnterpriseBusinessRules.UnitTests.Entities
{
    public class QuotationFilterEntityTests
    {
        public QuotationFilterEntity CreateQuotationFilterEntity()
        {
            return new QuotationFilterEntity()
            {
                Code = TestHelpers.RandomString(8),
                Client = TestHelpers.RandomString(7),
                Product = TestHelpers.RandomString(7),
                Description = TestHelpers.RandomString(240)
            };
        }

        [Fact]
        public void ValidateEntityCreationAndFieldsValues() 
        {
            var quotationFilterEntity = CreateQuotationFilterEntity();
            
            quotationFilterEntity.Code.Should().BeOfType<string>();
            quotationFilterEntity.Client.Should().BeOfType<string>();
            quotationFilterEntity.Product.Should().BeOfType<string>();
            quotationFilterEntity.Description.Should().BeOfType<string>();
            quotationFilterEntity.Should().BeOfType<QuotationFilterEntity>();
        }
        
        [Fact]
        public void ValidateMethodShouldBeCalled()
        {
            var quotationFilterEntity = CreateQuotationFilterEntity();

            quotationFilterEntity.Validate().IsValid.Should().BeTrue();
            quotationFilterEntity.Validate().Should().BeOfType<ValidationResult>();
        }
    }
}
