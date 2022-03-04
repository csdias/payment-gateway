using System.Linq;
using System.Collections.Generic;
using Xunit;
using FluentAssertions;
using EnterpriseBusinessRules.UnitTests.Helpers;
using EnterpriseBusinessRules.Entities;

namespace EnterpriseBusinessRules.UnitTests.Validators
{
    public class ProductGroupValidatorTests
    {
        [Fact]
        public ProductGroupEntity CreateProductGroupEntity()
        {
            var others = new Dictionary<string, object>();
            others.Add(TestHelpers.RandomString(10), TestHelpers.RandomString(10));

            return new ProductGroupEntity()
            {
                Id = TestHelpers.RandomString(8),
                Name = TestHelpers.RandomString(5),
                Description = TestHelpers.RandomString(255),
                Formula = TestHelpers.RandomString(10),
                Others = others
            };
        }

        [Fact]
        public void IdFieldShouldBeRequired()
        {
            var productGroupEntity = CreateProductGroupEntity();

            productGroupEntity.Validate().IsValid.Should().BeTrue();

            productGroupEntity.Id = null;

            productGroupEntity.Validate().Errors[0].ErrorMessage.Should().Be("Id is required");

            productGroupEntity.Id = TestHelpers.RandomString(8);

            productGroupEntity.Validate().IsValid.Should().BeTrue();

        }

        [Fact]
        public void NameFieldShouldBeRequired()
        {
            var productGroupEntity = CreateProductGroupEntity();

            productGroupEntity.Validate().IsValid.Should().BeTrue();

            productGroupEntity.Name = null;

            productGroupEntity.Validate().Errors[0].ErrorMessage.Should().Be("Name is required");

            productGroupEntity.Name = TestHelpers.RandomString(5);

            productGroupEntity.Validate().IsValid.Should().BeTrue();

        }

        [Theory]
        [InlineData(4)]
        [InlineData(256)]
        public void NameFieldCharacterQuantityHaveError(int quantity)
        {
            var productGroupEntity = CreateProductGroupEntity();

            productGroupEntity.Validate().IsValid.Should().BeTrue();

            productGroupEntity.Name = TestHelpers.RandomString(quantity);

            productGroupEntity.Validate().Errors[0].ErrorMessage.Should().Contain("characters");
            productGroupEntity.Validate().Errors.Count().Should().Be(1);

            productGroupEntity.Name = TestHelpers.RandomString(5);

            productGroupEntity.Validate().IsValid.Should().BeTrue();

        }

    }
}
