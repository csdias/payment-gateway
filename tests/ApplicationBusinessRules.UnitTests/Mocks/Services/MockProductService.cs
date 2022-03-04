using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using EnterpriseBusinessRules.Entities;
using ApplicationBusinessRules.Interfaces;
using EnterpriseBusinessRules.UnitTests.Mocks.Entities; 

namespace ApplicationBusinessRules.UnitTests.Mocks.Services 
{
    public class MockProductService : IProductService 
    {
        private bool FilterProduct(ProductEntity product, PaymentFilterEntity productFilter)
        {
            if(productFilter.ProductId != null && product.Id != productFilter.ProductId) {
                //return false;
            }
            if(productFilter.TypeId != null && product.Type.Id != productFilter.TypeId) {
                //return false;
            }
            if(productFilter.GroupId != null && product.Group.Id != productFilter.GroupId) {
                //return false;
            }
            return true;
        }

        public async Task<Response<List<ProductEntity>>> FindProduct (PaymentFilterEntity productFilter) 
        {            
            return await Task.Run(() => {
                var response = new Response<List<ProductEntity>>();
                var products = MockProductEntity
                    .Data
                    .Where(item => this.FilterProduct(item, productFilter))
                    .ToList();
                return response
                    .SetSuccess(true)
                    .SetResponse(products);   
            });                     
        }
        
        public async Task<Response<ProductEntity>> FindOneProduct (PaymentFilterEntity productFilter) 
        {
            return await Task.Run(() => {
                var response = new Response<ProductEntity>();
                var productFirst = MockProductEntity
                    .Data
                    .FirstOrDefault(item => this.FilterProduct(item, productFilter));
                if(productFirst == null) {
                    return response
                        .SetSuccess(false)
                        .SetStatus(404);
                }
                return response
                    .SetSuccess(true)
                    .SetResponse(productFirst);   
            });                    
        }
    }
}