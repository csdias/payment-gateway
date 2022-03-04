// using Xunit;
// using FluentAssertions;
// using EnterpriseBusinessRules.UnitTests.Helpers;
// using EnterpriseBusinessRules.Entities;

// namespace EnterpriseBusinessRules.UnitTests.Entities
// {
//     public class InsuranceQuoteInputItemValidatorTests
//     {
//         public InsuranceQuoteInputItemEntity CreateInsuranceQuoteInputItemEntity()
//         {
//             return new InsuranceQuoteInputItemEntity()
//             {
//                 Product = TestHelpers.RandomString(7),
//                 Amount = new InsuranceQuoteInputAmountEntity()
//                 {
//                     Type = "monthly",
//                     Amount = 1000F
//                 }
//             };
//         }

//         [Fact]
//         public void ProductFieldShouldBeRequired()
//         {
//             var clientEntity = CreateInsuranceQuoteInputItemEntity();

//             clientEntity.Validate().IsValid.Should().BeTrue();

//             clientEntity.Product = null;

//             clientEntity.Validate().Errors[0].ErrorMessage.Should().Be("Product is required");

//             clientEntity.Product = TestHelpers.RandomString(7);

//             clientEntity.Validate().IsValid.Should().BeTrue();

//         }

//         [Theory]
//         [InlineData(2)]
//         [InlineData(9)]
//         public void ProductCharacterQuantityShouldHaveError(int quantity)
//         {
//             var clientEntity = CreateInsuranceQuoteInputItemEntity();

//             clientEntity.Validate().IsValid.Should().BeTrue();

//             clientEntity.Product = TestHelpers.RandomString(quantity);

//             clientEntity.Validate().Errors[0].ErrorMessage.Should().Contain("characters");
//             clientEntity.Validate().Errors.Count.Should().Be(1);

//             clientEntity.Product = TestHelpers.RandomString(7);

//             clientEntity.Validate().IsValid.Should().BeTrue();

//         }

//         [Fact]
//         public void CapitalFieldShouldBeRequired()
//         {
//             var clientEntity = CreateInsuranceQuoteInputItemEntity();

//             clientEntity.Validate().IsValid.Should().BeTrue();

//             clientEntity.Amount = null;

//             clientEntity.Validate().Errors[0].ErrorMessage.Should().Be("Capital is required");

//             clientEntity.Amount = new InsuranceQuoteInputAmountEntity();

//             clientEntity.Validate().IsValid.Should().BeTrue();

//         }
//     }
// }
