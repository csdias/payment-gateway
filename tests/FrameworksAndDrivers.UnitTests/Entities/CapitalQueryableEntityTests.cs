using FluentAssertions;
using FrameworksAndDrivers.Database.Entities;
using FrameworksAndDrivers.UnitTests.Helpers;
using Xunit;
using Xunit.Abstractions;

namespace FrameworksAndDrivers.UnitTests.Entities
{
    public class CapitalQueryableEntityTests
    {
        private readonly ITestOutputHelper _output;

        public CapitalQueryableEntityTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        [Trait("Entity", "CapitalQueryableEntity")]
        public void ShouldReturnValidOfTheCapitalQueryable()
        {
            // Arrange & Act
            var productId = TestHelpers.RandomString(8);
            var capitalQueryableEntity = new CapitalQueryableEntity()
            {
                ProductId = productId,
                Minimum = 10_000F,
                Maximum = 25_000F
            };

            // Assert
            capitalQueryableEntity.Should().BeOfType<CapitalQueryableEntity>();
            capitalQueryableEntity.Should().NotBeNull();
            capitalQueryableEntity.ProductId.Should().Equals(productId);
            capitalQueryableEntity.Minimum.Should().Equals(10_000F);
            capitalQueryableEntity.Maximum.Should().Equals(25_000F);
        }
    }
}
