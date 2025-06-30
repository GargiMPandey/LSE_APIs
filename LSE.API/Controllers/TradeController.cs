using LSE.Core.DTO;
using LSE.Core.Entities;
using LSE.Core.ServiceContract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LSE.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TradeController : ControllerBase
    {
        private readonly ITradeService _tradeService;
        private readonly ILogger<TradeController> _logger;

        public TradeController(ITradeService tradeService, ILogger<TradeController> logger)
        {
            _tradeService = tradeService;
            _logger = logger;
        }

        /// <summary>
        /// Preccess a trade request by adding a new trade to the system.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> PostTrade([FromBody] TradeRequest request)
        {

            if (request == null)
            {
                _logger.LogWarning("TradeRequest is null.");
                return BadRequest("Trade request cannot be null.");
            }
            try
            {
                await _tradeService.AddTradeAsync(request);
                return Created();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding trade.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}