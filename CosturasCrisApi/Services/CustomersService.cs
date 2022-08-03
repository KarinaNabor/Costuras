using CosturasCrisApi.Data;
using CosturasCrisApi.Models;
using CosturasCrisApi.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CosturasCrisApi.Services
{
    public class CustomersService : ICustomersService
    {
        private readonly CosturasCrisContext _costurasCrisContext;

        public CustomersService(CosturasCrisContext costurasCrisContext)
        {
            this._costurasCrisContext = costurasCrisContext;
        }

        public void AddCustomer(Customers customer)
        {
            this._costurasCrisContext.Customers.Add(customer);
            this.Save();
        }

        public async Task<IEnumerable<Customers>> GetCustomers(int associateId)
        {
            return await _costurasCrisContext.Customers.Where(x=>x.AssociateId.Equals(associateId))
                .ToListAsync();
        }

        public void UpdateCustomer(Customers customer)
        {
            this._costurasCrisContext.Entry<Customers>(customer).State = EntityState.Modified;
            this.Save();
        }

        public void DeleteCustomer(int id)
        {
            Customers customer = this._costurasCrisContext
                .Customers
                .FirstOrDefault(x => x.Id.Equals(id));

            this._costurasCrisContext.Customers.Remove(customer);
            this.Save();
        }
        public void Save()
        {
            this._costurasCrisContext.SaveChanges();
        }
    }
}
