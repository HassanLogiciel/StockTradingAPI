using API.Common;
using API.Services.Services.Dtos;
using API.Services.Services.Interfaces;
using API.Services.Services.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Register.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Register : ControllerBase
    {
        private readonly IRegisterService _registerService;
        private readonly IRoleService _roleService;
        public Register(IRegisterService registerService, IRoleService roleService)
        {
            _registerService = registerService;
            _roleService = roleService;
        }   
        public async Task<IActionResult> Post([FromBody]UserDto model)
        {
            var response = new Response();
            if (ModelState.IsValid)
            {
                response = await _registerService.RegisterUser(model);
                if (response.Errors.Count == 0)
                {
                    return Ok(response);
                }
            }
            else
            {
                var modelErrors = string.Join(',', ModelState.Select(x => x.Value.Errors).Where(y => y.Count > 0).ToList());
                response.Errors.Add(modelErrors);
            }
            return BadRequest(response);
        }

        [Route("AddRole")]
        [HttpPost]
        public async Task<IActionResult> AddRole([FromBody] RoleDto model)
        {
            var response = new Response();
            if (ModelState.IsValid)
            {
                response = await _roleService.AddRole(model);
                if (response.Errors.Count == 0)
                {
                    return Ok(response);
                }
            }
            else
            {
                var modelErrors = string.Join(',', ModelState.Select(x => x.Value.Errors).Where(y => y.Count > 0).ToList());
                response.Errors.Add(modelErrors);
            }
            return BadRequest(response);
        }
    }
}
