using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfSystemUserDal : EfEntityRepositoryBase<SystemUser, ReCapProjectContext>, ISystemUserDal
    {
        public List<OperationClaim> GetClaims(SystemUser systemUser)
        {
            using (var context = new ReCapProjectContext())
            {
                var result = from operationClaim in context.OperationClaims
                             join systemUserOperationClaim in context.SystemUserOperationClaims
                                 on operationClaim.Id equals systemUserOperationClaim.OperationClaimId
                             where systemUserOperationClaim.SystemUserId == systemUser.Id
                             select new OperationClaim { Id = operationClaim.Id, Name = operationClaim.Name };
                return result.ToList();

            }
        }
    }
}
