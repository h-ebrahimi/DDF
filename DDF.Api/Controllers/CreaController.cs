using DDF.Services.Contract.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DDF.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreaController : ControllerBase
    {
        private readonly ILoginTransactionService _loginService;
        public CreaController(ILoginTransactionService loginService)
        {
            _loginService = loginService;
        }

        [HttpGet("Login")]
        public async Task<IActionResult> Login()
        {
            var response = await _loginService.Login();
            return Ok(response);
        }
    }
}
