using Asp.netCore_FinSharkProjAPI.Data;
using Asp.netCore_FinSharkProjAPI.Dtos.Stock;
using Asp.netCore_FinSharkProjAPI.Helpers;
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
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query) {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var stocks = await stockRepo.GetAllStocksAsync(query);
            var stockDtos = stocks.Select(s => s.ToStockDto()).ToList();
            if (stocks == null)
            {
                return NotFound("No stocks found.");
            }
            return Ok(stockDtos);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id) {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
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
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (stockDto == null)
            {
                return BadRequest("Invalid stock data.");
            }
            var stock = stockDto.ToStockFromCreateDto(); // Convert DTO to Model
            await stockRepo.CreateAsync(stock); // Save to database
            return CreatedAtAction(nameof(GetById), new { id = stock.Id }, stock.ToStockDto()); // Return the created stock as a DTO
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateStockRequestDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var stockModel = await stockRepo.UpdateAsync(id, updateDto);
            if (stockModel is null)
                return NotFound($"Stock with ID {id} not found.");

            if (updateDto is null)
                return BadRequest("Invalid stock data.");

            return Ok(stockModel.ToStockDto());   // 🔁 Return updated DTO
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var stockModel =await stockRepo.DeleteAsync(id);
            if (stockModel is null)
                return NotFound($"Stock with ID {id} not found.");
            
            return NoContent(); // 204 No Content
        }

    }
}
