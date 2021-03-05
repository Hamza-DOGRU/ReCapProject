using Business.Abstract;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public partial class SystemUserManager : ISystemUserService
    {
        ISystemUserDal _systemUserDal;

        public SystemUserManager(ISystemUserDal systemUserDal)
        {
            _systemUserDal = systemUserDal;
        }

        public List<OperationClaim> GetClaims(SystemUser systemUser)
        {
            return _systemUserDal.GetClaims(systemUser);
        }

        public void Add(SystemUser systemUser)
        {
            _systemUserDal.Add(systemUser);
        }

        public SystemUser GetByMail(string email)
        {
            return _systemUserDal.Get(u => u.Email == email);
        }
    }
}
