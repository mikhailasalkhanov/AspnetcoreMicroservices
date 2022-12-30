using Shopping.Agregator.Extensions;
using Shopping.Agregator.Models;

namespace Shopping.Agregator.Services;

public class BasketService : IBasketService
{
    private readonly HttpClient _client;

    public BasketService(HttpClient client)
    {
        _client = client;
    }

    public async Task<BasketModel> GetBasket(string username)
    {
        var response = await _client.GetAsync($"/api/v1/Basket/{username}");
        return await response.ReadContentAs<BasketModel>();
    }
}