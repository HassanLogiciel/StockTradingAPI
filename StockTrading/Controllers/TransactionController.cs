using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockTrading.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        public async Task<IActionResult> Deposit()
        {
            return Ok();
        }

        public async Task<IActionResult> Withdraw()
        {
            return Ok();
        }
    }
}
