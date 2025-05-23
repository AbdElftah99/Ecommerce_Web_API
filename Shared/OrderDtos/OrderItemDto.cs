namespace Shared.OrderDtos
{
    public record OrderItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PictureUrl { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}