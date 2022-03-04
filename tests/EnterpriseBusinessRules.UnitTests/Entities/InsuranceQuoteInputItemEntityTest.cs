using System.Collections.Generic;
using Xunit;
using FluentAssertions;
using FluentValidation.Results;
using EnterpriseBusinessRules.UnitTests.Helpers;
using EnterpriseBusinessRules.Entities;

namespace EnterpriseBusinessRules.UnitTests.Entities
{
    public class InsuranceQuoteInputItemEntityTests
    {
        public InsuranceQuoteInputItemEntity CreateInsuranceQuoteInputItemEntity()
        {
            return new InsuranceQuoteInputItemEntity()
            {
                Product = TestHelpers.RandomString(7),
                Amount = new InsuranceQuoteInputAmountEntity()
                {
                    Type = "monthly",
                    Amount = 1000F
                },
            };
        }

        [Fact]
        public void ValidateEntityCreationAndFieldsValues() 
        {
            var insuranceQuoteInputItemEntity = CreateInsuranceQuoteInputItemEntity();
            
            insuranceQuoteInputItemEntity.Product.Should().BeOfType<string>();
            insuranceQuoteInputItemEntity.Amount.Should().BeOfType<InsuranceQuoteInputAmountEntity>();
            insuranceQuoteInputItemEntity.Should().BeOfType<InsuranceQuoteInputItemEntity>();
        }
        
        [Fact]
        public void ValidateMethodShouldBeCalled()
        {
            var insuranceQuoteInputItemEntity = CreateInsuranceQuoteInputItemEntity();

            insuranceQuoteInputItemEntity.Validate().IsValid.Should().BeTrue();
            insuranceQuoteInputItemEntity.Validate().Should().BeOfType<ValidationResult>();
        }
    }
}
