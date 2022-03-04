using System.Collections.Generic;
using Xunit;
using FluentAssertions;
using FluentValidation.Results;
using EnterpriseBusinessRules.UnitTests.Helpers;
using EnterpriseBusinessRules.Entities;

namespace EnterpriseBusinessRules.UnitTests.Entities
{
    public class ProductClientEntityTests
    {
        private readonly string _productClientid;
        private readonly string _name;
        private readonly string _description;
        private readonly Dictionary<string, bool> _flags;
        private readonly Dictionary<string, object> _others;

        public  ProductClientEntityTests()
        {
            _productClientid = TestHelpers.RandomString(8);
            _name = TestHelpers.RandomString(10);
            _description = TestHelpers.RandomString(255);
            _flags = new Dictionary<string, bool>();
            _flags.Add(TestHelpers.RandomString(10), true);
            _flags.Add(TestHelpers.RandomString(10), false);
            _others = new Dictionary<string, object>();
            _others.Add(TestHelpers.RandomString(10), TestHelpers.RandomString(10));
        }

        public ProductClientEntity CreateProductClientEntity()
        {
            return new ProductClientEntity()
            {
                Id = _productClientid,
                Name = _name,
                Description = _description,
                Flags = _flags,
                Others = _others
            };
        }

        [Fact]
        public void ValidateEntityCreationAndFieldsValues() 
        {
            var productClientEntity = CreateProductClientEntity();
            
            productClientEntity.Id.Should().Equals(_productClientid);
            productClientEntity.Name.Should().Equals(_name);
            productClientEntity.Description.Should().Equals(_description);
            productClientEntity.Flags.Should().Equals(_flags);
            productClientEntity.Others.Should().Equals(_others);
            productClientEntity.Should().NotBeNull();
            productClientEntity.Should().BeOfType<ProductClientEntity>();
        }
        
        [Fact]
        public void ValidateMethodShouldBeCalled()
        {
            var productClientEntity = CreateProductClientEntity();

            productClientEntity.Validate().IsValid.Should().BeTrue();
            productClientEntity.Validate().Should().BeOfType<ValidationResult>();
        }
    }
}
