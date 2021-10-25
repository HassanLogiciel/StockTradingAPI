using API.Services.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace StockTrading.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(policy: "NormalUser")]
    [Authorize(policy: "NWalletApi")]
    public class WalletController : ControllerBase
    {
        private readonly IWalletService _walletService;

        public WalletController(IWalletService walletService)
        {
            _walletService = walletService;
        }

        [Route("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var response =await  _walletService.GetWalletAsync(id);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
    }
}
