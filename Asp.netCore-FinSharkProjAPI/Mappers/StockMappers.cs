using Asp.netCore_FinSharkProjAPI.Dtos.Stock;
using Asp.netCore_FinSharkProjAPI.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Asp.netCore_FinSharkProjAPI.Mappers
{
    public static class StockMappers
    {
        /* Model → DTO
        public static ModelNameDto ToDto(this ModelName model)
        {
            return new ModelNameDto
            {
                Id = model.Id,
                Property1 = model.Property1,
                Property2 = model.Property2
                // Map remaining properties
            };
        }
        */
        public static StockDto ToStockDto(this Stock stockModel) { 
            return new StockDto
            {
                Id = stockModel.Id,
                Symbol = stockModel.Symbol,
                CompanyName = stockModel.CompanyName,
                Purchase = stockModel.Purchase,
                LastDiv = stockModel.LastDiv,
                Industry = stockModel.Industry,
                MarketCap = stockModel.MarketCap
            };

        }

        /* DTO → Model
        public static ModelName ToModel(this ModelNameDto dto)
        {
            return new ModelName
            {
                Id = dto.Id,
                Property1 = dto.Property1,
                Property2 = dto.Property2
                // Map remaining properties
            };
        }
        */
        public static Stock ToStockFromCreateDto(this CreateStockRequestDto stockDto) { 
            return new Stock
            {
                Symbol = stockDto.Symbol,
                CompanyName = stockDto.CompanyName,
                Purchase = stockDto.Purchase,
                LastDiv = stockDto.LastDiv,
                Industry = stockDto.Industry,
                MarketCap = stockDto.MarketCap
            };
        }

        public static void UpdateFromDto(this Stock stockModel, UpdateStockRequestDto dto)
        {
            stockModel.Symbol = dto.Symbol;
            stockModel.CompanyName = dto.CompanyName;
            stockModel.Purchase = dto.Purchase;
            stockModel.LastDiv = dto.LastDiv;
            stockModel.Industry = dto.Industry;
            stockModel.MarketCap = dto.MarketCap;
        }
    }
}
