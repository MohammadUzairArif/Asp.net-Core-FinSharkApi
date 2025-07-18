using Asp.netCore_FinSharkProjAPI.Data;
using Asp.netCore_FinSharkProjAPI.Mappers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Asp.netCore_FinSharkProjAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public StockController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult GetAll() { 
            var stocks = context.Stocks.ToList().Select(s=>s.ToStockDto());
            if (stocks == null)
            {
                return NotFound("No stocks found.");
            }
            return Ok(stocks);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id) { 
            var stock = context.Stocks.FirstOrDefault(s => s.Id == id);
            if (stock == null)
            {
                return NotFound($"Stock with ID {id} not found.");
            }
            return Ok(stock.ToStockDto());
        }
    }
}
