using Microsoft.AspNetCore.Http;
using System;
using System.Security.Claims;

namespace CosturasCrisApi.Communs
{
    public class UserManagement
    {
        public static int ValidateAssociateId(ClaimsIdentity identity)
        {
            int AssociateId = 0;
            if (identity != null)
            {
                AssociateId = int.Parse(identity.FindFirst("AssociateId").Value);
            }
            else
            {
                throw new Exception(Messages.ErrorAssociate);
            }
            return AssociateId;
        }
    }
}
