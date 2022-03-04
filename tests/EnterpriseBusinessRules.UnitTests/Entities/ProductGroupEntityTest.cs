using System.Collections.Generic;
using Xunit;
using FluentAssertions;
using FluentValidation.Results;
using EnterpriseBusinessRules.UnitTests.Helpers;
using EnterpriseBusinessRules.Entities;

namespace EnterpriseBusinessRules.UnitTests.Entities
{
    public class ProductGroupEntityTests
    {
        private readonly string _productGroupId;
        private readonly string _name;
        private readonly string _description;
        private readonly Dictionary<string, bool> _flags;
        private readonly Dictionary<string, object> _others;
        private readonly Dictionary<string, object> _defaults;

        public ProductGroupEntityTests()
        {
            _productGroupId = TestHelpers.RandomString(8);
            _name = TestHelpers.RandomString(5);
            _description = TestHelpers.RandomString(240);
            _flags = new Dictionary<string, bool>();
            _flags.Add(TestHelpers.RandomString(10), true);
            _flags.Add(TestHelpers.RandomString(10), false);
            _others = new Dictionary<string, object>();
            _others.Add(TestHelpers.RandomString(10), TestHelpers.RandomString(10));
            _defaults = new Dictionary<string, object>();
            _defaults.Add(TestHelpers.RandomString(10), TestHelpers.RandomString(10));
        }

        public ProductGroupEntity CreateProductGroupEntity()
        {
            return new ProductGroupEntity()
            {
                Id = _productGroupId,
                Name = _name,
                Description = _description,
                Formula = TestHelpers.RandomString(4),
                FormulaWithoutCapital = TestHelpers.RandomString(4),
                Fields = new List<string>(),
                Defaults = _defaults,
                Flags = _flags,
                Others = _others
            };
        }

        [Fact]
        public void ValidateEntityCreationAndFieldsValues() 
        {
            var productGroupEntity = CreateProductGroupEntity();
            
            productGroupEntity.Id.Should().Equals(_productGroupId);
            productGroupEntity.Name.Should().Equals(_name);
            productGroupEntity.Description.Should().Equals(_description);
            productGroupEntity.Formula.Should().BeOfType<string>();
            productGroupEntity.FormulaWithoutCapital.Should().BeOfType<string>();
            productGroupEntity.Fields.Should().BeOfType<List<string>>();
            productGroupEntity.Defaults.Should().Equals(_defaults);
            productGroupEntity.Flags.Should().Equals(_flags);
            productGroupEntity.Others.Should().Equals(_others);
            productGroupEntity.Should().NotBeNull();
            productGroupEntity.Should().BeOfType<ProductGroupEntity>();
        }
        
        [Fact]
        public void ValidateMethodShouldBeCalled()
        {
            var productGroupEntity = CreateProductGroupEntity();

            productGroupEntity.Validate().IsValid.Should().BeTrue();
            productGroupEntity.Validate().Should().BeOfType<ValidationResult>();
        }
    }
}
