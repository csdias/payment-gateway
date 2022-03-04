using System.Collections.Generic;
using Xunit;
using FluentAssertions;
using FluentValidation.Results;
using EnterpriseBusinessRules.UnitTests.Helpers;
using EnterpriseBusinessRules.Entities;

namespace EnterpriseBusinessRules.UnitTests.Entities
{
    public class InsuranceQuoteInputEntityTests
    {
        private readonly string _insuranceQuoteInputCode;
        private readonly string _name;
        private readonly string _description;
        private readonly List<InsuranceQuoteInputItemEntity> _items;

        public InsuranceQuoteInputEntityTests()
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
        public void ValidateEntityCreationAndFieldsValues() 
        {
            var insuranceQuoteInputEntity = CreateInsuranceQuoteInputEntity();
            
            insuranceQuoteInputEntity.Code.Should().Equals(_insuranceQuoteInputCode);
            insuranceQuoteInputEntity.Name.Should().Equals(_name);
            insuranceQuoteInputEntity.Client.Should().BeOfType<string>();
            insuranceQuoteInputEntity.Description.Should().Equals(_description);
            insuranceQuoteInputEntity.Information.Should().BeOfType<Dictionary<string, object>>();
            insuranceQuoteInputEntity.Product.Should().BeOfType<string>();
            insuranceQuoteInputEntity.Rate.Should().Be(10F);
            insuranceQuoteInputEntity.Amount.Should().BeOfType<InsuranceQuoteInputAmountEntity>();
            insuranceQuoteInputEntity.Parameters.Should().BeOfType<Dictionary<string, object>>();
            insuranceQuoteInputEntity.Items.Should().BeOfType<List<InsuranceQuoteInputItemEntity>>();
            insuranceQuoteInputEntity.Should().NotBeNull();
            insuranceQuoteInputEntity.Should().BeOfType<InsuranceQuoteInputEntity>();
        }
        
        [Fact]
        public void ValidateMethodShouldBeCalled()
        {
            var insuranceQuoteInputEntity = CreateInsuranceQuoteInputEntity();

            insuranceQuoteInputEntity.Validate().IsValid.Should().BeTrue();
            insuranceQuoteInputEntity.Validate().Should().BeOfType<ValidationResult>();
        }
    }
}
