using API.Common;
using API.Services.Services;
using API.Services.Services.Interfaces;
using Login.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Login.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }
        public async Task<IActionResult> Post([FromBody]LoginVm model)
        {
            var response = new Response();
            if (ModelState.IsValid)
            {
                response =await _loginService.LoginUser(model);
            }
            else
            {
                var modelErrors = string.Join(',', ModelState.Select(x => x.Value.Errors).Where(y => y.Count > 0).ToList());
                response.Errors.Add(modelErrors);
            }
            if (response.IsSuccess)
            {
                return Ok(response);

            }
            else
            {
                return BadRequest(response);
            }
        }
    }
}
