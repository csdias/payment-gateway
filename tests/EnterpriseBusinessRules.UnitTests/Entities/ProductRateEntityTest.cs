using System.Collections.Generic;
using Xunit;
using FluentAssertions;
using FluentValidation.Results;
using EnterpriseBusinessRules.UnitTests.Helpers;
using EnterpriseBusinessRules.Entities;

namespace EnterpriseBusinessRules.UnitTests.Entities
{
    public class ProductRateEntityTests
    {
        private readonly float _rate;
        private readonly Dictionary<string, object> _fields;
        private readonly Dictionary<string, bool> _flags;
        private readonly Dictionary<string, object> _others;

        public ProductRateEntityTests()
        {
            _rate = 10F;
            _fields = new Dictionary<string, object>();
            _fields.Add(TestHelpers.RandomString(10), TestHelpers.RandomString(10));
            _flags = new Dictionary<string, bool>();
            _flags.Add(TestHelpers.RandomString(10), true);
            _flags.Add(TestHelpers.RandomString(10), false);
            _others = new Dictionary<string, object>();
            _others.Add(TestHelpers.RandomString(10), TestHelpers.RandomString(10));
        }

        public ProductRateEntity CreateProductRateEntity()
        {
            return new ProductRateEntity()
            {
                Rate = _rate,
                Fields = _fields,
                Flags = _flags,
                Others = _others
            };
        }

        [Fact]
        public void ValidateEntityCreationAndFieldsValues() 
        {
            var productRateEntity = CreateProductRateEntity();
            
            productRateEntity.Rate.Should().Be(_rate);
            productRateEntity.Fields.Should().Equals(_fields);
            productRateEntity.Flags.Should().Equals(_flags);
            productRateEntity.Others.Should().Equals(_others);
            productRateEntity.Should().NotBeNull();
            productRateEntity.Should().BeOfType<ProductRateEntity>();
        }
        
        [Fact]
        public void ValidateMethodShouldBeCalled()
        {
            var productRateEntity = CreateProductRateEntity();

            productRateEntity.Validate().IsValid.Should().BeTrue();
            productRateEntity.Validate().Should().BeOfType<ValidationResult>();
        }
    }
}
