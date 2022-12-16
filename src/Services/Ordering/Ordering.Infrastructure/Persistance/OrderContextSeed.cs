using Microsoft.Extensions.Logging;
using ordering.Domain.Entities;

namespace Ordering.Infrastructure.Persistance;

public class OrderContextSeed
{
    public static async Task Seed(OrderContext orderContext, ILogger<OrderContextSeed> logger)
    {
        if (orderContext.Orders is not null && !orderContext.Orders.Any())
        {
            orderContext.Orders.AddRange(GetPreconfiguredOrders());
            await orderContext.SaveChangesAsync();
            logger.LogInformation("Seed database associated with contexts {DbContextName}", typeof(OrderContext));
        }
    }

    private static IEnumerable<Order> GetPreconfiguredOrders()
    {
        return new List<Order>()
        {
            new Order
            {
                UserName = "swn",
                FirstName = "Mikhail",
                LastName = "Asalkhanov",
                EmailAddress = "asalkhanov.mikhail@gmail.com",
                AddressLine = "Rayimbek batyr",
                Country = "Kazakhstan",
                TotalPrice = 550
            }
        };
    }
}