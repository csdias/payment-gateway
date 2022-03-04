using System.Collections.Generic;
using FluentAssertions;
using FrameworksAndDrivers.Database.Entities;
using FrameworksAndDrivers.UnitTests.Helpers;
using Xunit;
using Xunit.Abstractions;

namespace FrameworksAndDrivers.UnitTests.Entities
{
    public class RatesQueryableEntityTests
    {
        private readonly ITestOutputHelper _output;

        public RatesQueryableEntityTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        [Trait("Entity", "RatesQueryableEntity")]
        public void ShouldReturnValidOfTheRatesQueryable()
        {
            // Arrange & Act
            var productId = TestHelpers.RandomString(8);
            var ratesQueryableEntity = new RatesQueryableEntity()
            {
                ProductId = productId,
                Fields = new Dictionary<string, object>(),
                Others = new Dictionary<string, object>(),
                Rate = 300.00F
            };

            // Assert
            ratesQueryableEntity.Should().BeOfType<RatesQueryableEntity>();
            ratesQueryableEntity.Should().NotBeNull();
            ratesQueryableEntity.ProductId.Should().Equals(productId);
            ratesQueryableEntity.Fields.Should().BeOfType<Dictionary<string, object>>();
            ratesQueryableEntity.Others.Should().BeOfType<Dictionary<string, object>>();
            ratesQueryableEntity.Rate.Should().Be(300.00F);
        }
    }
}