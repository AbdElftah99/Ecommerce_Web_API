namespace Domain.Contracts
{
    public interface IBusketRepository
    {
        Task<CustomerBusket?> GetBusketByIdAsync(string id);
        Task<CustomerBusket?> UpdateBusketAsync(CustomerBusket busket, TimeSpan? timeToLive = null);
        Task<bool> DeleteBusketAsync(string id);
    }
}
