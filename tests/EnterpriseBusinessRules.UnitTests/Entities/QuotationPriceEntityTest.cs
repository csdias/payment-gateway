using System.Collections.Generic;
using Xunit;
using FluentAssertions;
using FluentValidation.Results;
using EnterpriseBusinessRules.UnitTests.Helpers;
using EnterpriseBusinessRules.Entities;

namespace EnterpriseBusinessRules.UnitTests.Entities
{
    public class QuotationPriceEntityTests
    {
        public QuotationPriceEntity CreateQuotationPriceEntity()
        {
            return new QuotationPriceEntity()
            {
                Year = 10000F,
                Month = 120000F
            };
        }

        [Fact]
        public void ValidateEntityCreationAndFieldsValues() 
        {
            var quotationPriceEntity = CreateQuotationPriceEntity();
            
            quotationPriceEntity.Year.Should().Be(10000F);
            quotationPriceEntity.Month.Should().Be(120000F);
            quotationPriceEntity.Should().BeOfType<QuotationPriceEntity>();
        }
        
        [Fact]
        public void ValidateMethodShouldBeCalled()
        {
            var quotationPriceEntity = CreateQuotationPriceEntity();

            quotationPriceEntity.Validate().IsValid.Should().BeTrue();
            quotationPriceEntity.Validate().Should().BeOfType<ValidationResult>();
        }
    }
}
