using System;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;
using Xunit.Abstractions;
using FluentAssertions;
using EnterpriseBusinessRules.Entities;
using FrameworksAndDrivers.Database.Contexts;
using FrameworksAndDrivers.Database.Models;
using FrameworksAndDrivers.Database.Repositories;
using FrameworksAndDrivers.UnitTests.Helpers;
using ApplicationBusinessRules.Interfaces;
using FrameworksAndDrivers.UnitTests.Mocks.Database.Contexts;
using EnterpriseBusinessRules.UnitTests.Mocks.Entities;

[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]
namespace FrameworksAndDrivers.UnitTests.Repositories
{
    public class ProductRepositoryTests
    {
        private readonly ITestOutputHelper _output;
        private Mock<ApplicationDbContext> _mockDbContext;
        private Mock<ICacheRepository> _mockCacheRepository;

        public ProductRepositoryTests(ITestOutputHelper output)
        {
            this._output = output;
            this
                .MockDbContext()
                .MockCacheRepository();            
        }

        private ProductRepositoryTests MockDbContext()
        {        
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().Options;
            this._mockDbContext = new Mock<ApplicationDbContext>(options);
            this._mockDbContext.Setup(c => c.Clients)
                .Returns(MoqExtensions.DbSetMock<ProductClientModel>(MockProductClientModel.Data).Object);
            this._mockDbContext.Setup(c => c.Types)
                .Returns(MoqExtensions.DbSetMock<ProductTypeModel>(MockProductTypeModel.Data).Object);
            this._mockDbContext.Setup(c => c.Families)
                .Returns(MoqExtensions.DbSetMock<ProductFamilyModel>(MockProductFamilyModel.Data).Object);
            this._mockDbContext.Setup(c => c.Groups)
                .Returns(MoqExtensions.DbSetMock<ProductGroupModel>(MockProductGroupModel.Data).Object);
            this._mockDbContext.Setup(c => c.Products)
                .Returns(MoqExtensions.DbSetMock<CreditCardModel>(MockProductModel.Data).Object);
            this._mockDbContext.Setup(c => c.Capitals)
                .Returns(MoqExtensions.DbSetMock<PaymentModel>(MockProductCapitalModel.Data).Object);
            this._mockDbContext.Setup(c => c.ProductProducts)
                .Returns(MoqExtensions.DbSetMock<ProductProductModel>(MockProductProductModel.Data).Object);
            this._mockDbContext.Setup(c => c.Rates)
                .Returns(MoqExtensions.DbSetMock<ProductRateModel>(MockProductRateModel.Data).Object);
            return this;
        }

        private ProductRepositoryTests MockCacheRepository()
        {
            this._mockCacheRepository = new Mock<ICacheRepository>();
            this._mockCacheRepository
                .Setup(s => s.CreateKey(It.IsAny<string>(), It.IsAny<Object>()))
                .Returns("teste");
            this._mockCacheRepository
                .Setup(s => s.GetValue<Response<ProductEntity>>(It.IsAny<string>()))
                .Returns(default(Response<ProductEntity>));
            this._mockCacheRepository
                .Setup(s => s.GetValue<Response<List<ProductEntity>>>(It.IsAny<string>()))
                .Returns(default(Response<List<ProductEntity>>));
            this._mockCacheRepository
                .Setup(s => s.SetValue(It.IsAny<string>(), It.IsAny<string>(), 2));
            return this;
        }

        [Fact]
        [Trait("Repository", "RatesQueryableEntity")]
        public void CreateQueryRates_ShouldReturnRatesOfTheProduct_WhenAddCapitalsAndProductFiltered()
        {
            var clientId = "A953DC88EB1B350CE0532C118C0A2285";
            var repo = new ProductRepository(this._mockDbContext.Object, this._mockCacheRepository.Object);
            var result = repo.CreateQueryRates(clientId);
        }

        [Fact]
        [Trait("Repository", "ItemQueryableEntity")]
        public void CreateQueryItems_ShouldReturnItemsOfTheProduct_WhenAddCapitalsAndProductFiltered()
        {
            var clientId = "A953DC88EB1B350CE0532C118C0A2285";
            var repo = new ProductRepository(this._mockDbContext.Object, this._mockCacheRepository.Object);
            var result = repo.CreateQueryItems(clientId);
        }

        [Fact]
        [Trait("Repository", "ProductEntity")]
        public async void FindProduct_ShouldReturnListOfTheProduct_WhenProductFiltered()
        {
            var repo = new ProductRepository(this._mockDbContext.Object, this._mockCacheRepository.Object);
            var productFilter =  new PaymentFilterEntity {
                ClientId = "A953DC88EB1B350CE0532C118C0A2285"
            };
            var response = await repo.FindProduct(productFilter);
            var products = response.GetResponse();
            products.Should().NotBeEmpty();
            products.Count().Should().BeGreaterThan(1);
            products[0].Id.Should().Be(MockProductEntity.Data[0].Id);
            products[1].Id.Should().Be(MockProductEntity.Data[1].Id);
        }

        [Fact]
        [Trait("Repository", "ProductEntity")]
        public async void FindProduct_ShouldReturnAProduct_WhenProductFiltered()
        {
            var repo = new ProductRepository(this._mockDbContext.Object, this._mockCacheRepository.Object);
            var productFilter =  new PaymentFilterEntity
            {
                ClientId = "A953DC88EB1B350CE0532C118C0A2285",
                ProductId = "WLUPF"
            };
            var response = await repo.FindOneProduct(productFilter);
            var product = response.GetResponse();
            product.Should().NotBeNull();
            product.Id.Should().Be("WLUPF");
        }
    }
}
