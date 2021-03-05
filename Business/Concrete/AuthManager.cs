using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.JWT;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private ISystemUserService _systemUserService;
        private ITokenHelper _tokenHelper;

        public AuthManager(ISystemUserService systemUserService, ITokenHelper tokenHelper)
        {
            _systemUserService = systemUserService;
            _tokenHelper = tokenHelper;
        }

        public IDataResult<SystemUser> Register(SystemUserForRegisterDto systemUserForRegisterDto, string password)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);
            var systemUser = new SystemUser
            {
                Email = systemUserForRegisterDto.Email,
                FirstName = systemUserForRegisterDto.FirstName,
                LastName = systemUserForRegisterDto.LastName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true
            };
            _systemUserService.Add(systemUser);
            return new SuccessDataResult<SystemUser>(systemUser, Messages.UserRegistered);
        }

        public IDataResult<SystemUser> Login(SystemUserForLoginDto systemUserForLoginDto)
        {
            var systemUserToCheck = _systemUserService.GetByMail(systemUserForLoginDto.Email);
            if (systemUserToCheck == null)
            {
                return new ErrorDataResult<SystemUser>(Messages.UserNotFound);
            }

            if (!HashingHelper.VerifyPasswordHash(systemUserForLoginDto.Password, systemUserToCheck.PasswordHash, systemUserToCheck.PasswordSalt))
            {
                return new ErrorDataResult<SystemUser>(Messages.PasswordError);
            }

            return new SuccessDataResult<SystemUser>(systemUserToCheck, Messages.SuccessfulLogin);
        }

        public IResult UserExists(string email)
        {
            if (_systemUserService.GetByMail(email) != null)
            {
                return new ErrorResult(Messages.UserAlreadyExists);
            }
            return new SuccessResult();
        }

        public IDataResult<AccessToken> CreateAccessToken(SystemUser systemUser)
        {
            var claims = _systemUserService.GetClaims(systemUser);
            var accessToken = _tokenHelper.CreateToken(systemUser, claims);
            return new SuccessDataResult<AccessToken>(accessToken, Messages.AccessTokenCreated);
        }
    }
}
