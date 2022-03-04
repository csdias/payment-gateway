using System.Collections.Generic;
using Xunit;
using FluentAssertions;
using FluentValidation.Results;
using EnterpriseBusinessRules.UnitTests.Helpers;
using EnterpriseBusinessRules.Entities;

namespace EnterpriseBusinessRules.UnitTests.Entities
{
    public class ProductCapitalEntityTests
    {
        private readonly float _minimum;
        private readonly float _maximum;
        private readonly Dictionary<string, bool> _flags;
        private readonly Dictionary<string, object> _others;


        public  ProductCapitalEntityTests()
        {
            _minimum = 15000F;
            _maximum = 25000F;
            _flags = new Dictionary<string, bool>();
            _flags.Add(TestHelpers.RandomString(10), true);
            _flags.Add(TestHelpers.RandomString(10), false);
            _others = new Dictionary<string, object>();
            _others.Add(TestHelpers.RandomString(10), TestHelpers.RandomString(10));
        }

        public ProductCapitalEntity CreateProductCapitalEntity()
        {
            return new ProductCapitalEntity()
            {
                Minimum = _minimum,
                Maximum = _maximum,
                Flags = _flags,
                Others = _others
            };
        }

        [Fact]
        public void ValidateEntityCreationAndFieldsValues() 
        {
            var productCapitalEntity = CreateProductCapitalEntity();
            
            productCapitalEntity.Minimum.Should().Equals(_minimum);
            productCapitalEntity.Maximum.Should().Equals(_maximum);
            productCapitalEntity.Flags.Should().Equals(_flags);
            productCapitalEntity.Others.Should().Equals(_others);
            productCapitalEntity.Should().NotBeNull();
            productCapitalEntity.Should().BeOfType<ProductCapitalEntity>();
        }
        
        [Fact]
        public void ValidateMethodShouldBeCalled()
        {
            var productCapitalEntity = CreateProductCapitalEntity();

            productCapitalEntity.Validate().IsValid.Should().BeTrue();
            productCapitalEntity.Validate().Should().BeOfType<ValidationResult>();
        }
    }
}
