 using OrderService.Application.Interfaces;
using OrderService.Domain.Entities;
 namespace OrderService.Infrastructure.Persistence
 {
 public class OrderRepository : IOrderRepository
    {
        private readonly OrderDbContext _dbContext;

        public OrderRepository(OrderDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task SaveOrderAsync(Order order)
        {
            await _dbContext.Orders.AddAsync(order);
            await _dbContext.SaveChangesAsync();
        }
    }
 }