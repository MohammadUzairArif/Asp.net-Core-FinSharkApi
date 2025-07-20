using Asp.netCore_FinSharkProjAPI.Data;
using Asp.netCore_FinSharkProjAPI.Dtos.Stock;
using Asp.netCore_FinSharkProjAPI.Interfaces;
using Asp.netCore_FinSharkProjAPI.Mappers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Asp.netCore_FinSharkProjAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IStockRepository stockRepo;

        public StockController(ApplicationDbContext context, IStockRepository stockRepo)
        {
            this.context = context;
            this.stockRepo = stockRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() { 
            var stocks = await stockRepo.GetAllStocksAsync();
            var stockDtos = stocks.Select(s => s.ToStockDto());
            if (stocks == null)
            {
                return NotFound("No stocks found.");
            }
            return Ok(stockDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) { 
            var stock =await stockRepo.GetStockByIdAsync(id);
            if (stock == null)
            {
                return NotFound($"Stock with ID {id} not found.");
            }
            return Ok(stock.ToStockDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStockRequestDto stockDto)
        {
            if (stockDto == null)
            {
                return BadRequest("Invalid stock data.");
            }
            var stock = stockDto.ToStockFromCreateDto(); // Convert DTO to Model
            await stockRepo.CreateAsync(stock); // Save to database
            return CreatedAtAction(nameof(GetById), new { id = stock.Id }, stock.ToStockDto()); // Return the created stock as a DTO
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateStockRequestDto updateDto)
        {
            var stockModel = await stockRepo.UpdateAsync(id, updateDto);
            if (stockModel is null)
                return NotFound($"Stock with ID {id} not found.");

            if (updateDto is null)
                return BadRequest("Invalid stock data.");

            return Ok(stockModel.ToStockDto());   // 🔁 Return updated DTO
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var stockModel =await stockRepo.DeleteAsync(id);
            if (stockModel is null)
                return NotFound($"Stock with ID {id} not found.");
            
            return NoContent(); // 204 No Content
        }

    }
}
