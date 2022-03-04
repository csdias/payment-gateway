using System.Collections.Generic;
using Xunit;
using FluentAssertions;
using FluentValidation.Results;
using EnterpriseBusinessRules.UnitTests.Helpers;
using EnterpriseBusinessRules.Entities;

namespace EnterpriseBusinessRules.UnitTests.Entities
{
    public class QuotationTaxesEntityTests
    {
        public QuotationTaxesEntity CreateQuotationTaxesEntity()
        {
            return new QuotationTaxesEntity()
            {
                Iof = 10F,
                Factor = 10F
            };
        }

        [Fact]
        public void ValidateEntityCreationAndFieldsValues() 
        {
            var quotationTaxesEntity = CreateQuotationTaxesEntity();
            
            quotationTaxesEntity.Iof.Should().Be(10F);
            quotationTaxesEntity.Factor.Should().Be(10F);
            quotationTaxesEntity.Should().BeOfType<QuotationTaxesEntity>();
        }
        
        [Fact]
        public void ValidateMethodShouldBeCalled()
        {
            var quotationTaxesEntity = CreateQuotationTaxesEntity();

            quotationTaxesEntity.Validate().IsValid.Should().BeTrue();
            quotationTaxesEntity.Validate().Should().BeOfType<ValidationResult>();
        }
    }
}
