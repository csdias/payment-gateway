using System.Collections.Generic;
using Xunit;
using FluentAssertions;
using FluentValidation.Results;
using EnterpriseBusinessRules.UnitTests.Helpers;
using EnterpriseBusinessRules.Entities;

namespace EnterpriseBusinessRules.UnitTests.Entities
{
    public class InsuranceQuoteInputValidatorTests
    {
        private readonly string _insuranceQuoteInputCode;
        private readonly string _name;
        private readonly string _description;
        private readonly List<InsuranceQuoteInputItemEntity> _items;

        public InsuranceQuoteInputValidatorTests()
        {
            _insuranceQuoteInputCode = TestHelpers.RandomString(8);
            _name = TestHelpers.RandomString(5);
            _description = TestHelpers.RandomString(240);
            _items = new List<InsuranceQuoteInputItemEntity>();
            _items.Add(
                new InsuranceQuoteInputItemEntity()
                {
                    Amount = new InsuranceQuoteInputAmountEntity()
                    {
                        Type = "monthly",
                        Amount = 1000F
                    },
                    Product = TestHelpers.RandomString(7)
                }
            );
        }

        public InsuranceQuoteInputEntity CreateInsuranceQuoteInputEntity()
        {
            var parameters = new Dictionary<string, object>();
            parameters.Add("age", 30);
            parameters.Add("gender", "M");

            var information = new Dictionary<string, object>();
            information.Add("param1", "test");

            return new InsuranceQuoteInputEntity()
            {
                Code = _insuranceQuoteInputCode,
                Name = _name,
                Description = _description,
                Client = TestHelpers.RandomString(10),
                Product = TestHelpers.RandomString(7),
                Information = information,
                Rate = 10F,
                Amount = new InsuranceQuoteInputAmountEntity()
                {
                    Type = "monthly",
                    Amount = 1000F
                },
                Parameters = parameters,
                Items = _items
                
            };
        }

        [Fact]
        public void ProductFieldShouldBeRequired()
        {
            var productTypeEntity = CreateInsuranceQuoteInputEntity();

            productTypeEntity.Validate().IsValid.Should().BeTrue();

            productTypeEntity.Product = null;

            productTypeEntity.Validate().Errors[0].ErrorMessage.Should().Be("Product is required");

            productTypeEntity.Product = TestHelpers.RandomString(5);

            productTypeEntity.Validate().IsValid.Should().BeTrue();
        }

        [Fact]
        public void DescriptionHave255MaxCharacters()
        {
            var productTypeEntity = CreateInsuranceQuoteInputEntity();

            productTypeEntity.Validate().IsValid.Should().BeTrue();

            productTypeEntity.Description = TestHelpers.RandomString(256);

            productTypeEntity.Validate().Errors[0].ErrorMessage.Should().Be("Description should be a maximun 255 characters");

            productTypeEntity.Description = TestHelpers.RandomString(255);

            productTypeEntity.Validate().IsValid.Should().BeTrue();
        }

        [Fact]
        public void InformationFieldShouldBeRequired()
        {
            var productTypeEntity = CreateInsuranceQuoteInputEntity();

            productTypeEntity.Validate().IsValid.Should().BeTrue();

            productTypeEntity.Information = null;

            productTypeEntity.Validate().Errors[0].ErrorMessage.Should().Be("Information is required");

            productTypeEntity = CreateInsuranceQuoteInputEntity();

            productTypeEntity.Validate().IsValid.Should().BeTrue();
        }
    }
}
