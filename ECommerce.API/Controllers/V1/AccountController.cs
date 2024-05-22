using ECommerce.BL.Abstracts;
using ECommerce.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers.V1
{
    [Route("api/v1/security")]
    public class AccountController : BaseAPIController
    {
        private readonly IConfiguration _configuration;
        private readonly IAccountService _accountService;
        public AccountController(IConfiguration configuration,IAccountService accountService)
        {
            _configuration = configuration;
            _accountService = accountService;
        }
        [HttpPost("get-token")]
        public string GetToken(string userName,string password)
        {
            var key = _configuration.GetSection("Security:JWT-Key").Value;
            var durationInMinutes = Convert.ToInt32(_configuration.GetSection("Security:JWT-Duration").Value);
            var result = _accountService.GetToken(userName,password);
            return result;
        }
    }
}
