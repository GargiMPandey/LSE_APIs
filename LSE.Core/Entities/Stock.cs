using System.Collections.Generic;

namespace LSE.Core.Entities
{
    public class Stock
    {
        public int Id { get; set; }
        public string TickerSymbol { get; set; } = null!;
        public ICollection<Trade> Trades { get; set; } = new List<Trade>();
    }
}