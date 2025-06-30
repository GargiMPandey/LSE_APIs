using System.ComponentModel.DataAnnotations;

namespace LSE.Core.DTO
{
    public class TradeRequest
    {
        [Required(ErrorMessage = "Ticker is required.")]
        [StringLength(16, MinimumLength = 4, ErrorMessage = "Ticker must be between 4 and 16 characters.")]
        public string TickerSymbol { get; set; } = null!;


        [Required(ErrorMessage = "Price is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero.")]
        public decimal Price { get; set; }


        [Required(ErrorMessage = "Number of shares is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Shares must be greater than zero.")]
        public decimal NumberOfShares { get; set; }


        [Required(ErrorMessage = "Broker ID is required.")]
        public string? BrokerId { get; set; }
    }
}