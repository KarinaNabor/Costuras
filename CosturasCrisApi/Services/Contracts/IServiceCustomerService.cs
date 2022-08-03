using CosturasCrisApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CosturasCrisApi.Services.Contracts
{
    public interface IServiceCustomerService
    {
        public void AddServiceCustomer(ServicesCustomer servicesCustomer);
        Task<IEnumerable<ServiceCustomer>> GetServicesCustomers(int associateId);
        Task<IEnumerable<ServiceCustomer>> GetServicesCustomersCompleted(int associateId);
        Task<List<ServiceCustomer>> GetServicesCustomerByCustomerId(int id);
        public void UpdateServiceCustomer(ServiceCustomer serviceCustomer);
        public void DeleteServiceCustomer(int id);
        public void UpdateServicesToCustomer(ServicesCustomer services);
        public void AddServiceCustomer(ServiceCustomer serviceCustomer);
    }
}
