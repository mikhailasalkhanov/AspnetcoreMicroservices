using Microsoft.EntityFrameworkCore;
using Ordering.Application.Contracts.Persistence;
using ordering.Domain.Entities;
using Ordering.Infrastructure.Persistance;

namespace Ordering.Infrastructure.Repositories;

public class OrderRepository : RepositoryBase<Order>, IOrderRepository
{
    public OrderRepository(OrderContext dbContext) : base(dbContext)
    {
    }

    public async Task<IEnumerable<Order>> GetOrdersByUserName(string userName)
    {
        if (_dbContext.Orders == null)
        {
            return new List<Order>();
        }
        return await _dbContext.Orders
            .Where(o => o.UserName == userName).ToListAsync();
    }
}