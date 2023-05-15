using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TodoApi.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly ITokenService _tokenService;
        public AccountController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpGet("Get Token")]
        public async Task<ActionResult> SignInAndGetToken(string username, string password)
        {
            var oktaToken = await _tokenService.GetToken(username, password);
            if (oktaToken != null)
                return Ok(oktaToken);
                   
            return null;
        }
    }
}

