using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class BusketService(IBusketRepository _repository, IMapper _mapper) : IBusketService
    {
        public Task<bool> DeleteBusketAsync(string id)
                => _repository.DeleteBusketAsync(id);

        public async Task<BasketDto?> GetBusketByIdAsync(string id)
        {
            var busket = await _repository.GetBusketByIdAsync(id);

            return busket is null ?
                    throw new BusketNotFoundException(id)
                    : _mapper.Map<BasketDto>(busket);
        }

        public async Task<BasketDto?> UpdateBusketAsync(BasketDto busket)
        {
            var customerBusket = _mapper.Map<CustomerBusket>(busket);
            var updatedBusket = await _repository.UpdateBusketAsync(customerBusket);

            return updatedBusket is null ?
                throw new Exception("Can't Update now !!")
                : _mapper.Map<BasketDto>(updatedBusket);
        }
    }
}
