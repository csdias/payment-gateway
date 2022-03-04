using System.Collections.Generic;
using Xunit;
using FluentAssertions;
using FluentValidation.Results;
using EnterpriseBusinessRules.UnitTests.Helpers;
using EnterpriseBusinessRules.Entities;

namespace EnterpriseBusinessRules.UnitTests.Entities
{
    public class InsuranceQuoteAmountEntityTests
    {
        public InsuranceQuoteInputAmountEntity CreateInsuranceQuoteAmountEntity()
        {
            return new InsuranceQuoteInputAmountEntity()
            {
                Type = "monthly",
                Amount = 1000F
            };
        }

        [Fact]
        public void ValidateEntityCreationAndFieldsValues() 
        {
            var InsuranceQuoteAmountEntity = CreateInsuranceQuoteAmountEntity();
            
            InsuranceQuoteAmountEntity.Type.Should().Equals("monthly");
            InsuranceQuoteAmountEntity.Amount.Should().Equals(1000F);
            InsuranceQuoteAmountEntity.Should().NotBeNull();
            InsuranceQuoteAmountEntity.Should().BeOfType<InsuranceQuoteInputAmountEntity>();
        }
        
        [Fact]
        public void ValidateMethodShouldBeCalled()
        {
            var InsuranceQuoteAmountEntity = CreateInsuranceQuoteAmountEntity();

            InsuranceQuoteAmountEntity.Validate().IsValid.Should().BeTrue();
            InsuranceQuoteAmountEntity.Validate().Should().BeOfType<ValidationResult>();
        }
    }
}
