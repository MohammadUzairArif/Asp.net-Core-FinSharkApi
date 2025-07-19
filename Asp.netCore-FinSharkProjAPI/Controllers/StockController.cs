using Asp.netCore_FinSharkProjAPI.Data;
using Asp.netCore_FinSharkProjAPI.Dtos.Stock;
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

        [HttpPost]
        public IActionResult Create([FromBody] CreateStockRequestDto stockDto)
        {
            if (stockDto == null)
            {
                return BadRequest("Invalid stock data.");
            }
            var stock = stockDto.ToStockFromCreateDto(); // Convert DTO to Model
            context.Stocks.Add(stock); // Add the stock to the context
            context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = stock.Id }, stock.ToStockDto()); // Return the created stock as a DTO
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UpdateStockRequestDto updateDto)
        {
            var stockModel = context.Stocks.FirstOrDefault(s => s.Id == id);
            if (stockModel is null)
                return NotFound($"Stock with ID {id} not found.");

            if (updateDto is null)
                return BadRequest("Invalid stock data.");

            stockModel.UpdateFromDto(updateDto);  // 🔄 Use the extension method

            context.SaveChanges();
            return Ok(stockModel.ToStockDto());   // 🔁 Return updated DTO
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var stockModel = context.Stocks.FirstOrDefault(s => s.Id == id);
            if (stockModel is null)
                return NotFound($"Stock with ID {id} not found.");
            context.Stocks.Remove(stockModel);
            context.SaveChanges();
            return NoContent(); // 204 No Content
        }

    }
}
