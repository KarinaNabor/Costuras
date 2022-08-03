using CosturasCrisApi.Data;
using CosturasCrisApi.Models;
using CosturasCrisApi.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CosturasCrisApi.Services
{
    public class ProductServiceService : IProductServiceService
    {
        private readonly CosturasCrisContext _costurasCrisContext;

        public ProductServiceService(CosturasCrisContext costurasCrisContext)
        {
            this._costurasCrisContext = costurasCrisContext;
        }

        public void AddProductService(ProductService productService)
        {
            this._costurasCrisContext.ProductService.Add(productService);
            this.Save();
        }

        public async Task<IEnumerable<ProductService>> GetProductServices(int associateId)
        {
            return await _costurasCrisContext.ProductService.Where(x=>x.AssociateId == associateId)
                .ToListAsync();
        }

        public void UpdateProductService(ProductService productService)
        {
            this._costurasCrisContext.Entry<ProductService>(productService).State = EntityState.Modified;
            this.Save();
        }

        public void DeleteProductService(int id)
        {
            ProductService productService = this._costurasCrisContext
                .ProductService
                .FirstOrDefault(x => x.Id.Equals(id));

            this._costurasCrisContext.ProductService.Remove(productService);
            this.Save();
        }
        public void Save()
        {
            this._costurasCrisContext.SaveChanges();
        }
    }
}
