using CosturasCrisApi.Communs;
using CosturasCrisApi.Models;
using CosturasCrisApi.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CosturasCrisApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomersService _customersService;
        protected readonly ILogger<CustomersController> _logger;
        public CustomersController(ICustomersService customersService, ILogger<CustomersController> logger)
        {
            this._customersService = customersService;
            this._logger=logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                int associateId = UserManagement.ValidateAssociateId(HttpContext.User.Identity as ClaimsIdentity);
                var response = await this._customersService.GetCustomers(associateId);
                return Ok(response);
            }
            catch (System.Exception)
            {
                return StatusCode(900, Messages.ErrorMessage);
            }         
        }

        [HttpPost]
        public IActionResult Post(Customers customers)
        {
            try
            {
                int associateId = UserManagement.ValidateAssociateId(HttpContext.User.Identity as ClaimsIdentity);
                customers.AssociateId = associateId;
                this._customersService.AddCustomer(customers);
                return Ok();
            }
            catch (System.Exception)
            {
                return StatusCode(800, Messages.ErrorNewCustomer);
            }         
        }

        [HttpPut("{customers}")]
        public IActionResult Put(Customers customers)
        {
            try
            {
                int associateId = UserManagement.ValidateAssociateId(HttpContext.User.Identity as ClaimsIdentity);
                customers.AssociateId = associateId;
                this._customersService.UpdateCustomer(customers);
                return Ok();
            }
            catch (System.Exception)
            {
                return StatusCode(800, Messages.ErrorUpdateCustomer);
            }         
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            try
            {
                this._customersService.DeleteCustomer(id);
                return Ok();
            }
            catch (System.Exception)
            {
                return StatusCode(800, Messages.ErrorDeleteCustomer);
            }      
        }
    }
}