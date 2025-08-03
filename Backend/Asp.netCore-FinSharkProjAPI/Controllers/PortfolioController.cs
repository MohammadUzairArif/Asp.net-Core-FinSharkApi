using Asp.netCore_FinSharkProjAPI.Extentions;
using Asp.netCore_FinSharkProjAPI.Interfaces;
using Asp.netCore_FinSharkProjAPI.Models;
using Asp.netCore_FinSharkProjAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace Asp.netCore_FinSharkProjAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PortfolioController : ControllerBase
    {
        private readonly UserManager<AppUser> userManager;
        private readonly IStockRepository stockRepo;
        private readonly IPortfolioRepository portfolioRepo;
        private readonly IFMPService fMPService;

        public PortfolioController(UserManager<AppUser> userManager, IStockRepository stockRepo, IPortfolioRepository portfolioRepo,IFMPService fMPService)
        {
            this.userManager = userManager;
            this.stockRepo = stockRepo;
            this.portfolioRepo = portfolioRepo;
            this.fMPService = fMPService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetUserPortfolio()
        {

            var username = User.GetUsername();

            if (string.IsNullOrWhiteSpace(username))
            {
                return BadRequest("Username claim is missing or invalid.");
            }

            var appUser = await userManager.FindByNameAsync(username);

            if (appUser == null)
            {
                return NotFound("User not found.");
            }

            var userPortfolio = await portfolioRepo.GetUserPortfolio(appUser);
            return Ok(userPortfolio);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddPortfolio(string symbol)
        {
            var username = User.GetUsername();
            
            var appUser = await userManager.FindByNameAsync(username);
           

            var stock = await stockRepo.GetBySymbolAsync(symbol);
            if (stock == null)
            {
                stock = await fMPService.FindStockBySymbolAsync(symbol);
                if (stock == null)
                {
                    return BadRequest("Stock doesnot exists");
                }
                else
                {
                    await stockRepo.CreateAsync(stock);
                }
            }

            if (stock == null)
            {
                return BadRequest("Stock not found");
            }

            var userPortfolio = await portfolioRepo.GetUserPortfolio(appUser);
            if (userPortfolio.Any(e => e.Symbol.ToLower() == symbol.ToLower()))
            {
                return BadRequest("Stock already exists in the portfolio.");
            }

            var portfolioModel = new Portfolio
            {
                StockId = stock.Id,
                AppUserId = appUser.Id,
            };
            await portfolioRepo.CreateAsync(portfolioModel);
            if (portfolioModel == null)
            {
                return BadRequest("Failed to add stock to portfolio.");
            }
            return Ok(new { Message = "Stock added to portfolio successfully." });
        }


        [HttpDelete]
        [Authorize]

        public async Task<IActionResult> DeletePortfolio(string symbol)
        {
            var username = User.GetUsername();
            if (string.IsNullOrWhiteSpace(username))
            {
                return BadRequest("Username claim is missing or invalid.");
            }
            var appUser = await userManager.FindByNameAsync(username);
            if (appUser == null)
            {
                return NotFound("User not found.");
            }
            var userPortfolio = await portfolioRepo.GetUserPortfolio(appUser);
            var filteredStock = userPortfolio.Where(e => e.Symbol.ToLower() == symbol.ToLower()).ToList();

            if (filteredStock.Count() == 1)
            {
                await portfolioRepo.DeletePortfolio(appUser, symbol);
            }
            else
            {
                return BadRequest("Stock not found in portfolio.");
            }
            return Ok(new { Message = "Stock removed from portfolio successfully." });
        }
    }
}
