using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        private readonly IConfiguration _configuration;

        public LoginController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Login loginModel)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "https://dev-46138610.okta.com/oauth2/default/v1/token");

            var body = new Dictionary<string, string>
        {
            {"grant_type", "password"},
            {"username", loginModel.Username},
            {"password", loginModel.Password},
            {"scope", "openid profile"}
        };

            request.Content = new FormUrlEncodedContent(body);
            request.Headers.Add("Accept", "application/json");
            request.Headers.Add("Authorization", $"Basic {Convert.ToBase64String(Encoding.ASCII.GetBytes($"{_configuration["Okta:ClientId"]}:{_configuration["Okta:ClientSecret"]}"))}");

            var response = await client.SendAsync(request);
            var responseBody = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                return BadRequest("Invalid login credentials.");
            }

            return Ok(responseBody);
        }

        //[HttpGet("Login")]
        //public IActionResult Login([FromQuery] string returnUrl)
        //{
        //    var redirectUrl = returnUrl is null ? Url.Content("~/") : "/" + returnUrl;
        //    if (User.Identity.IsAuthenticated)
        //    {
        //        return LocalRedirect(returnUrl);
        //    }
        //    return Challenge();
        //}

        //[HttpGet("Logout")]
        //public async Task<IActionResult> Logout([FromQuery] string returnUrl)
        //{
        //    var redirectUrl = returnUrl is null ? Url.Content("~/") : "/" + returnUrl;
        //    if (!User.Identity.IsAuthenticated)
        //    {
        //        return LocalRedirect(returnUrl);
        //    }
        //    await HttpContext.SignOutAsync();

        //    return LocalRedirect(returnUrl);
        //}
    }
}

