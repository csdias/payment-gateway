using System.Collections.Generic;
using Xunit;
using FluentAssertions;
using EnterpriseBusinessRules.UnitTests.Helpers;
using EnterpriseBusinessRules.Entities;

namespace EnterpriseBusinessRules.UnitTests.Validators
{
    public class ProductRateValidatorTests
    {
        [Fact]
        public void FieldsFieldShouldBeRequired()
        {
            var others = new Dictionary<string, object>();
            others.Add(TestHelpers.RandomString(10), TestHelpers.RandomString(10));

            var fields = new Dictionary<string, object>();
            fields.Add(TestHelpers.RandomString(10), TestHelpers.RandomString(10));

            var clientEntity = new ProductRateEntity()
            {
                Fields = fields,
                Others = others,
                Rate = 10F
            };

            clientEntity.Validate().IsValid.Should().BeTrue();

            clientEntity.Fields = null;

            clientEntity.Validate().Errors[0].ErrorMessage.Should().Be("Fields is required");

            clientEntity.Fields = fields;

            clientEntity.Validate().IsValid.Should().BeTrue();

        }

    }
}
