using Shopping.Agregator.Models;

namespace Shopping.Agregator.Services;

public interface IOrderService
{
    Task<IEnumerable<OrderResponseModel>> GetOrderByUserName(string username);
}