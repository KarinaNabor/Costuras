using AutoMapper;
using CosturasCrisApi.Data;
using CosturasCrisApi.Models;
using CosturasCrisApi.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CosturasCrisApi.Services
{
    public class AssociateService : IAssociateService
    {
        private readonly CosturasCrisContext _costurasCrisContext;
        public AssociateService(CosturasCrisContext costurasCrisContext)
        {
            this._costurasCrisContext = costurasCrisContext;
        }

        public void AddAssociate(Associate associate)
        {
            this._costurasCrisContext.Associate.Add(associate);
            this.Save();
        }

        public async Task<Associate> GetAssociate(int id)
        {
            return await _costurasCrisContext.Associate
                .FirstOrDefaultAsync(x => x.Id.Equals(id));
        }

        public async Task<IEnumerable<Associate>> GetAssociates()
        {
            return await _costurasCrisContext.Associate
                .ToListAsync();
        }

        public void UpdateAssociate(Associate associate)
        {
            this._costurasCrisContext.Entry<Associate>(associate).State = EntityState.Modified;
            this.Save();
        }

        public void DeleteAssociate(int id)
        {
            Associate associate = this._costurasCrisContext
                .Associate
                .FirstOrDefault(x => x.Id.Equals(id));

            this._costurasCrisContext.Associate.Remove(associate);
            this.Save();
        }
        public void Save()
        {
            this._costurasCrisContext.SaveChanges();
        }
    }
}
