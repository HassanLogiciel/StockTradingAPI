using API.Common;
using API.Services.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Register.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClaimController : ControllerBase
    {
        private readonly IClaimService _claimService;

        public ClaimController(IClaimService claimService)
        {
            _claimService = claimService;
        }

        public async Task<IActionResult> Post(ClaimVm model)
        {
            var response = new Response();
            if (ModelState.IsValid)
            {
                response = await _claimService.AddClaim(model);
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
