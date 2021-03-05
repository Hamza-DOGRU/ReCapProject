using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.JWT;
using Entities.DTOs;
using System;
using System.Text;

namespace Business.Abstract
{
    public interface IAuthService
    {
        IDataResult<SystemUser> Register(SystemUserForRegisterDto systemUserForRegisterDto, string password);
        IDataResult<SystemUser> Login(SystemUserForLoginDto systemUserForLoginDto);
        IResult UserExists(string email);
        IDataResult<AccessToken> CreateAccessToken(SystemUser systemUser);
    }
}
