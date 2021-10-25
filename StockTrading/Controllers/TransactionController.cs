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
    [Authorize(policy: "TransactionApi")]
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

        [HttpPost]
        [Route("Withdraw")]
        public async Task<IActionResult> Withdraw([FromBody] WithdrawVm model)
        {
            var response = new Response();
            if (ModelState.IsValid)
            {
                response = await _transactionService.WithdrawAsync(model);
                if (response.IsSuccess)
                {
                    return Ok(response);
                }
            }
            return BadRequest(response);
        }
    }
}
