using CosturasCrisApi.Data;
using CosturasCrisApi.Models;
using CosturasCrisApi.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using CosturasCrisApi.Communs;

namespace CosturasCrisApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssociateController : Controller
    {
        private readonly IAssociateService _associateService;
        private readonly CosturasCrisContext _costurasCrisContext;
        public IConfiguration _configuration;

        public AssociateController(IConfiguration config,IAssociateService associateService, CosturasCrisContext costurasCrisContext)
        {
            this._associateService = associateService;
            this._costurasCrisContext = costurasCrisContext;
            this._configuration = config;
        }

        [HttpGet]
        public Task<IEnumerable<Associate>> Get()
        {
            return this._associateService.GetAssociates();
        }

        [HttpGet("{id}", Name = "GetAssociate")]
        public Task<Associate> Get(int id)
        {
            return this._associateService.GetAssociate(id);
        }

        [HttpPost, Route("login")]
        public IActionResult LoginUser ([FromBody]Associate associate)
        {
            var user = _costurasCrisContext.Associate.Where(x=>x.UserAccount== EncryptionSewing.DecodeFrom64(associate.UserAccount) && 
                                                            x.Password== EncryptionSewing.DecodeFrom64(associate.Password)).FirstOrDefault();
            if (associate == null)
            {
                return BadRequest("Invalid user request");
            }
            if (user!=null)
            {
                var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("AssociateId", user.Id.ToString()),
                        new Claim("UserAccount", user.UserAccount),
                };
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                var tokenOptios = new JwtSecurityToken(
                    _configuration["Jwt:Issuer"],
                    _configuration["Jwt:Audience"],
                    claims: claims,
                    expires: DateTime.Now.AddHours(3),
                    signingCredentials: signingCredentials
                    ) ;
                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptios);
                return Ok( new {token=tokenString} );
            }
            return Unauthorized(); 
        }

        [HttpPut]
        public void Put(Associate associate)
        {
            this._associateService.UpdateAssociate(associate);
        }

        [HttpDelete]
        public void Delete(int id)
        {
            this._associateService.DeleteAssociate(id);
        }
    }
}
