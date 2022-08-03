using CosturasCrisApi.Communs;
using CosturasCrisApi.Models;
using CosturasCrisApi.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CosturasCrisApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceCustomerController : ControllerBase
    {
        private readonly IServiceCustomerService _serviceCustomerService;

        public ServiceCustomerController(IServiceCustomerService serviceCustomerService)
        {
            this._serviceCustomerService = serviceCustomerService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                int associateId = UserManagement.ValidateAssociateId(HttpContext.User.Identity as ClaimsIdentity);
                var response = await this._serviceCustomerService.GetServicesCustomers(associateId);
                return Ok(response);
            }
            catch (Exception)
            {
                return StatusCode(800, Messages.ErrorMessage);
            }
        }

        [Route("orders/completed")]
        [HttpGet]
        public async Task<IActionResult> GetOrdersCompleted()
        {
            try
            {
                int associateId = UserManagement.ValidateAssociateId(HttpContext.User.Identity as ClaimsIdentity);
                var response = await this._serviceCustomerService.GetServicesCustomersCompleted(associateId);
                return Ok(response);
            }
            catch (Exception)
            {
                return StatusCode(800, Messages.ErrorMessage);
            }
        }

        [Route("ServiceCustomer/{customerId}/orders")]
        [HttpGet]
        public async Task<IActionResult> GetCustomerId(int customerId)
        {
            try
            {
                var response = await this._serviceCustomerService.GetServicesCustomerByCustomerId(customerId);
                return Ok(response);
            }
            catch (Exception)
            {
                return StatusCode(800, Messages.ErrorMessage);
            }           
        }

        [HttpPost]
        public IActionResult Post(ServicesCustomer servicesCustomer)
        {
            try
            {
                this._serviceCustomerService.AddServiceCustomer(servicesCustomer);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(800,Messages.ErrorNewOrder);
            }         
        }

        [HttpPut("{serviceCustomer}")]
        public IActionResult Put(ServiceCustomer serviceCustomer)
        {
            try
            {
                this._serviceCustomerService.UpdateServiceCustomer(serviceCustomer);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(800, Messages.ErrorUpdateOrder);
            }    
        }

        [Route("ServiceCustomer/UpdateOrders")]
        [HttpPut]
        public IActionResult UpdateOrders(ServicesCustomer servicesCustomer)
        {
            try
            {
                this._serviceCustomerService.UpdateServicesToCustomer(servicesCustomer);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(800, Messages.ErrorUpdateOrders);
            }            
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            try
            {
                this._serviceCustomerService.DeleteServiceCustomer(id);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(800, Messages.ErrorDeleteOrder);
            }           
        }
    }
}