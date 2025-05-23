namespace Domain.Entities
{
    public class BusketItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }
        public string BrandName { get; set; }
        public string TypeName { get; set; }
        public int Quantity { get; set; }
    }
}