using System.Collections.Generic;
using Xunit;
using FluentAssertions;
using FluentValidation.Results;
using EnterpriseBusinessRules.UnitTests.Helpers;
using EnterpriseBusinessRules.Entities;

namespace EnterpriseBusinessRules.UnitTests.Entities
{
    public class QuotationSettingsEntityTests
    {
        public QuotationSettingsEntity CreateQuotationSettingsEntity()
        {
            var settings = new Dictionary<string, object>();
            settings.Add("param1", "test");

            return new QuotationSettingsEntity()
            {
                Id = TestHelpers.RandomString(10),
                Settings = settings
            };
        }

        [Fact]
        public void ValidateEntityCreationAndFieldsValues() 
        {
            var quotationSettingsEntity = CreateQuotationSettingsEntity();
            
            quotationSettingsEntity.Id.Should().BeOfType<string>();
            quotationSettingsEntity.Settings.Should().BeOfType<Dictionary<string, object>>();
            quotationSettingsEntity.Should().BeOfType<QuotationSettingsEntity>();
        }
        
        [Fact]
        public void ValidateMethodShouldBeCalled()
        {
            var quotationSettingsEntity = CreateQuotationSettingsEntity();

            quotationSettingsEntity.Validate().IsValid.Should().BeTrue();
            quotationSettingsEntity.Validate().Should().BeOfType<ValidationResult>();
        }

    }
}
