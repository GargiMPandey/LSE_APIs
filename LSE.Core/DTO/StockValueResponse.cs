namespace LSE.Core.DTO
{
    public class StockValueResponse
    {
        public string TickerSymbol { get; set; } = null!;
        public decimal AveragePrice { get; set; }
    }
}