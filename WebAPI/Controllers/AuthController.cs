using Business.Abstract;
using Entities.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public ActionResult Login(SystemUserForLoginDto systemUserForLoginDto)
        {
            var systemUserToLogin = _authService.Login(systemUserForLoginDto);
            if (!systemUserToLogin.Success)
            {
                return BadRequest(systemUserToLogin.Message);
            }

            var result = _authService.CreateAccessToken(systemUserToLogin.Data);
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }

        [HttpPost("register")]
        public ActionResult Register(SystemUserForRegisterDto systemUserForRegisterDto)
        {
            var systemUserExists = _authService.UserExists(systemUserForRegisterDto.Email);
            if (!systemUserExists.Success)
            {
                return BadRequest(systemUserExists.Message);
            }

            var registerResult = _authService.Register(systemUserForRegisterDto, systemUserForRegisterDto.Password);
            var result = _authService.CreateAccessToken(registerResult.Data);
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }
    }
}
