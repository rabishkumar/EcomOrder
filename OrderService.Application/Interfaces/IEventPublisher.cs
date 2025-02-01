using OrderService.Domain.Entities;
namespace OrderService.Application.Interfaces
{
    public interface IEventPublisher
    {
        Task PublishOrderCreatedAsync(Order order);
    }
}