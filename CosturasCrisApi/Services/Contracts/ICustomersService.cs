using CosturasCrisApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CosturasCrisApi.Services.Contracts
{
    public interface ICustomersService
    {
        public void AddCustomer(Customers customers);
        Task<IEnumerable<Customers>> GetCustomers(int associateId);
        public void UpdateCustomer(Customers customers);
        public void DeleteCustomer(int id);
    }
}
