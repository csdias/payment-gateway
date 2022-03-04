using System.Collections.Generic;
using Xunit;
using FluentAssertions;
using EnterpriseBusinessRules.UnitTests.Helpers;
using EnterpriseBusinessRules.Entities;

namespace EnterpriseBusinessRules.UnitTests.Entities
{
    public class QuotationValidatorTests
    {
        public QuotationEntity CreateQuotationEntity()
        {
            var information = new Dictionary<string, object>();
            information.Add("param1", "test");

            var settings = new Dictionary<string, object>();
            settings.Add("param1", "test");

            return new QuotationEntity()
            {
                Code = TestHelpers.RandomString(8),
                ClientId = TestHelpers.RandomString(10),
                Description = TestHelpers.RandomString(10),
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
                    Name = TestHelpers.RandomString(10),
                    Description = TestHelpers.RandomString(10),
                    Type = new ProductTypeEntity()
                    {
                        Id = TestHelpers.RandomString(8),
                        Name = TestHelpers.RandomString(10),
                        Description = TestHelpers.RandomString(10),
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
        public void ProductFieldShouldBeRequired()
        {
            var quotationEntity = CreateQuotationEntity();
            
            quotationEntity.Validate().IsValid.Should().BeTrue();

            quotationEntity.Product = null;

            quotationEntity.Validate().Errors[0].ErrorMessage.Should().Contain("Product is required");

            quotationEntity.Product = new ProductQuotationEntity()
            {
                Id = TestHelpers.RandomString(8),
                Name = TestHelpers.RandomString(8),
                Description = TestHelpers.RandomString(255),
                Type = new ProductTypeEntity()
                {
                    Id = TestHelpers.RandomString(8),
                    Name = TestHelpers.RandomString(8),
                    Description = TestHelpers.RandomString(255),
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
                Others = new Dictionary<string, object>(),
            };

            quotationEntity.Validate().IsValid.Should().BeTrue();
        }

        [Fact]
        public void InformationFieldShouldBeRequired()
        {
            var quotationEntity = CreateQuotationEntity();
            
            quotationEntity.Validate().IsValid.Should().BeTrue();

            quotationEntity.Information = null;

            quotationEntity.Validate().Errors[0].ErrorMessage.Should().Contain("Information is required");

            var information = new Dictionary<string, object>();
            information.Add("param1", "test");
            quotationEntity.Information = information;

            quotationEntity.Validate().IsValid.Should().BeTrue();
        }

        [Fact]
        public void SettingsFieldShouldBeRequired()
        {
            var quotationEntity = CreateQuotationEntity();
            
            quotationEntity.Validate().IsValid.Should().BeTrue();

            quotationEntity.Settings = null;

            quotationEntity.Validate().Errors[0].ErrorMessage.Should().Contain("Settings is required");

            var settings = new Dictionary<string, object>();
            settings.Add("param1", "test");
            quotationEntity.Settings = new QuotationSettingsEntity()
            {
                Settings = settings
            };

            quotationEntity.Validate().IsValid.Should().BeTrue();
        }

        [Fact]
        public void PriceFieldShouldBeRequired()
        {
            var quotationEntity = CreateQuotationEntity();
            
            quotationEntity.Validate().IsValid.Should().BeTrue();

            quotationEntity.Price = null;

            quotationEntity.Validate().Errors[0].ErrorMessage.Should().Contain("Price is required");

            quotationEntity.Price = new QuotationPriceEntity();

            quotationEntity.Validate().IsValid.Should().BeTrue();
        }

    }
}
