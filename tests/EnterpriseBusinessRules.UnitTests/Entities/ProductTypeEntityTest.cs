using System.Collections.Generic;
using Xunit;
using FluentAssertions;
using FluentValidation.Results;
using EnterpriseBusinessRules.UnitTests.Helpers;
using EnterpriseBusinessRules.Entities;

namespace EnterpriseBusinessRules.UnitTests.Entities
{
    public class ProductTypeEntityTests
    {
        private readonly string _productTypeId;
        private readonly string _name;
        private readonly string _description;
        private readonly Dictionary<string, bool> _flags;
        private readonly Dictionary<string, object> _others;

        public ProductTypeEntityTests()
        {
            _productTypeId = TestHelpers.RandomString(8);
            _name = TestHelpers.RandomString(5);
            _description = TestHelpers.RandomString(240);
            _flags = new Dictionary<string, bool>();
            _flags.Add(TestHelpers.RandomString(10), true);
            _flags.Add(TestHelpers.RandomString(10), false);
            _others = new Dictionary<string, object>();
            _others.Add(TestHelpers.RandomString(10), TestHelpers.RandomString(10));
        }

        public ProductTypeEntity CreateProductTypeEntity()
        {
            return new ProductTypeEntity()
            {
                Id = _productTypeId,
                Name = _name,
                Description = _description,
                Flags = _flags,
                Others = _others
            };
        }

        [Fact]
        public void ValidateEntityCreationAndFieldsValues() 
        {
            var productTypeEntity = CreateProductTypeEntity();
            
            productTypeEntity.Id.Should().Equals(_productTypeId);
            productTypeEntity.Name.Should().Equals(_name);
            productTypeEntity.Description.Should().Equals(_description);
            productTypeEntity.Flags.Should().Equals(_flags);
            productTypeEntity.Others.Should().Equals(_others);
            productTypeEntity.Should().NotBeNull();
            productTypeEntity.Should().BeOfType<ProductTypeEntity>();
        }
        
        [Fact]
        public void ValidateMethodShouldBeCalled()
        {
            var productTypeEntity = CreateProductTypeEntity();

            productTypeEntity.Validate().IsValid.Should().BeTrue();
            productTypeEntity.Validate().Should().BeOfType<ValidationResult>();
        }
    }
}
