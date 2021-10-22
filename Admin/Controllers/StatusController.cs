using API.Common;
using API.Services.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(policy: "Admin")]
    [Authorize(policy: "TransactionAdminApi")]
    public class StatusController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public StatusController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        public IActionResult Get()
        {
            var response = _transactionService.GetTransactionsStatuses();
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
    }
}
