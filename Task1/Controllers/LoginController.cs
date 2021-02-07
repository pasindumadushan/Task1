using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task1.Classes;
using Task1.Models;

namespace Task1.Controllers
{
    [Route("api/Login")]
    [ApiController]
    public class LoginController : Controller
    {
        [HttpGet]
        public IActionResult Create(string UserName, string Password)
        {
            if (UserName != "test" && Password != "123")
                return Unauthorized();

            var token = new JwtTokenBuilder()
                                .AddSecurityKey(JwtSecurityKey.Create("fiversecretfiversecret "))
                                .AddSubject("james bond")
                                .AddIssuer("Fiver.Security.Bearer")
                                .AddAudience("Fiver.Security.Bearer")
                                .AddClaim("MembershipId", "111")
                                .AddExpiry(1)
                                .Build();

            return Ok(token.Value);
        }
    }
}
