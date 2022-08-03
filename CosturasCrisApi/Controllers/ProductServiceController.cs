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
    public class ProductServiceController : Controller
    {
        private readonly IProductServiceService _productServiceService;

        public ProductServiceController(IProductServiceService productServiceService)
        {
            this._productServiceService = productServiceService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                int associateId = UserManagement.ValidateAssociateId(HttpContext.User.Identity as ClaimsIdentity);
                var response = await _productServiceService.GetProductServices(associateId);
                return Ok(response);
            }
            catch (Exception)
            {
                return StatusCode(900, Messages.ErrorMessage);
            }
        }

        [HttpPost]
        public IActionResult Post(ProductService productService)
        {
            try
            {
                int associateId = UserManagement.ValidateAssociateId(HttpContext.User.Identity as ClaimsIdentity);
                productService.AssociateId = associateId;
                this._productServiceService.AddProductService(productService);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(800, Messages.ErrorNewServiceProduct);
            }
        }

        [HttpPut ("{productService}")]
        public IActionResult Put(ProductService productService)
        {
            try
            {
                int associateId = UserManagement.ValidateAssociateId(HttpContext.User.Identity as ClaimsIdentity);
                productService.AssociateId = associateId;
                this._productServiceService.UpdateProductService(productService);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(800,Messages.ErrorUpdateServiceProduct);
            }
            
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            try
            {
                this._productServiceService.DeleteProductService(id);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(800,Messages.ErrorDeleteServiceProduct);
            }           
        }
    }
}