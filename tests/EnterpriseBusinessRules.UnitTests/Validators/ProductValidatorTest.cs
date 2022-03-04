using System.Collections.Generic;
using Xunit;
using FluentAssertions;
using EnterpriseBusinessRules.UnitTests.Helpers;
using EnterpriseBusinessRules.Entities;

namespace EnterpriseBusinessRules.UnitTests.Validators
{
    public class ProductValidatorTests
    {
        public ProductEntity CreateProductEntity()
        {
            var others = new Dictionary<string, object>();
            others.Add(TestHelpers.RandomString(10), TestHelpers.RandomString(10));

            var rates = new List<ProductRateEntity>();
            rates.Add(
                new ProductRateEntity()
                {
                    Fields = new Dictionary<string, object>()
                }
            );

            return new ProductEntity()
            {
                Id = TestHelpers.RandomString(5),
                Name = TestHelpers.RandomString(5),
                Description = TestHelpers.RandomString(255),
                Type = new ProductTypeEntity()
                {
                    Id = TestHelpers.RandomString(8),
                    Name = TestHelpers.RandomString(8)
                },
                Group = new ProductGroupEntity()
                {
                    Id = TestHelpers.RandomString(8),
                    Name = TestHelpers.RandomString(8)
                },
                Family = new ProductFamilyEntity()
                {
                    Id = TestHelpers.RandomString(8),
                    Name = TestHelpers.RandomString(8)
                },
                Capital = new ProductCapitalEntity(),
                Rates = rates,
                Others = others,
                Items = new List<ProductEntity>(),
            };
        }

        [Fact]
        public void IdFieldShouldBeRequired()
        {
            var productEntity = CreateProductEntity();

            productEntity.Validate().IsValid.Should().BeTrue();

            productEntity.Id = null;

            productEntity.Validate().Errors[0].ErrorMessage.Should().Be("Id is required");

            productEntity.Id = TestHelpers.RandomString(8);

            productEntity.Validate().IsValid.Should().BeTrue();
        }

        [Theory]
        [InlineData(1)]
        [InlineData(9)]
        public void IdFieldCharacterQuantityHaveError(int quantity)
        {
            var productEntity = CreateProductEntity();

            productEntity.Validate().IsValid.Should().BeTrue();

            productEntity.Id = TestHelpers.RandomString(quantity);;
            
            productEntity.Validate().Errors[0].ErrorMessage.Should().Contain("characters");

            productEntity.Id = TestHelpers.RandomString(5);

            productEntity.Validate().IsValid.Should().BeTrue();
        }

        [Fact]
        public void NameFieldShouldBeRequired()
        {
            var productEntity = CreateProductEntity();

            productEntity.Validate().IsValid.Should().BeTrue();

            productEntity.Name = null;

            productEntity.Validate().Errors[0].ErrorMessage.Should().Be("Name is required");

            productEntity.Name = TestHelpers.RandomString(8);

            productEntity.Validate().IsValid.Should().BeTrue();
        }

        [Theory]
        [InlineData(4)]
        [InlineData(256)]
        public void NameFieldCharacterQuantityHaveError(int quantity)
        {
            var productEntity = CreateProductEntity();

            productEntity.Validate().IsValid.Should().BeTrue();

            productEntity.Name = TestHelpers.RandomString(quantity);;
            
            productEntity.Validate().Errors[0].ErrorMessage.Should().Contain("characters");

            productEntity.Name = TestHelpers.RandomString(5);

            productEntity.Validate().IsValid.Should().BeTrue();
        }

        [Fact]
        public void TypeFieldShouldBeRequired()
        {
            var productEntity = CreateProductEntity();

            productEntity.Validate().IsValid.Should().BeTrue();

            productEntity.Type = null;

            productEntity.Validate().Errors[0].ErrorMessage.Should().Be("Type is required");

            productEntity.Type = new ProductTypeEntity()
            {
                Id = TestHelpers.RandomString(8),
                Name = TestHelpers.RandomString(8)
            };

            productEntity.Validate().IsValid.Should().BeTrue();
        }

        [Fact]
        public void GroupFieldShouldBeRequired()
        {
            var productEntity = CreateProductEntity();

            productEntity.Validate().IsValid.Should().BeTrue();

            productEntity.Group = null;

            productEntity.Validate().Errors[0].ErrorMessage.Should().Be("Group is required");

            productEntity.Group = new ProductGroupEntity()
            {
                Id = TestHelpers.RandomString(8),
                Name = TestHelpers.RandomString(8)
            };

            productEntity.Validate().IsValid.Should().BeTrue();
        }

        [Fact]
        public void RatesFieldShouldBeRequired()
        {
            var productEntity = CreateProductEntity();

            productEntity.Validate().IsValid.Should().BeTrue();

            productEntity.Rates = null;

            productEntity.Validate().Errors[0].ErrorMessage.Should().Be("Rates is required");

            var rates = new List<ProductRateEntity>();
            rates.Add(
                new ProductRateEntity()
                {
                    Fields = new Dictionary<string, object>()
                }
            );

            productEntity.Rates = rates;
            
            productEntity.Validate().IsValid.Should().BeTrue();
        }

    }
}
