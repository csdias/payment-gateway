using System.Collections.Generic;
using Xunit;
using FluentAssertions;
using FluentValidation.Results;
using EnterpriseBusinessRules.UnitTests.Helpers;
using EnterpriseBusinessRules.Entities;

namespace EnterpriseBusinessRules.UnitTests.Entities
{
    public class QuotationProductRateEntityTests
    {
        public ProductQuotationRateEntity CreateQuotationProductRateEntity()
        {
            return new ProductQuotationRateEntity()
            {
                Rate = 10000F,
                Additional = 10,
                Total = 10010F
            };
        }

        [Fact]
        public void ValidateEntityCreationAndFieldsValues() 
        {
            var quotationProductRateEntity = CreateQuotationProductRateEntity();
            
            quotationProductRateEntity.Rate.Should().Be(10000F);
            quotationProductRateEntity.Additional.Should().Be(10F);
            quotationProductRateEntity.Total.Should().Be(10010F);
            quotationProductRateEntity.Should().BeOfType<ProductQuotationRateEntity>();
        }
        
        [Fact]
        public void ValidateMethodShouldBeCalled()
        {
            var quotationProductRateEntity = CreateQuotationProductRateEntity();

            quotationProductRateEntity.Validate().IsValid.Should().BeTrue();
            quotationProductRateEntity.Validate().Should().BeOfType<ValidationResult>();
        }
    }
}
