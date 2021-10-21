using API.Common;
using API.Services.Services.Interfaces;
using API.Services.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace StockTrading.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(policy: "NormalUser")]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpPost]
        [Route("Deposit")]
        public async Task<IActionResult> Deposit([FromBody] DepositVm model)
        {
            var response = new Response();
            if (ModelState.IsValid)
            {
                response = await _transactionService.DepositAsync(model);
                if (response.IsSuccess)
                {
                    return Ok(response);
                }
            }
            return BadRequest(response);
        }

        public async Task<IActionResult> Withdraw()
        {
            return Ok();
        }
    }
}
