namespace Domain.Exceptions
{
    public class ProductNotFoundExcpetion : NotFoundException
    {
        public ProductNotFoundExcpetion(int id) : base($"Product with id : {id} not found")
        {
        }
    }
}
