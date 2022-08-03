using CosturasCrisApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CosturasCrisApi.Services.Contracts
{
    public interface IProductServiceService
    {
        public void AddProductService(ProductService productService);
        Task<IEnumerable<ProductService>> GetProductServices(int associateId);
        public void UpdateProductService(ProductService productService);
        public void DeleteProductService(int id);
    }
}
