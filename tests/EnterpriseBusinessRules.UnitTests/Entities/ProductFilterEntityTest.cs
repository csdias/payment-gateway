using System.Collections.Generic;
using Xunit;
using FluentAssertions;
using FluentValidation.Results;
using EnterpriseBusinessRules.UnitTests.Helpers;
using EnterpriseBusinessRules.Entities;

namespace EnterpriseBusinessRules.UnitTests.Entities
{
    public class ProductFilterEntityTests
    {
        public PaymentFilterEntity CreateProductFilterEntity()
        {
            return new PaymentFilterEntity()
            {
                TypeId = TestHelpers.RandomString(8),
                GroupId = TestHelpers.RandomString(8),
                ProductId = TestHelpers.RandomString(8),
                ClientId = TestHelpers.RandomString(7)
            };
        }

        [Fact]
        public void ValidateEntityCreationAndFieldsValues() 
        {
            var ProductFilterEntity = CreateProductFilterEntity();
            
            ProductFilterEntity.TypeId.Should().BeOfType<string>();
            ProductFilterEntity.GroupId.Should().BeOfType<string>();
            ProductFilterEntity.ProductId.Should().BeOfType<string>();
            ProductFilterEntity.ClientId.Should().BeOfType<string>();
            ProductFilterEntity.Should().BeOfType<PaymentFilterEntity>();
        }
        
        [Fact]
        public void ValidateMethodShouldBeCalled()
        {
            var ProductFilterEntity = CreateProductFilterEntity();

            ProductFilterEntity.Validate().IsValid.Should().BeTrue();
            ProductFilterEntity.Validate().Should().BeOfType<ValidationResult>();
        }
    }
}
