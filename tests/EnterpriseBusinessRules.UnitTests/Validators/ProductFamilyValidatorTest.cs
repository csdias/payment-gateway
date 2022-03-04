using Xunit;
using FluentAssertions;
using EnterpriseBusinessRules.UnitTests.Helpers;
using EnterpriseBusinessRules.Entities;

namespace EnterpriseBusinessRules.UnitTests.Validators
{
    public class ProductFamilyValidatorTests
    {

        [Fact]
        public void IdFieldShouldBeRequired()
        {
            var productFamilyEntity = new ProductFamilyEntity()
            {
                Id = TestHelpers.RandomString(8),
                Name = TestHelpers.RandomString(10)
            };

            productFamilyEntity.Validate().IsValid.Should().BeTrue();

            productFamilyEntity.Id = null;
            
            productFamilyEntity.Validate().Errors[0].ErrorMessage.Should().Contain("Id is required");

            productFamilyEntity.Id = TestHelpers.RandomString(8);

            productFamilyEntity.Validate().IsValid.Should().BeTrue();
        }

        [Fact]
        public void NameFieldShouldBeRequired()
        {
            var productFamilyEntity = new ProductFamilyEntity()
            {
                Id = TestHelpers.RandomString(8),
                Name = TestHelpers.RandomString(10)
            };

            productFamilyEntity.Validate().IsValid.Should().BeTrue();

            productFamilyEntity.Name = null;
            
            productFamilyEntity.Validate().Errors[0].ErrorMessage.Should().Contain("Name is required");

            productFamilyEntity.Name = TestHelpers.RandomString(7);

            productFamilyEntity.Validate().IsValid.Should().BeTrue();
        }

        [Theory]
        [InlineData(4)]
        [InlineData(256)]
        public void NameFieldCharacterQuantityHaveError(int quantity)
        {
            var productFamilyEntity = new ProductFamilyEntity()
            {
                Id = TestHelpers.RandomString(8),
                Name = TestHelpers.RandomString(7)
            };

            productFamilyEntity.Validate().IsValid.Should().BeTrue();

            productFamilyEntity.Name = TestHelpers.RandomString(quantity);;
            
            productFamilyEntity.Validate().Errors[0].ErrorMessage.Should().Contain("characters");

            productFamilyEntity.Name = TestHelpers.RandomString(7);

            productFamilyEntity.Validate().IsValid.Should().BeTrue();
        }

    }
}
