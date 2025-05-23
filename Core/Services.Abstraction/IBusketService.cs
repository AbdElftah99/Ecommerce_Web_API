namespace Services.Abstraction
{
    public interface IBusketService
    {
        Task<BasketDto?> GetBusketByIdAsync(string id);
        Task<BasketDto?> UpdateBusketAsync(BasketDto busket);
        Task<bool> DeleteBusketAsync(string id);
    }
}
