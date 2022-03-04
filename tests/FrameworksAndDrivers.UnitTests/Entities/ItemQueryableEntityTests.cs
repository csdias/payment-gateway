using FluentAssertions;
using FrameworksAndDrivers.Database.Entities;
using FrameworksAndDrivers.UnitTests.Helpers;
using Xunit;
using Xunit.Abstractions;

namespace FrameworksAndDrivers.UnitTests.Entities
{
    public class ItemQueryableEntityTests
    {
        private readonly ITestOutputHelper _output;

        public ItemQueryableEntityTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        [Trait("Entity", "ItemQueryableEntity")]
        public void ShouldReturnValidOfTheItemQueryable()
        {
            // Arrange & Act
            var productId = TestHelpers.RandomString(8);
            var itemQueryableEntity = new ItemQueryableEntity()
            {
                ProductId = productId
            };

            // Assert
            itemQueryableEntity.Should().BeOfType<ItemQueryableEntity>();
            itemQueryableEntity.Should().NotBeNull();
            itemQueryableEntity.ProductId.Should().Equals(productId);
        }
    }
}
