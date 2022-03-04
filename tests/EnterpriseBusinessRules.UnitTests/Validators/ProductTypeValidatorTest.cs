using System.Linq;
using System.Collections.Generic;
using Xunit;
using FluentAssertions;
using EnterpriseBusinessRules.UnitTests.Helpers;
using EnterpriseBusinessRules.Entities;

namespace EnterpriseBusinessRules.UnitTests.Validators
{
    public class ProductTypeValidatorTests
    {
        [Fact]
        public ProductTypeEntity CreateProductTypeEntity()
        {
            var others = new Dictionary<string, object>();
            others.Add(TestHelpers.RandomString(10), TestHelpers.RandomString(10));

            return new ProductTypeEntity()
            {
                Id = TestHelpers.RandomString(8),
                Name = TestHelpers.RandomString(5),
                Description = TestHelpers.RandomString(255),
                Others = others
            };
        }

        [Fact]
        public void IdFieldShouldBeRequired()
        {
            var productTypeEntity = CreateProductTypeEntity();

            productTypeEntity.Validate().IsValid.Should().BeTrue();

            productTypeEntity.Id = null;

            productTypeEntity.Validate().Errors[0].ErrorMessage.Should().Be("Id is required");

            productTypeEntity.Id = TestHelpers.RandomString(8);

            productTypeEntity.Validate().IsValid.Should().BeTrue();

        }

        [Fact]
        public void NameFieldShouldBeRequired()
        {
            var productTypeEntity = CreateProductTypeEntity();

            productTypeEntity.Validate().IsValid.Should().BeTrue();

            productTypeEntity.Name = null;

            productTypeEntity.Validate().Errors[0].ErrorMessage.Should().Be("Name is required");

            productTypeEntity.Name = TestHelpers.RandomString(5);

            productTypeEntity.Validate().IsValid.Should().BeTrue();

        }

        [Theory]
        [InlineData(4)]
        [InlineData(256)]
        public void NameFieldCharacterQuantityHaveError(int quantity)
        {
            var productTypeEntity = CreateProductTypeEntity();

            productTypeEntity.Validate().IsValid.Should().BeTrue();

            productTypeEntity.Name = TestHelpers.RandomString(quantity);

            productTypeEntity.Validate().Errors[0].ErrorMessage.Should().Contain("characters");
            productTypeEntity.Validate().Errors.Count().Should().Be(1);

            productTypeEntity.Name = TestHelpers.RandomString(5);

            productTypeEntity.Validate().IsValid.Should().BeTrue();

        }

    }
}
