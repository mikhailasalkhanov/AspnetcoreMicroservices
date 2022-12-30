using Shopping.Agregator.Extensions;
using Shopping.Agregator.Models;

namespace Shopping.Agregator.Services;

public class OrderService : IOrderService
{
    private readonly HttpClient _client;

    public OrderService(HttpClient client)
    {
        _client = client;
    }

    public async Task<IEnumerable<OrderResponseModel>> GetOrderByUserName(string username)
    {
        var response = await _client.GetAsync($"/api/v1/Order/{username}");
        return await response.ReadContentAs<List<OrderResponseModel>>();
    }
}