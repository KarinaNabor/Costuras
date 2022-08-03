using CosturasCrisApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CosturasCrisApi.Services.Contracts
{
    public interface IAssociateService 
    {
        public void AddAssociate(Associate associate);
        Task<IEnumerable<Associate>> GetAssociates();
        Task<Associate> GetAssociate(int id);
        public void UpdateAssociate(Associate associate);
        public void DeleteAssociate(int id);
    }
}
