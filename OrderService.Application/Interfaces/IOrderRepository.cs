using OrderService.Domain.Entities;
namespace OrderService.Application.Interfaces
{
    public interface IOrderRepository
    {
        Task SaveOrderAsync(Order order);
    }
}