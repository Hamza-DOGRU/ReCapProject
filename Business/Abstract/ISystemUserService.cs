using Core.Entities.Concrete;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface ISystemUserService
    {
        List<OperationClaim> GetClaims(SystemUser systemUser);
        void Add(SystemUser systemUser);
        SystemUser GetByMail(string email);
    }
}
