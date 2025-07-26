using Asp.netCore_FinSharkProjAPI.Extentions;
using Asp.netCore_FinSharkProjAPI.Interfaces;
using Asp.netCore_FinSharkProjAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Asp.netCore_FinSharkProjAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PortfolioController : ControllerBase
    {
        private readonly UserManager<AppUser> userManager;
        private readonly IStockRepository stockRepo;
        private readonly IPortfolioRepository portfolioRepo;

        public PortfolioController(UserManager<AppUser> userManager, IStockRepository stockRepo, IPortfolioRepository portfolioRepo)
        {
            this.userManager = userManager;
            this.stockRepo = stockRepo;
            this.portfolioRepo = portfolioRepo;
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
    }
}
