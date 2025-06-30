using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace LSE.Core.Entities
{
    public class Trade
    {
        public int Id { get; set; }
        public int StockId { get; set; }
        public Stock Stock { get; set; } = null!;
        public decimal Price { get; set; }
        public decimal NumberOfShares { get; set; }

        [ForeignKey(nameof(Broker))]
        public Guid BrokerId { get; set; } 
        public Broker Broker { get; set; } = null!;
        public DateTime Timestamp { get; set; }
    }
}