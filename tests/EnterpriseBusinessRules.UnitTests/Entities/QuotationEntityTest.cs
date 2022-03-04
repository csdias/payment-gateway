using System.Collections.Generic;
using Xunit;
using FluentAssertions;
using FluentValidation.Results;
using EnterpriseBusinessRules.UnitTests.Helpers;
using EnterpriseBusinessRules.Entities;

namespace EnterpriseBusinessRules.UnitTests.Entities
{
    public class QuotationEntityTests
    {
        private readonly string _quotationCode;
        private readonly string _name;
        private readonly string _description;

        public QuotationEntityTests()
        {
            _quotationCode = TestHelpers.RandomString(8);
            _name = TestHelpers.RandomString(5);
            _description = TestHelpers.RandomString(240);
        }

        public QuotationEntity CreateQuotationEntity()
        {
            var information = new Dictionary<string, object>();
            information.Add("param1", "test");

            var settings = new Dictionary<string, object>();
            settings.Add("param1", "test");

            return new QuotationEntity()
            {
                Code = _quotationCode,
                ClientId = TestHelpers.RandomString(10),
                Description = _description,
                Information = information,
                Settings = new QuotationSettingsEntity()
                {
                    Settings = settings
                },
                Rate = 10F,
                //Capital = 25000F,
                Parameters = new Dictionary<string, object>(),
                Price = new QuotationPriceEntity(),
                Product = new ProductQuotationEntity()
                {
                    Id = TestHelpers.RandomString(8),
                    Name = _name,
                    Description = _description,
                    Type = new ProductTypeEntity()
                    {
                        Id = TestHelpers.RandomString(8),
                        Name = _name,
                        Description = _description,
                        Others = new Dictionary<string, object>()
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
                    Others = new Dictionary<string, object>()
                }
            };
        }

        [Fact]
        public void ValidateEntityCreationAndFieldsValues() 
        {
            var quotationEntity = CreateQuotationEntity();
            
            quotationEntity.Id.Should().Equals(_quotationCode);
            quotationEntity.ClientId.Should().BeOfType<string>();
            quotationEntity.Description.Should().Equals(_description);
            quotationEntity.Information.Should().BeOfType<Dictionary<string, object>>();
            quotationEntity.Settings.Should().BeOfType<QuotationSettingsEntity>();
            quotationEntity.Product.Should().BeOfType<ProductQuotationEntity>();
            quotationEntity.Rate.Should().Be(10F);
            quotationEntity.Capital.Should().Be(25000F);
            quotationEntity.Parameters.Should().BeOfType<Dictionary<string, object>>();
            quotationEntity.Price.Should().BeOfType<QuotationPriceEntity>();
            quotationEntity.Should().NotBeNull();
            quotationEntity.Should().BeOfType<QuotationEntity>();
        }
        
        [Fact]
        public void ValidateMethodShouldBeCalled()
        {
            var quotationEntity = CreateQuotationEntity();

            quotationEntity.Validate().IsValid.Should().BeTrue();
            quotationEntity.Validate().Should().BeOfType<ValidationResult>();
        }
    }
}
