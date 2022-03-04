using System.Collections.Generic;
using Xunit;
using FluentAssertions;
using FluentValidation.Results;
using EnterpriseBusinessRules.UnitTests.Helpers;
using EnterpriseBusinessRules.Entities;

namespace EnterpriseBusinessRules.UnitTests.Entities
{
    public class ProductQuotationEntityTests
    {
        private readonly string _quotationProductId;
        private readonly string _name;
        private readonly string _description;
        private readonly Dictionary<string, object> _others;

        public ProductQuotationEntityTests()
        {
            _quotationProductId = TestHelpers.RandomString(8);
            _name = TestHelpers.RandomString(5);
            _description = TestHelpers.RandomString(240);
            _others = new Dictionary<string, object>();
            _others.Add(TestHelpers.RandomString(10), TestHelpers.RandomString(10));
        }

        public ProductQuotationEntity CreateProductQuotationEntity()
        {
            return new ProductQuotationEntity()
            {
                Id = _quotationProductId,
                Name = _name,
                Description = _description,
                Type = new ProductTypeEntity(),
                Family = new ProductFamilyEntity(),
                Capital = 10000F,
                Rate = new ProductQuotationRateEntity(),
                Taxes = new QuotationTaxesEntity(),
                Price = new QuotationPriceEntity(),
                Others = _others,
                Items = new List<ProductQuotationEntity>(),
            };
        }

        [Fact]
        public void ValidateEntityCreationAndFieldsValues() 
        {
            var quotationProductEntity = CreateProductQuotationEntity();
            
            quotationProductEntity.Id.Should().Equals(_quotationProductId);
            quotationProductEntity.Name.Should().Equals(_name);
            quotationProductEntity.Description.Should().Equals(_description);
            quotationProductEntity.Type.Should().BeOfType<ProductTypeEntity>();
            quotationProductEntity.Family.Should().BeOfType<ProductFamilyEntity>();
            quotationProductEntity.Capital.Should().Be(10000F);
            quotationProductEntity.Rate.Should().BeOfType<ProductQuotationRateEntity>();
            quotationProductEntity.Taxes.Should().BeOfType<QuotationTaxesEntity>();
            quotationProductEntity.Price.Should().BeOfType<QuotationPriceEntity>();
            quotationProductEntity.Others.Should().Equals(_others);
            quotationProductEntity.Items.Should().BeOfType<List<ProductQuotationEntity>>();
            quotationProductEntity.Should().NotBeNull();
            quotationProductEntity.Should().BeOfType<ProductQuotationEntity>();
        }
        
        [Fact]
        public void ValidateMethodShouldBeCalled()
        {
            var quotationProductEntity = CreateProductQuotationEntity();

            //quotationProductEntity.Validate().IsValid.Should().BeTrue();
            //quotationProductEntity.Validate().Should().BeOfType<ValidationResult>();
        }
    }
}
