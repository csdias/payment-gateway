using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Moq;
using FluentAssertions;
using FluentAssertions.Execution;
using EnterpriseBusinessRules.Entities;
using ApplicationBusinessRules.Interfaces;
using ApplicationBusinessRules.Services;

namespace ApplicationBusinessRules.UnitTests.UseCases.User 
{
    public class ProducServiceTest 
    {
        private readonly Mock<IReadProductUseCase> _mock;

        public ProducServiceTest() {
            this._mock = new Mock<IReadProductUseCase>(MockBehavior.Strict);
        }

        [Fact]
        public async void FindOneProductShouldBeCalledOnce() {
            var expectedResponseFromUseCase = new Response<ProductEntity>();
            
            this._mock.Setup(s => s.FindOneProduct(It.IsAny<PaymentFilterEntity>()))
                     .Returns(() => Task.Run( () => expectedResponseFromUseCase));

            var filter = new PaymentFilterEntity();
            var sut = new ProductService(this._mock.Object);
            var result = await sut.FindOneProduct(filter);

            this._mock.Verify(x => x.FindOneProduct(It.IsAny<PaymentFilterEntity>()), Times.Once);
        }

        [Fact]
        public async void FindOneProductShouldBeOk() {
            var productEntityToBeFound = FindProductFromUseCase().First();
            var expectedResponseFromUseCase = new Response<ProductEntity>();
            expectedResponseFromUseCase.SetSuccess(true);
            expectedResponseFromUseCase.SetResponse(productEntityToBeFound);

            this._mock.Setup(s => s.FindOneProduct(It.IsAny<PaymentFilterEntity>()))
                    .Returns(() => Task.Run(() => expectedResponseFromUseCase));
            
            var filter = new PaymentFilterEntity();
            var sut = new ProductService(this._mock.Object);
            var result = await sut.FindOneProduct(filter);

            using(new AssertionScope()){
                result.IsOk().Should().BeTrue();
                result.HasErrors().Should().BeFalse();
                result.HasException().Should().BeFalse();
                result.GetResponse().Should().BeEquivalentTo(expectedResponseFromUseCase.GetResponse());
            }
        }

        [Fact]
        public async void FindProductShouldBeCalledOnce() {
            var expectedResponseFromUseCase = new Response<List<ProductEntity>>();
            
            this._mock.Setup(s => s.FindProduct(It.IsAny<PaymentFilterEntity>()))
                     .Returns(() => Task.Run( () => expectedResponseFromUseCase));

            var filter = new PaymentFilterEntity();
            var sut = new ProductService(this._mock.Object);
            var result = await sut.FindProduct(filter);

            this._mock.Verify(x => x.FindProduct(It.IsAny<PaymentFilterEntity>()), Times.Once);
        }

        [Fact]
        public async void FindProductShouldBeOk() {
            var productEntityListToBeFound = FindProductFromUseCase();
            var expectedResponseFromUseCase = new Response<List<ProductEntity>>();
            expectedResponseFromUseCase.SetSuccess(true);
            expectedResponseFromUseCase.SetResponse(productEntityListToBeFound);

            this._mock.Setup(s => s.FindProduct(It.IsAny<PaymentFilterEntity>()))
                    .Returns(() => Task.Run(() => expectedResponseFromUseCase));

            var filter = new PaymentFilterEntity();
            var sut = new ProductService(this._mock.Object);
            var result = await sut.FindProduct(filter);
            
            using(new AssertionScope()){
                result.IsOk().Should().BeTrue();
                result.HasErrors().Should().BeFalse();
                result.HasException().Should().BeFalse();
                result.GetResponse().Count().Should().Be(expectedResponseFromUseCase.GetResponse().Count());
            }
        }

        private ProductEntity FindOneProductFromUseCase() { 
            return FindProductFromUseCase().FirstOrDefault();
        }

        private List<ProductEntity> FindProductFromUseCase() {
            var productEntityI = new ProductEntity() {
                Id = "AB05F",
                Name = "Morte Acidental por 5 anos",
                Description = "Morte Acidental por 5 anos - Portfolio F - AB05F",
                Type = GetProductTypeEntityI(),
                Group = GetProductGroupEntityI(),
                Family = GetProductFamilyEntityI(),
                Capital = GetProductCapitalEntityI(),
                Rates = GetProductRateEntityListI(),
                Others = new Dictionary<string, object>(),
                Items = GetProductEntityListI()
            };

            var productEntityII = new ProductEntity() {
                Id = "WLUPF",
                Name = "Vida Inteira Único",
                Description = "Vida Inteira Único - Portfolio F - WLUPF",
                Type = GetProductTypeEntityII(),
                Group = GetProductGroupEntityII(),
                Family = GetProductFamilyEntityII(),
                Capital = GetProductCapitalEntityII(),
                Rates = GetProductRateEntityListII(),
                Others = new Dictionary<string, object>(),
                Items = GetProductEntityListII()
            };

            return new List<ProductEntity>() {
                productEntityI,
                productEntityII
            };
        }

#region ProductEntityI
        private ProductTypeEntity GetProductTypeEntityI() {
            return new  ProductTypeEntity() {
                Id = "ADDT",
                Name = "Cobertura Adicional",
                Description = "Cobertura Adicional",
                Others = new Dictionary<string, object>() {}
            };
        }

        private ProductGroupEntity GetProductGroupEntityI() {
            return new ProductGroupEntity() {
                Id = "DEFAULT",
                Name = "Regra padrão",
                Description = "Regra padrão",
                Formula = "({rate}*{capital})/1000",
                FormulaWithoutCapital = "({amount}*1000)/{rate}",
                Fields = new List<string> {
                    "age",
                    "gender"
                },
                Defaults = null,
                Others = new Dictionary<string, object>() {}
            };
        }

        private ProductFamilyEntity GetProductFamilyEntityI() {
            return new ProductFamilyEntity() {
                Id = "ADBU",
                Name = "Morte Acidental",
                Description = "Morte Acidental",
                Others = new Dictionary<string, object>()                
            };
        }
        private ProductCapitalEntity GetProductCapitalEntityI() {
            return new ProductCapitalEntity() {
                Minimum = 5_000f,
                Maximum = 25_000f
            };
        }
        private List<ProductRateEntity> GetProductRateEntityListI() {
            return new List<ProductRateEntity>();
        } 
        private List<ProductEntity> GetProductEntityListI() {
            return new List<ProductEntity>();
        }
#endregion ProductEntityI

#region ProductEntityII
        private ProductTypeEntity GetProductTypeEntityII() {
            return new  ProductTypeEntity() {
                Id = "BASIC",
                Name = "Cobertura Básica",
                Description = "Cobertura Básica",
                Others = new Dictionary<string, object>() {}
            };
        }

        private ProductGroupEntity GetProductGroupEntityII() {
            return new ProductGroupEntity() {
                Id = "DEFAULT",
                Name = "Regra padrão",
                Description = "Regra padrão",
                Formula = "({rate}*{capital})/1000",
                FormulaWithoutCapital = "({amount}*{1000})/{rate}",
                Fields = new List<string> {
                    "age",
                    "gender"
                },
                Defaults = null,
                Others = new Dictionary<string, object>() {}
            };
        }

        private ProductFamilyEntity GetProductFamilyEntityII() {
            return new ProductFamilyEntity() {
                Id = "WDU",
                Name = "Vida Inteira Modificado",
                Description = "Vida Inteira Modificado",
                Others = new Dictionary<string, object>()                
            };
        }
        private ProductCapitalEntity GetProductCapitalEntityII() {
            return new ProductCapitalEntity() {
                Minimum = 10_000f,
                Maximum = 300_000f
            };
        }

        private List<ProductRateEntity> GetProductRateEntityListII() {
            var fields_22_STANDARD_F = new Dictionary<string, object>();
            fields_22_STANDARD_F.Add("age", 22);
            fields_22_STANDARD_F.Add("class", "STANDARD");
            fields_22_STANDARD_F.Add("gender", "F");

            var fields_22_STANDARD_M = new Dictionary<string, object>();
            fields_22_STANDARD_M.Add("age", 22);
            fields_22_STANDARD_M.Add("class", "STANDARD");
            fields_22_STANDARD_M.Add("gender", "F");

            return new List<ProductRateEntity>() {
                new ProductRateEntity() {
                    Fields = fields_22_STANDARD_F,
                    Others = new Dictionary<string, object>(),
                    Rate = 300
                },
                new ProductRateEntity() {
                    Fields = fields_22_STANDARD_M,
                    Others = new Dictionary<string, object>(),
                    Rate = 200
                }
            };
        }

        private List<ProductEntity> GetProductEntityListII() {
            return new List<ProductEntity>() {
                GetProductEntityIII()
            };
        }
#endregion ProductEntityII

#region ProductEntityIII
        private ProductEntity GetProductEntityIII(){
            return new ProductEntity() {
                Id = "AB05F",
                Name = "Morte Acidental por 5 anos",
                Description = "Morte Acidental por 5 anos - Portfolio F - AB05F",
                Type = GetProductTypeEntityIII(),
                Group = GetProductGroupEntityIII(),
                Family = GetProductFamilyEntityIII(),
                Capital = GetProductCapitalEntityIII(),
                Rates = new List<ProductRateEntity>(),
                Others = new Dictionary<string, object>(),
                Items = new List<ProductEntity>()
            };
        }
        private ProductTypeEntity GetProductTypeEntityIII() {
            return new ProductTypeEntity() {
                Id = "ADDT",
                Name = "Cobertura Adicional",
                Description ="Cobertura Adicional",
                Others = new Dictionary<string, object>()
            };
        }

        private ProductGroupEntity GetProductGroupEntityIII() {
            return new ProductGroupEntity() {
                Id = "DEFAULT",
                Name = "Regra padrão",
                Description = "Regra padrão",
                Formula = "({rate}*{capital})/1000",
                FormulaWithoutCapital = "({amount}*1000)/{rate}",
                Fields = new List<string>() {
                    "age",
                    "gender"
                },
                Defaults = null,
                Others = new Dictionary<string, object>()
            };
        }
        private ProductFamilyEntity GetProductFamilyEntityIII() {
            return new ProductFamilyEntity() {
                Id = "ADBU",
                Name = "Morte Acidental",
                Description =  "Morte Acidental",
                Others = new Dictionary<string, object>()
            };
        }
        private ProductCapitalEntity GetProductCapitalEntityIII() {
            return new ProductCapitalEntity() {
                Minimum = 5_000,
                Maximum = 25_000
            };
        }
#endregion ProductEntityIII
    }
}
