using System.ComponentModel.DataAnnotations;

namespace Shared
{
    public class BasketItemDto
    {
        public int Id { get; init; }
        public string ProductName { get; init; }
        public string PictureUrl { get; init; }
        [Range(1, double.MaxValue)]
        public decimal Price { get; init; }
        public string? Brand { get; init; }
        public string? Type { get; init; }
        [Range(1, 99)]
        public int Quantity { get; init; }
    }
}