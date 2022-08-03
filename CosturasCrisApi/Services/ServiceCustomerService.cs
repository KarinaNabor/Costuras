using CosturasCrisApi.Data;
using CosturasCrisApi.Models;
using CosturasCrisApi.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CosturasCrisApi.Services
{
    public class ServiceCustomerService : IServiceCustomerService
    {
        private readonly CosturasCrisContext _costurasCrisContext;

        public ServiceCustomerService(CosturasCrisContext costurasCrisContext)
        {
            this._costurasCrisContext = costurasCrisContext;
        }

        public void AddServiceCustomer(ServicesCustomer servicesCustomer)
        {
            DateTime currencyDate= DateTime.Now;
            string codeRandom =string.Empty;
            codeRandom = Guid.NewGuid().ToString();
            if(servicesCustomer.serviceCustomersList.Count>0)
            {
                for(int i = 0; i < servicesCustomer.serviceCustomersList.Count; i++)
                {
                    servicesCustomer.serviceCustomersList[i].RegistrationDate = currencyDate;
                    servicesCustomer.serviceCustomersList[i].CodeClothing = codeRandom;
                }
            }
            this._costurasCrisContext.ServiceCustomer.AddRange(servicesCustomer.serviceCustomersList);
            this.Save();
        }

        public void AddServiceCustomer(ServiceCustomer serviceCustomer)
        {
            this._costurasCrisContext.ServiceCustomer.Add(serviceCustomer);
            this.Save();
        }

        public async Task<List<ServiceCustomer>> GetServicesCustomerByCustomerId(int id)
        {
            DateTime currentDate=DateTime.Today;
            return await _costurasCrisContext.ServiceCustomer.Where(x => x.CustomerId == id
            && x.RegistrationDate == currentDate).ToListAsync();
        }

        public async Task<IEnumerable<ServiceCustomer>> GetServicesCustomers(int associateId)
        {
            this.UpdateStatusOrder();
            var response = await _costurasCrisContext.ServiceCustomer
                .Join
                (
                    _costurasCrisContext.Customers,
                    servicesCustomer => servicesCustomer.CustomerId, 
                    customer => customer.Id,
                    (servicesCustomer, customer) => new { servicesCustomer, customer} 
                )
                .Where
                (
                    e =>e.customer.AssociateId==associateId 
                    && !(e.servicesCustomer.Status == Models.Enum.StatusServiceCustomer.E 
                    && e.servicesCustomer.StatusPaid == Models.Enum.StatusPaid.P)
                )
                .Select(x=>x.servicesCustomer).ToListAsync();
            
            return response;
        }

        public async Task<IEnumerable<ServiceCustomer>> GetServicesCustomersCompleted(int associateId)
        {
            var response = await _costurasCrisContext.ServiceCustomer
                .Join
                (
                    _costurasCrisContext.Customers,
                    servicesCustomer => servicesCustomer.CustomerId,
                    customer => customer.Id,
                    (servicesCustomer, customer) => new { servicesCustomer, customer }
                )
                .Where
                (
                    e => e.customer.AssociateId == associateId
                    && (e.servicesCustomer.Status == Models.Enum.StatusServiceCustomer.E
                    && e.servicesCustomer.StatusPaid == Models.Enum.StatusPaid.P)
                )
                .Select(x => x.servicesCustomer).ToListAsync();

            return response;
        }

        public void UpdateServicesToCustomer(ServicesCustomer services)
        {         
            string newCode = services.serviceCustomersList.FirstOrDefault(x=>!(x.CodeClothing==null)).CodeClothing;
            for (int i = 0; i < services.serviceCustomersList.Count; i++)
            {
                var itemToUpdate = _costurasCrisContext.ServiceCustomer.FirstOrDefault(x =>
                    x.CodeClothing.Equals(services.serviceCustomersList[i].CodeClothing) 
                    && x.ProductServiceId==services.serviceCustomersList[i].ProductServiceId);

                if (itemToUpdate!=null)
                {
                    itemToUpdate.CustomerId = services.serviceCustomersList[i].CustomerId;
                    itemToUpdate.ProductServiceId= services.serviceCustomersList[i].ProductServiceId;
                    itemToUpdate.StatusPaid= services.serviceCustomersList[i].StatusPaid;
                    itemToUpdate.RealPrice= services.serviceCustomersList[i].RealPrice;
                    itemToUpdate.DescriptionService= services.serviceCustomersList[i].DescriptionService;
                    itemToUpdate.DeliveryDate= services.serviceCustomersList[i].DeliveryDate;
                    this._costurasCrisContext.ServiceCustomer.Update(itemToUpdate);
                    this.Save();
                }
                else
                {
                    ServiceCustomer serviceCustomer=services.serviceCustomersList[i];
                    serviceCustomer.CodeClothing = newCode;
                    this.AddServiceCustomer(serviceCustomer);
                }
            }
        }

        public void UpdateServiceCustomer(ServiceCustomer serviceCustomer)
        {
            var itemsToUpdate = _costurasCrisContext.ServiceCustomer.Where(x=>
            x.CodeClothing.Equals(serviceCustomer.CodeClothing)).ToList();

            foreach(var item in itemsToUpdate)
            {
                item.CustomerId = item.CustomerId;
                item.RealPrice = item.RealPrice;
                item.CodeClothing = item.CodeClothing;
                item.Id = item.Id;
                item.ProductServiceId = item.ProductServiceId;
                item.RegistrationDate = item.RegistrationDate;
                item.DescriptionService = item.DescriptionService;
                item.StatusPaid = item.StatusPaid;
                item.Status = item.Status;
                item.DeliveryDate = serviceCustomer.DeliveryDate;

                if (item.Id.Equals(serviceCustomer.Id))
                { 
                    item.StatusPaid = serviceCustomer.StatusPaid;
                    item.Status = serviceCustomer.Status;                                     
                }
                if (serviceCustomer.Status == Models.Enum.StatusServiceCustomer.E)
                {
                    item.Status=serviceCustomer.Status;
                }
            }
            this._costurasCrisContext.ServiceCustomer.UpdateRange(itemsToUpdate); 
            this.Save();
        }

        public void DeleteServiceCustomer(int id)
        {
            ServiceCustomer serviceCustomer = this._costurasCrisContext
                .ServiceCustomer
                .FirstOrDefault(x => x.Id.Equals(id));

            this._costurasCrisContext.ServiceCustomer.Remove(serviceCustomer);
            this.Save();
        }

        public void UpdateStatusOrder()
        {
            var order = _costurasCrisContext.ServiceCustomer.ToList();
            DateTime today = DateTime.Today;
            foreach (var item in order)
            {     
                int result = DateTime.Compare(today, item.DeliveryDate);
                var statusActual = item.Status.Equals(Models.Enum.StatusServiceCustomer.T)|| 
                                   item.Status.Equals(Models.Enum.StatusServiceCustomer.E);

                if (result > 0 && statusActual == false)
                {
                    item.Status = Models.Enum.StatusServiceCustomer.R;
                }
            }
            _costurasCrisContext.ServiceCustomer.UpdateRange(order);
            this.Save();
        }

        public void Save()
        {
            this._costurasCrisContext.SaveChanges();
        }
    }
}
