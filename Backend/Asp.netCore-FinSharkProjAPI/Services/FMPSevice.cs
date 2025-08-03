using Asp.netCore_FinSharkProjAPI.Dtos.Stock;
using Asp.netCore_FinSharkProjAPI.Interfaces;
using Asp.netCore_FinSharkProjAPI.Mappers;
using Asp.netCore_FinSharkProjAPI.Models;
using Newtonsoft.Json;


namespace Asp.netCore_FinSharkProjAPI.Services
{
    public class FMPSevice : IFMPService
    {
        private readonly HttpClient httpClient;
        private readonly IConfiguration config;

        public FMPSevice(HttpClient httpClient, IConfiguration config)
        {
            this.httpClient = httpClient;
            this.config = config;
        }
        public async Task<Stock> FindStockBySymbolAsync(string symbol)
        {
            try {
                var result = await httpClient.GetAsync($"https://financialmodelingprep.com/api/v3/profile/{symbol}?apikey={config["FMPKey"]}");
                if(result.IsSuccessStatusCode)
                {
                    var content = await result.Content.ReadAsStringAsync();
                    var tasks = JsonConvert.DeserializeObject<FMPStock[]>(content);
                    var stock = tasks[0];
                    if (stock != null) {
                        return stock.ToStockFromFMP();
                    }
                    return null;
                }
                return null;
            }
            catch(Exception e) {
                Console.WriteLine(e.Message);
                return null;
            }
        }
    }
}
