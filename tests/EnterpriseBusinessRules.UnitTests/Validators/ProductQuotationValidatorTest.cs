using System.Collections.Generic;
using Xunit;
using FluentAssertions;
using EnterpriseBusinessRules.UnitTests.Helpers;
using EnterpriseBusinessRules.Entities;

namespace EnterpriseBusinessRules.UnitTests.Validators
{
    public class ProductQuotationValidatorTests
    {
        [Fact]
        public ProductQuotationEntity CreateProductQuotationEntity()
        {
            var others = new Dictionary<string, object>();
            others.Add(TestHelpers.RandomString(10), TestHelpers.RandomString(10));

            return new ProductQuotationEntity()
            {
                Id = TestHelpers.RandomString(8),
                Name = TestHelpers.RandomString(8),
                Description = TestHelpers.RandomString(255),
                Type = new ProductTypeEntity()
                {
                    Id = TestHelpers.RandomString(8),
                    Name = TestHelpers.RandomString(8),
                    Description = TestHelpers.RandomString(255),
                    Others = others
                },
                Family = new ProductFamilyEntity()
                {
                    Id = TestHelpers.RandomString(8),
                    Name = TestHelpers.RandomString(10)
                },
                Capital = 10000F,
                Rate = new ProductQuotationRateEntity(),
                Taxes = new QuotationTaxesEntity(),
                Price = new QuotationPriceEntity(),
                Others = others,
                Items = new List<ProductQuotationEntity>(),
            };
        }

        [Fact]
        public void IdFieldShouldBeRequired()
        {
            var quotationProductEntity = CreateProductQuotationEntity();

            quotationProductEntity.Validate().IsValid.Should().BeTrue();

            quotationProductEntity.Id = null;

            quotationProductEntity.Validate().Errors[0].ErrorMessage.Should().Be("Id is required");

            quotationProductEntity.Id = TestHelpers.RandomString(8);

            quotationProductEntity.Validate().IsValid.Should().BeTrue();
        }

        [Theory]
        [InlineData(2)]
        [InlineData(9)]
        public void IdFieldCharacterQuantityHaveError(int quantity)
        {
            var quotationProductEntity = CreateProductQuotationEntity();

            quotationProductEntity.Validate().IsValid.Should().BeTrue();

            quotationProductEntity.Id = TestHelpers.RandomString(quantity);

            quotationProductEntity.Validate().Errors[0].ErrorMessage.Should().Contain("characters");
            quotationProductEntity.Validate().Errors.Count.Should().Be(1);

            quotationProductEntity.Id = TestHelpers.RandomString(7);

            quotationProductEntity.Validate().IsValid.Should().BeTrue();
        }

        [Fact]
        public void NameFieldShouldBeRequired()
        {
            var quotationProductEntity = CreateProductQuotationEntity();

            quotationProductEntity.Validate().IsValid.Should().BeTrue();

            quotationProductEntity.Name = null;

            quotationProductEntity.Validate().Errors[0].ErrorMessage.Should().Be("Name is required");

            quotationProductEntity.Name = TestHelpers.RandomString(10);

            quotationProductEntity.Validate().IsValid.Should().BeTrue();
        }

        [Theory]
        [InlineData(4)]
        [InlineData(256)]
        public void NameFieldCharacterQuantityHaveError(int quantity)
        {
            var quotationProductEntity = CreateProductQuotationEntity();

            quotationProductEntity.Validate().IsValid.Should().BeTrue();

            quotationProductEntity.Name = TestHelpers.RandomString(quantity);

            quotationProductEntity.Validate().Errors[0].ErrorMessage.Should().Contain("characters");
            quotationProductEntity.Validate().Errors.Count.Should().Be(1);

            quotationProductEntity.Name = TestHelpers.RandomString(7);

            quotationProductEntity.Validate().IsValid.Should().BeTrue();
        }

        [Fact]
        public void DescriptionFieldShouldBeRequired()
        {
            var quotationProductEntity = CreateProductQuotationEntity();

            quotationProductEntity.Validate().IsValid.Should().BeTrue();

            quotationProductEntity.Description = null;

            quotationProductEntity.Validate().Errors[0].ErrorMessage.Should().Be("Description is required");

            quotationProductEntity.Description = TestHelpers.RandomString(10);

            quotationProductEntity.Validate().IsValid.Should().BeTrue();
        }

        [Theory]
        [InlineData(4)]
        [InlineData(256)]
        public void DescriptionFieldCharacterQuantityHaveError(int quantity)
        {
            var quotationProductEntity = CreateProductQuotationEntity();

            quotationProductEntity.Validate().IsValid.Should().BeTrue();

            quotationProductEntity.Description = TestHelpers.RandomString(quantity);

            quotationProductEntity.Validate().Errors[0].ErrorMessage.Should().Contain("characters");
            quotationProductEntity.Validate().Errors.Count.Should().Be(1);

            quotationProductEntity.Description = TestHelpers.RandomString(7);

            quotationProductEntity.Validate().IsValid.Should().BeTrue();
        }

        [Fact]
        public void TypeFieldShouldBeRequired()
        {
            var quotationProductEntity = CreateProductQuotationEntity();

            quotationProductEntity.Validate().IsValid.Should().BeTrue();

            quotationProductEntity.Type = null;

            quotationProductEntity.Validate().Errors[0].ErrorMessage.Should().Be("Type is required");

            quotationProductEntity = CreateProductQuotationEntity();

            quotationProductEntity.Validate().IsValid.Should().BeTrue();
        }

        [Fact]
        public void FamilyFieldShouldBeRequired()
        {
            var quotationProductEntity = CreateProductQuotationEntity();

            quotationProductEntity.Validate().IsValid.Should().BeTrue();

            quotationProductEntity.Family = null;

            quotationProductEntity.Validate().Errors[0].ErrorMessage.Should().Be("Family is required");

            quotationProductEntity = CreateProductQuotationEntity();

            quotationProductEntity.Validate().IsValid.Should().BeTrue();
        }

        [Fact]
        public void RateFieldShouldBeRequired()
        {
            var quotationProductEntity = CreateProductQuotationEntity();

            quotationProductEntity.Validate().IsValid.Should().BeTrue();

            quotationProductEntity.Rate = null;

            quotationProductEntity.Validate().Errors[0].ErrorMessage.Should().Be("Rate is required");

            quotationProductEntity = CreateProductQuotationEntity();

            quotationProductEntity.Validate().IsValid.Should().BeTrue();
        }

        [Fact]
        public void TaxesFieldShouldBeRequired()
        {
            var quotationProductEntity = CreateProductQuotationEntity();

            quotationProductEntity.Validate().IsValid.Should().BeTrue();

            quotationProductEntity.Taxes = null;

            quotationProductEntity.Validate().Errors[0].ErrorMessage.Should().Be("Taxes is required");

            quotationProductEntity = CreateProductQuotationEntity();

            quotationProductEntity.Validate().IsValid.Should().BeTrue();
        }

        [Fact]
        public void PriceFieldShouldBeRequired()
        {
            var quotationProductEntity = CreateProductQuotationEntity();

            quotationProductEntity.Validate().IsValid.Should().BeTrue();

            quotationProductEntity.Price = null;

            quotationProductEntity.Validate().Errors[0].ErrorMessage.Should().Be("Price is required");

            quotationProductEntity = CreateProductQuotationEntity();

            quotationProductEntity.Validate().IsValid.Should().BeTrue();
        }

    }
}
