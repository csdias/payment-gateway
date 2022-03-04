using System.Linq;
using System.Collections.Generic;
using Xunit;
using FluentAssertions;
using FluentValidation.Results;
using EnterpriseBusinessRules.Entities;
using EnterpriseBusinessRules.Validators;
using EnterpriseBusinessRules.UnitTests.Helpers;

namespace EnterpriseBusinessRules.UnitTests.Entities
{
    public class InsuranceQuoteValidatorTests
    {
        private readonly ProductEntity _product;
        private readonly InsuranceQuoteInputEntity _input;
        
        public InsuranceQuoteValidatorTests()
        {
            var parameters = new Dictionary<string, object>();
            parameters.Add("age", 30);
            parameters.Add("gender", "M");

            var inputItems = new List<InsuranceQuoteInputItemEntity>();
            inputItems.Add(
                new InsuranceQuoteInputItemEntity()
                {
                    Amount = new InsuranceQuoteInputAmountEntity()
                    {
                        Type = "monthly",
                        Amount = 1000F
                    },
                    Product = "AB777F"
                }
            );

            var productItems = new List<ProductEntity>();
            productItems.Add(
                new ProductEntity()
                {
                    Id = "AB777F"
                }
            );

            var rates = new List<ProductRateEntity>();
            rates.Add(
                new ProductRateEntity()
                {
                    Fields = new Dictionary<string, object>()
                }
            );

            var others = new Dictionary<string, object>();
            others.Add(TestHelpers.RandomString(10), TestHelpers.RandomString(10));

            var information = new Dictionary<string, object>();
            information.Add("param1", "test");

            _product = new ProductEntity()
            {
                Id = "AB777F",
                Name = TestHelpers.RandomString(8),
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
                Items = productItems,
            };

            _input = new InsuranceQuoteInputEntity()
            {
                Code = TestHelpers.RandomString(8),
                Name = TestHelpers.RandomString(8),
                Description = TestHelpers.RandomString(10),
                Client = TestHelpers.RandomString(10),
                Product = TestHelpers.RandomString(8),
                Information = information,
                Rate = 10F,
                Amount = new InsuranceQuoteInputAmountEntity()
                {
                    Type = "monthly",
                    Amount = 1000F
                },
                Parameters = parameters,
                Items = inputItems
            };
        }

        public InsuranceQuoteEntity CreateInsuranceQuoteEntity()
        {
            var settings = new Dictionary<string, object>();
            settings.Add("param1", "test");

            return new InsuranceQuoteEntity()
            {
                Input = _input,
                Product = _product,
                Settings = new QuotationSettingsEntity()
                {
                    Settings = settings
                }
            };
        }

        [Fact]
        public void ProductItemsFieldSholdBeRequired()
        {
            var insuranceQuote = CreateInsuranceQuoteEntity();

            insuranceQuote.Validate().IsValid.Should().BeTrue();

            insuranceQuote.Product.Items = null;

            insuranceQuote.Validate().IsValid.Should().BeTrue();   
        }

        [Fact]
        public void ParametersAgeSholdBeNumeric()
        {
            var insuranceQuote = CreateInsuranceQuoteEntity();

            insuranceQuote.Validate().IsValid.Should().BeTrue();

            insuranceQuote.Input.Parameters = new Dictionary<string, object>();
            insuranceQuote.Input.Parameters.Add("age", "X");
            insuranceQuote.Input.Parameters.Add("gender", "M");

            insuranceQuote.Validate().Errors[0].ErrorMessage.Should().Contain("{PropertyName}.age must be a numeric value");

            insuranceQuote.Input.Parameters = new Dictionary<string, object>();
            insuranceQuote.Input.Parameters.Add("age", 33);
            insuranceQuote.Input.Parameters.Add("gender", "M");

            insuranceQuote.Validate().IsValid.Should().BeTrue();   
        }

        [Fact]
        public void ParametersGenderSholdBeValid()
        {
            var insuranceQuote = CreateInsuranceQuoteEntity();

            insuranceQuote.Validate().IsValid.Should().BeTrue();

            insuranceQuote.Input.Parameters = new Dictionary<string, object>();
            insuranceQuote.Input.Parameters.Add("age", 33);
            insuranceQuote.Input.Parameters.Add("gender", "X");

            insuranceQuote.Validate().Errors[0].ErrorMessage.Should().Contain("{PropertyName}.gender should only have the values \"M\" and \"F\"");

            insuranceQuote.Input.Parameters = new Dictionary<string, object>();
            insuranceQuote.Input.Parameters.Add("age", 33);
            insuranceQuote.Input.Parameters.Add("gender", "M");

            insuranceQuote.Validate().IsValid.Should().BeTrue();   
        }
    }
}
