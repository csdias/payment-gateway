using System.Collections.Generic;
using Xunit;
using FluentAssertions;
using FluentValidation.Results;
using EnterpriseBusinessRules.UnitTests.Helpers;
using EnterpriseBusinessRules.Entities;

namespace EnterpriseBusinessRules.UnitTests.Entities
{
    public class ProductFamilyEntityTests
    {
        private readonly string _productFamilyId;
        private readonly string _name;
        private readonly string _description;
        private readonly Dictionary<string, bool> _flags;
        private readonly Dictionary<string, object> _others;

        public ProductFamilyEntityTests()
        {
            _productFamilyId = TestHelpers.RandomString(8);
            _name = TestHelpers.RandomString(5);
            _description = TestHelpers.RandomString(240);
            _flags = new Dictionary<string, bool>();
            _flags.Add(TestHelpers.RandomString(10), true);
            _flags.Add(TestHelpers.RandomString(10), false);
            _others = new Dictionary<string, object>();
            _others.Add(TestHelpers.RandomString(10), TestHelpers.RandomString(10));
        }

        public ProductFamilyEntity CreateProductFamilyEntity()
        {
            return new ProductFamilyEntity()
            {
                Id = _productFamilyId,
                Name = _name,
                Description = _description,
                Flags = _flags,
                Others = _others
            };
        }

        [Fact]
        public void ValidateEntityCreationAndFieldsValues() 
        {
            var productFamilyEntity = CreateProductFamilyEntity();
            
            productFamilyEntity.Id.Should().Equals(_productFamilyId);
            productFamilyEntity.Name.Should().Equals(_name);
            productFamilyEntity.Description.Should().Equals(_description);
            productFamilyEntity.Flags.Should().Equals(_flags);
            productFamilyEntity.Others.Should().Equals(_others);
            productFamilyEntity.Should().NotBeNull();
            productFamilyEntity.Should().BeOfType<ProductFamilyEntity>();
        }
        
        [Fact]
        public void ValidateMethodShouldBeCalled()
        {
            var productFamilyEntity = CreateProductFamilyEntity();

            productFamilyEntity.Validate().IsValid.Should().BeTrue();
            productFamilyEntity.Validate().Should().BeOfType<ValidationResult>();
        }
    }
}
