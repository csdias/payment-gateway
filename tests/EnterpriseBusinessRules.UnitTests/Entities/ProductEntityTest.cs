using System.Collections.Generic;
using Xunit;
using FluentAssertions;
using FluentValidation.Results;
using EnterpriseBusinessRules.UnitTests.Helpers;
using EnterpriseBusinessRules.Entities;

namespace EnterpriseBusinessRules.UnitTests.Entities
{
    public class ProductEntityTests
    {
        private readonly string _productId;
        private readonly string _name;
        private readonly string _description;
        private readonly Dictionary<string, bool> _flags;
        private readonly Dictionary<string, object> _others;

        public ProductEntityTests()
        {
            _productId = TestHelpers.RandomString(8);
            _name = TestHelpers.RandomString(5);
            _description = TestHelpers.RandomString(240);
            _flags = new Dictionary<string, bool>();
            _flags.Add(TestHelpers.RandomString(10), true);
            _flags.Add(TestHelpers.RandomString(10), false);
            _others = new Dictionary<string, object>();
            _others.Add(TestHelpers.RandomString(10), TestHelpers.RandomString(10));
        }

        public ProductEntity CreateProductEntity()
        {
            var rates = new List<ProductRateEntity>();
            rates.Add(
                new ProductRateEntity()
                {
                    Fields = new Dictionary<string, object>()
                }
            );
            return new ProductEntity()
            {
                Id = _productId,
                Name = _name,
                Description = _description,
                Type = new ProductTypeEntity()
                {
                    Id = TestHelpers.RandomString(8),
                    Name = _name
                },
                Group = new ProductGroupEntity()
                {
                    Id = TestHelpers.RandomString(8),
                    Name = _name
                },
                Family = new ProductFamilyEntity()
                {
                    Id = TestHelpers.RandomString(8),
                    Name = _name
                },
                Capital = new ProductCapitalEntity(),
                Rates = rates,
                Flags = _flags,
                Others = _others,
                Items = new List<ProductEntity>()
            };
        }

        [Fact]
        public void ValidateEntityCreationAndFieldsValues() 
        {
            var productEntity = CreateProductEntity();
            
            productEntity.Id.Should().Equals(_productId);
            productEntity.Name.Should().Equals(_name);
            productEntity.Description.Should().Equals(_description);
            productEntity.Type.Should().BeOfType<ProductTypeEntity>();
            productEntity.Group.Should().BeOfType<ProductGroupEntity>();
            productEntity.Family.Should().BeOfType<ProductFamilyEntity>();
            productEntity.Capital.Should().BeOfType<ProductCapitalEntity>();
            productEntity.Rates.Should().BeOfType<List<ProductRateEntity>>();
            productEntity.Flags.Should().Equals(_flags);
            productEntity.Others.Should().Equals(_others);
            productEntity.Items.Should().BeOfType<List<ProductEntity>>();
            productEntity.Should().NotBeNull();
            productEntity.Should().BeOfType<ProductEntity>();
        }
        
        [Fact]
        public void ValidateMethodShouldBeCalled()
        {
            var productEntity = CreateProductEntity();
            
            productEntity.Validate().IsValid.Should().BeTrue();
            productEntity.Validate().Should().BeOfType<ValidationResult>();
        }
    }
}
